using System.Collections.Generic;

namespace LibraryMvc.Models.Patron.IndexModels
{
  public class PatronListModel
  {
    public IEnumerable<PatronModel> PatronsList { get; set; }
  }
}
