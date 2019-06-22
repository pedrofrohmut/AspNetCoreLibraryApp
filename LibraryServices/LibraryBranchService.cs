using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
  public class LibraryBranchService : ILibraryBranch
  {
    private readonly LibraryDbContext _context;

    public LibraryBranchService(LibraryDbContext context)
    {
      _context = context;
    }

    public void Add(LibraryBranch newBranch)
    {
      _context.Add(newBranch);
      _context.SaveChanges();
    }

    public IEnumerable<LibraryBranch> GetAll() => 
      _context.LibraryBranches
        .Include(branch => branch.Patrons)
        .Include(branch => branch.LibraryAssets);

    public LibraryBranch Get(int branchId) => 
      GetAll().FirstOrDefault(branch => branch.Id == branchId);

    public IEnumerable<LibraryAsset> GetAssets(int branchId) => 
      _context.LibraryBranches
        .Include(branch => branch.LibraryAssets)
        .FirstOrDefault(branch => branch.Id == branchId)
        .LibraryAssets;

    public IEnumerable<BranchHour> GetBranchHours(int branchId) =>
      _context.BranchHours
        .Include(branchHour => branchHour.LibraryBranch)
        .Where(branchHour => branchHour.LibraryBranch.Id == branchId);

    public IEnumerable<string> GetHumanizeBranchHours(int branchId) =>
      DataHelpers.HumanizeBusinessHours(GetBranchHours(branchId));

    public IEnumerable<Patron> GetPatrons(int branchId) => 
      _context.LibraryBranches
        .Include(branch => branch.Patrons)
        .FirstOrDefault(branch => branch.Id ==  branchId)
        .Patrons;

    public bool IsBranchOpen(int branchId)
    {
      var currentTimeHour = DateTime.Now.Hour;
      // Little Hacky for fixing the database data (+ 1)
      var currentDayOfWeek = (int) DateTime.Now.DayOfWeek + 1;
      var hours = 
        _context.BranchHours
          .Include(branchHour => branchHour.LibraryBranch)
          .Where(branchHour => branchHour.LibraryBranch.Id == branchId);
      var daysHours = hours.FirstOrDefault(hour => hour.DayOfWeek == currentDayOfWeek);
      return currentTimeHour > daysHours.OpenTime && currentTimeHour < daysHours.CloseTime;
    }
  }
}
