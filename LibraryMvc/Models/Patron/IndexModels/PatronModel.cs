using LibraryData.Models;
using System;
using System.Collections.Generic;

namespace LibraryMvc.Models.Patron.IndexModels
{
  public class PatronModel
  {
    public int PatronId { get; set; }
    public int LibraryCardId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => FirstName + " " + LastName;
    public string Address { get; set; }
    public string Telephone { get; set; }
    public string HomeLibraryBranch { get; set; }

    public decimal OverdueFees { get; set; }

    public DateTime MemberSince { get; set; }

    public IEnumerable<Checkout> AssetsCheckedOut { get; set; }
    public IEnumerable<CheckoutHistory> CheckoutHistories { get; set; }
    public IEnumerable<Hold> Holds { get; set; }
  }
}
