using LibraryData.Models;
using System.Collections.Generic;

namespace LibraryData
{
  public interface ILibraryBranch
  {
    // Create.
    void Add(LibraryBranch newBranch);

    // Read.
    IEnumerable<LibraryBranch> GetAll();
    LibraryBranch Get(int branchId);
    IEnumerable<LibraryAsset> GetAssets(int branchId);
    IEnumerable<Patron> GetPatrons(int branchId);
    IEnumerable<BranchHour> GetBranchHours(int branchId);
    IEnumerable<string> GetHumanizeBranchHours(int branchId);
    bool IsBranchOpen(int branchId);
  }
}
