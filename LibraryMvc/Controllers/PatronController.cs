using LibraryData;
using LibraryData.Models;
using LibraryMvc.Models.Patron.DetailModels;
using LibraryMvc.Models.Patron.IndexModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LibraryMvc.Controllers
{
  [Controller]
  [Route("/[controller]")]
  public class PatronController : Controller
  {
    private readonly IPatron _patronService;

    public PatronController(IPatron patronService)
    {
      _patronService = patronService;
    }

    [HttpGet]
    public ActionResult Index()
    {
      var patrons = _patronService.GetAll();

      var patronList = new PatronListModel
      {
        PatronsList = patrons.Select(patron =>
          new PatronModel
          {
            PatronId = patron.Id,
            FirstName = patron.FirstName,
            LastName = patron.LastName,
            LibraryCardId = patron.LibraryCard.Id,
            OverdueFees = patron.LibraryCard.Fees,
            HomeLibraryBranch = patron.HomeLibrarybranch.Name,
          }
        ).ToList()
      };

      return View(patronList);
    }

    [HttpGet("Detail/{patronId:int}")]
    public ActionResult Detail(int patronId)
    {
      var patron = _patronService.Get(patronId);

      var patronDetail = new PatronDetailModel
      {
        PatronId = patronId,
        FirstName = patron.FirstName,
        LastName = patron.LastName,
        Address = patron.Address,
        HomeLibraryBranch = patron.HomeLibrarybranch.Name,
        MemberSince = patron.LibraryCard.CreatedAt,
        OverdueFees = patron.LibraryCard.Fees,
        LibraryCardId = patron.LibraryCard.Id,
        Telephone = patron.TelephoneNumber,
        AssetsCheckedOut = _patronService.GetCheckouts(patronId).ToList() 
          ?? new List<Checkout>(),
        CheckoutHistories = _patronService.GetCheckoutHistories(patronId).ToList() 
          ?? new List<CheckoutHistory>(),
        Holds = _patronService.GetHolds(patronId)
      };

      return View(patronDetail);
    }
  }
}
