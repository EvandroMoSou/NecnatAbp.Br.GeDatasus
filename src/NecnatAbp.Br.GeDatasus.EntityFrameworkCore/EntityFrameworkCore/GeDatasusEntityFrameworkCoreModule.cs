using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace NecnatAbp.Br.GeDatasus.EntityFrameworkCore;

[DependsOn(
    typeof(GeDatasusDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class GeDatasusEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<GeDatasusDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
        });
    }
}
