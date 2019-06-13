using LibraryData.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryData
{
  public class LibraryDbContext : DbContext
  {
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
      : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }

    public DbSet<BranchHour> BranchHours { get; set; }

    public DbSet<Checkout> Checkouts { get; set; }

    public DbSet<CheckoutHistory> CheckoutHistories { get; set; }

    public DbSet<Hold> Holds { get; set; }

    public DbSet<LibraryBranch> Librarybranches { get; set; }

    public DbSet<LibraryCard> LibraryCards { get; set; }

    public DbSet<Patron> Patrons { get; set; }

    public DbSet<Status> Statuses { get; set; }

    public DbSet<Video> Videos { get; set; }
  } 
}
