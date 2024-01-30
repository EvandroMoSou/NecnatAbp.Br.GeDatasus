using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace NecnatAbp.Br.GeDatasus.MongoDB;

[DependsOn(
    typeof(GeDatasusDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class GeDatasusMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<GeDatasusMongoDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
        });
    }
}
