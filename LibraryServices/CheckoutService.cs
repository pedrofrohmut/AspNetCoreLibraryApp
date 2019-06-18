using LibraryData;
using LibraryData.Models;
using LibraryServices.constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LibraryServices
{
  public class CheckoutService : ICheckout
  {
    private readonly LibraryDbContext _context;

    public CheckoutService(LibraryDbContext context)
    {
      _context = context;
    }

    public void Add(Checkout newCheckout)
    {
      _context.Add(newCheckout);
      _context.SaveChanges();
    }

    public IEnumerable<Checkout> GetAll() =>
      _context.Checkouts;

    public Checkout GetById(int id) =>
      _context.Checkouts
        .FirstOrDefault(checkout => checkout.Id == id);

    public IEnumerable<CheckoutHistory> GetCheckoutHistory(int id) =>
      _context.CheckoutHistories
        .Include(h => h.LibraryAsset)
        .Include(h => h.LibraryCard)
        .Where(h => h.LibraryAsset.Id == id);

    public IEnumerable<Hold> GetCurrentHolds(int assetId) =>
      _context.Holds
        .Include(_hold => _hold.LibraryAsset)
        .Include(_hold => _hold.LibraryCard)
        .Where(_hold => _hold.LibraryAsset.Id == assetId);

    public Checkout GetLatestCheckout(int assetId) =>
      _context.Checkouts
        .Where(c => c.LibraryAsset.Id == assetId)
        .OrderByDescending(c => c.Since)
        .FirstOrDefault();

    public void MarkFound(int assetId)
    {
      UpdateItemStatusByStatusName(assetId, StatusNames.AVAILABLE);
      RemovingExistingCheckouts(assetId);
      CloseAnyExistingCheckoutHistory(assetId);
      _context.SaveChanges();
    }

    private void UpdateItemStatusByStatusName(int assetId, string statusName)
    {
      var item = _context.LibraryAssets
        .FirstOrDefault(asset => asset.Id == assetId);

      _context.Update(item);

      item.Status = _context.Statuses
        .FirstOrDefault(status => status.Name == statusName);
    }

    private void CloseAnyExistingCheckoutHistory(int assetId)
    {
      // close any existing checkout history
      var history = _context.CheckoutHistories
        .FirstOrDefault(h => h.LibraryAsset.Id == assetId && h.CheckIn == null);

      if (history != null)
      {
        _context.Update(history);
        history.CheckIn = DateTime.Now;
      }
    }

    private void RemovingExistingCheckouts(int assetId)
    {
      // remove existing checkouts
      var checkout = _context.Checkouts
        .FirstOrDefault(c => c.LibraryAsset.Id == assetId);

      if (checkout != null)
      {
        _context.Remove(checkout);
      }
    }

    public void MarkLost(int assetId)
    {
      UpdateItemStatusByStatusName(assetId, StatusNames.LOST);
      _context.SaveChanges();
    }

    public void PlaceHold(int assetId, int libraryCardId)
    {
      var now = DateTime.Now;
      
      var asset = _context.LibraryAssets
        .Include(_asset => _asset.Status)
        .FirstOrDefault(_asset => _asset.Id == assetId);
      
      var card = _context.LibraryCards
        .FirstOrDefault(c => c.Id == libraryCardId);

      if (asset.Status.Name == StatusNames.AVAILABLE)
      {
        UpdateItemStatusByStatusName(assetId, StatusNames.ON_HOLD);
      }

      var hold = new Hold
      {
        HoldPlaced = now,
        LibraryAsset = asset,
        LibraryCard = card
      };
      _context.Add(hold);
      _context.SaveChanges();
    }

    public void CheckInItem(int assetId)
    {
      var item = _context.LibraryAssets
        .FirstOrDefault(a => a.Id == assetId);

      // Remove any exiting checkouts of the item.
      RemovingExistingCheckouts(assetId);

      // close any existing checkout history.
      CloseAnyExistingCheckoutHistory(assetId);

      // Looking for existing holds on the item.
      var currentHolds = _context.Holds
        .Include(_hold => _hold.LibraryAsset)
        .Include(_hold => _hold.LibraryCard)
        .Where(_hold => _hold.LibraryAsset.Id == assetId);

      // If there are holds, checkout the item to the 
      //   LibraryCard with the earliest hold.
      if (currentHolds.Any())
      {
        CheckoutToEarliestHold(assetId, currentHolds);
        return;
      }

      // Otherwise, update item status to available.
      UpdateItemStatusByStatusName(assetId, StatusNames.AVAILABLE);

      _context.SaveChanges();
    }

    private void CheckoutToEarliestHold(int assetId, 
      IQueryable<Hold> currentHolds)
    {
      var earliestHold = currentHolds
        .OrderBy(hold => hold.HoldPlaced)
        .FirstOrDefault();

      var card = earliestHold.LibraryCard;

      _context.Remove(earliestHold);
      _context.SaveChanges();

      CheckOutItem(assetId, card.Id);
    }

    public void CheckOutItem(int assetId, int libraryCardId)
    {
      // Add logic here to handle feedback to the user
      if (IsCheckedOut(assetId)) { return; }

      var item = _context.LibraryAssets
        .FirstOrDefault(a => a.Id == assetId);

      UpdateItemStatusByStatusName(assetId, StatusNames.CHECKED_OUT);

      var libraryCard = _context.LibraryCards
        .Include(card => card.Checkouts)
        .FirstOrDefault(card => card.Id == libraryCardId);

      var now = DateTime.Now;

      var checkout = new Checkout
      {
        LibraryAsset = item,
        LibraryCard = libraryCard,
        Since = now,
        Until = GetDefaultCheckoutTime(now)
      };  
      _context.Add(checkout);

      var checkoutHistory = new CheckoutHistory
      {
        CheckOut = now,
        LibraryAsset = item,
        LibraryCard = libraryCard
      };
      _context.Add(checkoutHistory);

      _context.SaveChanges();
    }

    private DateTime GetDefaultCheckoutTime(DateTime now) => now.AddDays(30);

    public bool IsCheckedOut(int assetId) =>
      _context.Checkouts
        .Where(c => c.LibraryAsset.Id == assetId)
        .Any();

    public string GetCurrentHoldPatron(int holdId)
    {
      var hold = _context.Holds
        .Include(_hold => _hold.LibraryAsset) 
        .Include(_hold => _hold.LibraryCard)
        .FirstOrDefault(_hold => _hold.Id == holdId);

      var cardId = hold?.LibraryCard.Id;

      var patron = _context.Patrons
        .Include(_patron => _patron.LibraryCard)
        .FirstOrDefault(_patron => _patron.LibraryCard.Id == cardId);

      return GetPatronFullname(patron);
    }

    private string GetPatronFullname(Patron patron) => 
      patron?.FirstName + " " + patron?.LastName;

    public string GetCurrentHoldPlaced(int holdId) =>
      _context.Holds
        .Include(_hold => _hold.LibraryAsset)
        .Include(_hold => _hold.LibraryCard)
        .FirstOrDefault(_hold => _hold.Id == holdId)
        .HoldPlaced
        .ToString(CultureInfo.InvariantCulture);

    public string GetCurrentCheckoutPatron(int assetId)
    {
      var checkout = GetCheckoutByAssetId(assetId);
      if (checkout == null)
      {
        return "";
      }

      var cardId = checkout.LibraryCard.Id;

      var patron = _context.Patrons
        .Include(_patron => _patron.LibraryCard)
        .FirstOrDefault(_patron => _patron.LibraryCard.Id == cardId);

      return GetPatronFullname(patron);
    }

    private Checkout GetCheckoutByAssetId(int assetId) =>
      _context.Checkouts
        .Include(_checkout => _checkout.LibraryAsset)
        .Include(_checkout => _checkout.LibraryCard)
        .FirstOrDefault(_checkout => _checkout.LibraryAsset.Id == assetId);
  }
}
