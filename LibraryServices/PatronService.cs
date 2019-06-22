using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
  public class PatronService : IPatron
  {
    private readonly LibraryDbContext _context;

    public PatronService(LibraryDbContext context)
    {
      _context = context;
    }

    public void Add(Patron newPatron)
    {
      _context.Add(newPatron);
      _context.SaveChanges();
    }

    public IEnumerable<Patron> GetAll() => 
      _context.Patrons
        .Include(patron => patron.LibraryCard)
        .Include(patron => patron.HomeLibrarybranch);

    public Patron Get(int patronId) => GetAll()
      .FirstOrDefault(patron => patron.Id == patronId);

    private int GetLibraryCardId(int patronId) => Get(patronId).LibraryCard.Id;

    public IEnumerable<CheckoutHistory> GetCheckoutHistories(int patronId) =>
      _context.CheckoutHistories
        .Include(checkoutHistory => checkoutHistory.LibraryCard)
        .Include(checkoutHistory => checkoutHistory.LibraryAsset)
        .Where(checkoutHistory => 
          checkoutHistory.LibraryCard.Id == GetLibraryCardId(patronId)
        )
        .OrderByDescending(checkoutHistory => checkoutHistory.CheckOut);

    public IEnumerable<Checkout> GetCheckouts(int patronId) => 
      _context.Checkouts 
        .Include(checkout => checkout.LibraryCard)
        .Include(checkout => checkout.LibraryAsset)
        .Where(checkout => 
          checkout.LibraryCard.Id == GetLibraryCardId(patronId));

    public IEnumerable<Hold> GetHolds(int patronId) =>
      _context.Holds
        .Include(hold => hold.LibraryAsset)
        .Include(hold => hold.LibraryCard)
        .Where(hold => hold.LibraryCard.Id == GetLibraryCardId(patronId));
  }
}
