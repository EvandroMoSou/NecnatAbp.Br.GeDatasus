using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace NecnatAbp.Br.GeDatasus.MongoDB;

[ConnectionStringName(GeDatasusDbProperties.ConnectionStringName)]
public class GeDatasusMongoDbContext : AbpMongoDbContext, IGeDatasusMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureGeDatasus();
    }
}
