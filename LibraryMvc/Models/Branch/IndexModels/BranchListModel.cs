using System.Collections.Generic;

namespace LibraryMvc.Models.Branch.IndexModels
{
  public class BranchListModel
  {
    public IEnumerable<BranchModel> Branches { get; set; }
  }
}
