using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PierreAuthId.Models
{
   public class PierreAuthIdFactory : IDesignTimeDbContextFactory<PierreAuthIdContext>
  {

    PierreAuthIdContext IDesignTimeDbContextFactory<PierreAuthIdContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<PierreAuthIdContext>();

      builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));

      return new PierreAuthIdContext(builder.Options);
    }
  }
}