using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace NecnatAbp.Br.GeDatasus.MongoDB;

[ConnectionStringName(GeDatasusDbProperties.ConnectionStringName)]
public interface IGeDatasusMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
