using LibraryData;
using LibraryMvc.Models.Catalog.CheckoutModels;
using LibraryMvc.Models.Catalog.DetailModels;
using LibraryMvc.Models.Catalog.IndexModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryMvc.Controllers
{
  [Controller]
  [Route("/[controller]")]
  public class CatalogController : Controller
  {
    private readonly ILibraryAsset _libraryAssetService;
    private readonly ICheckout _checkoutService;

    public CatalogController(
      ILibraryAsset libraryAssetService,
      ICheckout checkoutService
    )
    {
      _libraryAssetService = libraryAssetService;
      _checkoutService = checkoutService;
    }

    [HttpGet]
    public IActionResult Index()
    {
      var assetModels = _libraryAssetService.GetAll();

      var listingResult = assetModels
        .Select(result => new AssetModel
        {
          AssetId = result.Id,
          ImageUrl = result.ImageUrl,
          AuthorOrDirector = _libraryAssetService.GetAuthorOrDirector(result.Id),
          DeweyCallNumber = _libraryAssetService.GetDeweyIndex(result.Id),
          Title = _libraryAssetService.GetTitle(result.Id),
          Type = _libraryAssetService.GetType(result.Id)
        });

      var model = new AssetListModel
      {
        AssetModels = listingResult
      };

      return View(model);
    }

    [HttpGet("Detail/{assetId:int}")]
    public ActionResult Detail(int assetId)
    {
      var asset = _libraryAssetService.GetById(assetId);

      var currentHolds = _checkoutService
        .GetCurrentHolds(assetId)
        .Select(_hold => _hold.Id)
        .Select(_holdId => new AssetHoldModel
        {
          HoldPlaced = _checkoutService.GetCurrentHoldPlaced(_holdId),
          PatronName = _checkoutService.GetCurrentHoldPatron(_holdId)
        });

      var model = new AssetDetailModel
      {
        AssetId = assetId,
        Title = asset.Title,
        Type = _libraryAssetService.GetType(assetId),
        Year = asset.Year,
        Cost = asset.Cost,
        Status = asset.Status.Name,
        ImageUrl = asset.ImageUrl,
        AuthorOrDirector = _libraryAssetService.GetAuthorOrDirector(assetId),
        CurrentLocation = _libraryAssetService.GetCurrentLocation(assetId).Name,
        DeweyCallNumber = _libraryAssetService.GetDeweyIndex(assetId),
        CheckoutHistory = _checkoutService.GetCheckoutHistory(assetId),
        ISBN = _libraryAssetService.GetIsbn(assetId),
        LastCheckout = _checkoutService.GetLatestCheckout(assetId),
        CurrentHolds = currentHolds,
        PatronName = _checkoutService.GetCurrentCheckoutPatron(assetId),
      };

      return View(model);
    }

    [HttpGet("CheckOut/{assetId:int}")]
    public ActionResult CheckOut(int assetId)
    {
      var asset = _libraryAssetService.GetById(assetId);

      var model = new CheckoutModel
      {
        AssetId = assetId,
        ImageUrl = asset.ImageUrl,
        Title = asset.Title,
        LibraryCardId = "",
        IsCheckedOut = _checkoutService.IsCheckedOut(assetId)
      };

      return View(model);
    }

    [HttpPost("CheckIn")]
    public ActionResult CheckIn(int assetId)
    {
      _checkoutService.CheckInItem(assetId);
      return RedirectToAction("Detail", "Catalog", new { assetId });
    }

    [HttpPost("MarkLost")]
    public ActionResult MarkLost(int assetId)
    {
      _checkoutService.MarkLost(assetId);
      return RedirectToAction("Detail", new { assetId });
    }

    [HttpPost("MarkFound")]
    public ActionResult MarkFound(int assetId)
    {
      _checkoutService.MarkFound(assetId);
      return RedirectToAction("Detail", new { assetId });
    }

    [HttpGet("Hold/{assetId:int}")]
    public ActionResult Hold(int assetId)
    {
      var asset = _libraryAssetService.GetById(assetId);

      var model = new CheckoutModel
      {
        AssetId = assetId,
        ImageUrl = asset.ImageUrl,
        Title = asset.Title,
        LibraryCardId = "",
        IsCheckedOut = _checkoutService.IsCheckedOut(assetId),
        HoldCount = _checkoutService.GetCurrentHolds(assetId).Count()
      };

      return View(model);
    }

    [HttpPost("PlaceCheckOut")]
    public ActionResult PlaceCheckout(int assetId, int libraryCardId)
    {
      _checkoutService.CheckOutItem(assetId, libraryCardId);
      return RedirectToAction("Detail", new { assetId });
    }

    [HttpPost("PlaceHold")]
    public ActionResult PlaceHold(int assetId, int libraryCardId)
    {
      _checkoutService.PlaceHold(assetId, libraryCardId);
      return RedirectToAction("Detail", new { assetId });
    }
  }
}
