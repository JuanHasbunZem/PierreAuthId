using System.Collections.Generic;

namespace PierreAuthId.Models
{
  public class Treats
  {
    public Treats()
    {
      this.JoinEntities = new HashSet<TreatsFlavors>();
    }

    public int TreatsId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<TreatsFlavors> JoinEntities { get; set; }
  }
}