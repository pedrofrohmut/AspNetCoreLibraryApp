using LibraryData;
using LibraryMvc.Models.Branch.DetailModels;
using LibraryMvc.Models.Branch.IndexModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMvc.Controllers
{
  public class BranchController : Controller
  {
    private readonly ILibraryBranch _libraryBranchService;

    public BranchController(ILibraryBranch branchService)
    {
       _libraryBranchService = branchService;
    }

    [HttpGet]
    public ActionResult Index()
    {
      var branches = new BranchListModel
      {
        Branches = _libraryBranchService.GetAll()
          .Select(branch => new BranchModel
          {
            BranchId = branch.Id,
            Name = branch.Name,
            IsOpen = _libraryBranchService.IsBranchOpen(branch.Id),
            NumberOfAssets = _libraryBranchService.GetAssets(branch.Id).Count(),
            NumberOfPatrons = _libraryBranchService.GetPatrons(branch.Id).Count(),
          })
      };

      return View(branches);
    }

    [HttpGet("Detail/{branchId}")]
    public ActionResult Detail(int branchId)
    {
      var branch = _libraryBranchService.Get(branchId);

      var model = new BranchDetailModel
      {
        BranchId = branch.Id,
        Name = branch.Name,
        Address = branch.Address,
        TelephoneNumber = branch.Telephone,
        OpenDate = branch.OpenDate.ToString("dd-MM-yyyy"),
        NumberOfAssets = _libraryBranchService.GetAssets(branchId).Count(),
        NumberOfPatrons = _libraryBranchService.GetPatrons(branchId).Count(),
        TotalAssetValue = _libraryBranchService.GetAssets(branchId).Sum(asset => asset.Cost),
        ImageUrl = branch.ImageUrl,
        HoursOpen = _libraryBranchService.GetHumanizeBranchHours(branchId),
      };

      return View(model);
    }
  }
}
