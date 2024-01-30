using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace NecnatAbp.Br.GeDatasus.EntityFrameworkCore;

[ConnectionStringName(GeDatasusDbProperties.ConnectionStringName)]
public class GeDatasusDbContext : AbpDbContext<GeDatasusDbContext>, IGeDatasusDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public GeDatasusDbContext(DbContextOptions<GeDatasusDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureGeDatasus();
    }
}
