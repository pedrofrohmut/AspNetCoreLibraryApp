using System.Collections.Generic;

namespace LibraryMvc.Models.Branch.IndexModels
{
  public class BranchModel
  {
    public int BranchId { get; set; }
    public string Address { get; set; }
    public string Name { get; set; }
    public string OpenDate { get; set; }
    public string TelephoneNumber { get; set; }
    public bool IsOpen { get; set; }
    public string Description { get; set; }
    public int NumberOfPatrons { get; set; }
    public int NumberOfAssets { get; set; }
    public decimal TotalAssetvalue { get; set; }
    public string ImageUrl { get; set; }
    public IEnumerable<string> HoursOpen { get; set; }
  }
}
