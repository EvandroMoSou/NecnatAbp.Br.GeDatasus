using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NecnatAbp.Br.GeDatasus.EntityFrameworkCore;

public class GeDatasusHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<GeDatasusHttpApiHostMigrationsDbContext>
{
    public GeDatasusHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<GeDatasusHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("GeDatasus"));

        return new GeDatasusHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
