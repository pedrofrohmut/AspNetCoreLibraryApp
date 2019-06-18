namespace LibraryMvc.Models.Catalog.IndexModels
{
  public class AssetModel
  {
    public int AssetId { get; set; }
    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string AuthorOrDirector { get; set; }
    public string Type { get; set; }
    public string DeweyCallNumber { get; set; }
    public int NumberOfCopies { get; set; }
  }
}
