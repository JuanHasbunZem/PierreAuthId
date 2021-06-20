using System.Collections.Generic;

namespace PierreAuthId.Models
{
  public class Flavors
  {
    public Flavors()
    {
      this.JoinEntities = new HashSet<TreatsFlavors>();
    }

    public int FlavorsId { get; set; }
    public string Type { get; set; }
    public virtual ICollection<TreatsFlavors> JoinEntities { get;}
  }
}