using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace NecnatAbp.Br.GeDatasus.EntityFrameworkCore;

public class GeDatasusHttpApiHostMigrationsDbContext : AbpDbContext<GeDatasusHttpApiHostMigrationsDbContext>
{
    public GeDatasusHttpApiHostMigrationsDbContext(DbContextOptions<GeDatasusHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureGeDatasus();
    }
}
