using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace NecnatAbp.Br.GeDatasus;

[DependsOn(
    typeof(GeDatasusDomainModule),
    typeof(GeDatasusApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class GeDatasusApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<GeDatasusApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<GeDatasusApplicationModule>(validate: true);
        });
    }
}
