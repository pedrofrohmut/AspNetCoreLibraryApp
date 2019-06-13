using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryData.Models
{
  public class LibraryCard
  {
    public int Id { get; set; }

    [Column(TypeName = "DECIMAL(18,2)")]
    public decimal Fees { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual IEnumerable<Checkout> Checkouts { get; set; }
  }
}
