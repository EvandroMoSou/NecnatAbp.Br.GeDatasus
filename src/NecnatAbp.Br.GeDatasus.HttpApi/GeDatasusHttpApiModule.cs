using Localization.Resources.AbpUi;
using NecnatAbp.Br.GeDatasus.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace NecnatAbp.Br.GeDatasus;

[DependsOn(
    typeof(GeDatasusApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class GeDatasusHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(GeDatasusHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<GeDatasusResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
