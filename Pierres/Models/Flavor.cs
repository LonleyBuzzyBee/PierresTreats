using System.Collections.Generic;

namespace Pierres.Models
{
  public class Flavor
  {
    public Flavor()
    {
      this.Treats = new HashSet<TreatFlavor>();
    }
    public int FlavorId { get; set; }
    public string Type { get; set; }
    // public virtual ApplicationUser User{ get; set; } //identity
    public ICollection<TreatFlavor> Treats { get; }

  }
}