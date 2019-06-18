using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMvc.Models.Catalog.CheckoutModels
{
  public class CheckoutModel
  {
    public string LibraryCardId { get; set; }
    public string Title { get; set; }
    public int AssetId { get; set; }
    public string ImageUrl { get; set; }
    public int HoldCount { get; set; }
    public bool IsCheckedOut { get; set; }
  }
}
