using LibraryData.Models;
using System;
using System.Collections.Generic;

namespace LibraryData
{
  public interface ICheckout
  {
    IEnumerable<Checkout> GetAll();
    IEnumerable<CheckoutHistory> GetCheckoutHistory(int assetId);
    IEnumerable<Hold> GetCurrentHolds(int assetId);

    Checkout GetLatestCheckout(int assetId);
    Checkout GetById(int checkoutId);
    string GetCurrentCheckoutPatron(int assetId);
    string GetCurrentHoldPatron(int holdId);
    string GetCurrentHoldPlaced(int holdId);
    
    void Add(Checkout newCheckout);

    void CheckOutItem(int assetId, int libraryCardId);
    void CheckInItem(int assetId, int libraryCardId);
    void PlaceHold(int assetId, int libraryCardId);
    void MarkLost(int assetId);
    void MarkFound(int assetId);
    
    //int GetNumberOfCopies(int id);
    //bool IsCheckedOut(int id);
    //string GetCurrentPatron(int id);
  }
}
