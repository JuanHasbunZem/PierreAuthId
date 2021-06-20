namespace PierreAuthId.Models
{
  public class TreatsFlavors
  {       
    public int TreatsFlavorsId { get; set; }
    public int FlavorsId { get; set; }
    public int TreatsId { get; set; }
    public virtual Flavors Flavors { get; set; }
    public virtual Treats Treats { get; set; }
  }
}