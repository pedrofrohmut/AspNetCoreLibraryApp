using LibraryData;
using LibraryMvc.Models.Catalog.Detail;
using LibraryMvc.Models.Catalog.Index;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryMvc.Controllers
{
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

    public IActionResult Index()
    {
      var assetModels = _libraryAssetService.GetAll();

      var listingResult = assetModels
        .Select(result => new AssetModel
        {
          Id = result.Id,
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

    // GET Catalog/Detail/{id}
    //[HttpGet("{assetId:int}")]
    public ActionResult Detail(int id)
    {
      var asset = _libraryAssetService.GetById(id);

      var currentHolds = _checkoutService
        .GetCurrentHolds(id)
        .Select(_hold => _hold.Id)
        .Select(_holdId => new AssetHoldModel
        {
          HoldPlaced = _checkoutService.GetCurrentHoldPlaced(_holdId),
          PatronName = _checkoutService.GetCurrentHoldPatron(_holdId)
        });

      var model = new AssetDetailModel
      {
        AssetId = id,
        Title = asset.Title,
        Type = _libraryAssetService.GetType(id),
        Year = asset.Year,
        Cost = asset.Cost,
        Status = asset.Status.Name,
        ImageUrl = asset.ImageUrl,
        AuthorOrDirector = _libraryAssetService.GetAuthorOrDirector(id),
        CurrentLocation = _libraryAssetService.GetCurrentLocation(id).Name,
        DeweyCallNumber = _libraryAssetService.GetDeweyIndex(id),
        CheckoutHistory = _checkoutService.GetCheckoutHistory(id),
        ISBN = _libraryAssetService.GetIsbn(id),
        LastCheckout = _checkoutService.GetLatestCheckout(id),
        CurrentHolds = currentHolds,
        PatronName = _checkoutService.GetCurrentCheckoutPatron(id),
      };

      return View(model);
    }

    public ActionResult Checkout(int id)
    {

    }
  }
}
