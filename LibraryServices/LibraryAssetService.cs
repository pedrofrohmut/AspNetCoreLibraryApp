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
  public class LibraryAssetService : ILibraryAsset
  {
    private readonly LibraryDbContext _context;

    public LibraryAssetService(LibraryDbContext context)
    {
      _context = context;
    }

    public void Add(LibraryAsset newAsset)
    {
      _context.Add(newAsset);
      _context.SaveChanges();
    }

    public IEnumerable<LibraryAsset> GetAll() =>
      _context.LibraryAssets
        .Include(asset => asset.Status)
        .Include(asset => asset.Location);

    public LibraryAsset GetById(int id) =>
      _context.LibraryAssets
        .Include(asset => asset.Status)
        .Include(asset => asset.Location)
        .FirstOrDefault(asset => asset.Id == id);

    public LibraryBranch GetCurrentLocation(int id) =>
      _context.LibraryAssets
        .FirstOrDefault(asset => asset.Id == id)
        .Location;

    public string GetDeweyIndex(int id) =>
      _context.Books.Any(book => book.Id == id)
        ? _context.Books.FirstOrDefault(book => book.Id == id).DeweyIndex
        : "";

    public string GetIsbn(int id) =>
      _context.Books.Any(book => book.Id == id)
        ? _context.Books.FirstOrDefault(book => book.Id == id).ISBN
        : "";

    public string GetTitle(int id) =>
      _context.LibraryAssets
        .FirstOrDefault(asset => asset.Id == id)
        .Title;

    public string GetType(int id) => 
      IsBook(id) ? "Book" : IsVideo(id) ? "Video" : "";

    private bool IsBook(int id) => 
      _context.LibraryAssets
        .OfType<Book>()
        .Where(asset => asset.Id == id)
        .Any();

    private bool IsVideo(int id) => 
      _context.LibraryAssets
        .OfType<Video>()
        .Where(asset => asset.Id == id)
        .Any();

    public string GetAuthorOrDirector(int id) =>
      IsBook(id)
        ? _context.Books.FirstOrDefault(book => book.Id == id).Author
        : _context.Videos.FirstOrDefault(video => video.Id == id).Director
        ?? "Unknow";
  }
}
