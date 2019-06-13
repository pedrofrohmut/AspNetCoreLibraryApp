using LibraryData;
using LibraryMvc.Models.Catalog.Index;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMvc.Controllers
{
  public class CatalogController : Controller
  {
    private readonly ILibraryAsset _service;

    public CatalogController(ILibraryAsset assetService)
    {
      _service = assetService;
    }

    public IActionResult Index()
    {
      var assetModels = _service.GetAll();

      var listingResult = assetModels
        .Select(result => new AssetModel 
        {
          Id = result.Id,
          ImageUrl =  result.ImageUrl,
          AuthorOrDirector = _service.GetAuthorOrDirector(result.Id),
          DeweyCallNumber = _service.GetDeweyIndex(result.Id),
          Title = _service.GetTitle(result.Id),
          Type = _service.GetType(result.Id)
        });

      var model = new AssetListModel
      {
        AssetModels = listingResult
      };

      return View(model);
    }
  }
}
