using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace NecnatAbp.Br.GeDatasus.EntityFrameworkCore;

[ConnectionStringName(GeDatasusDbProperties.ConnectionStringName)]
public interface IGeDatasusDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
