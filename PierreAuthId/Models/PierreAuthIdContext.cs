using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PierreAuthId.Models
{
  public class PierreAuthIdContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Treats> Treats { get; set; }
    public DbSet<Flavors> Flavors { get; set; }
    public DbSet<TreatsFlavors> TreatsFlavors { get; set; }

    public PierreAuthIdContext(DbContextOptions options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}