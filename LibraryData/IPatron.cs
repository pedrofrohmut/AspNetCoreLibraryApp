using LibraryData.Models;
using System.Collections.Generic;

namespace LibraryData
{
  public interface IPatron
  {
    // Create
    void Add(Patron newPatron);

    // Read
    Patron Get(int patronId);
    IEnumerable<Patron> GetAll();
    IEnumerable<CheckoutHistory> GetCheckoutHistories(int patronId);
    IEnumerable<Hold> GetHolds(int patronId);
    IEnumerable<Checkout> GetCheckouts(int patronId);

    // Update

    // Delete
  }
}
