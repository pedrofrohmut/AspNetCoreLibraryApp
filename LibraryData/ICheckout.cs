using LibraryData.Models;
using System;
using System.Collections.Generic;

namespace LibraryData
{
  public interface ICheckout
  {
    // Create.
    void Add(Checkout newCheckout);
    void PlaceHold(int assetId, int libraryCardId);

    // Read.
    IEnumerable<Checkout> GetAll();
    IEnumerable<CheckoutHistory> GetCheckoutHistory(int assetId);
    IEnumerable<Hold> GetCurrentHolds(int assetId);

    Checkout GetLatestCheckout(int assetId);
    Checkout GetById(int checkoutId);
    string GetCurrentCheckoutPatron(int assetId);
    string GetCurrentHoldPatron(int holdId);
    string GetCurrentHoldPlaced(int holdId);

    bool IsCheckedOut(int assetId);
    
    // Update.
    void CheckOutItem(int assetId, int libraryCardId);
    void CheckInItem(int assetId);
    void MarkLost(int assetId);
    void MarkFound(int assetId);

    // Delete.

    //int GetNumberOfCopies(int id);
    //string GetCurrentPatron(int id);
  }
}
