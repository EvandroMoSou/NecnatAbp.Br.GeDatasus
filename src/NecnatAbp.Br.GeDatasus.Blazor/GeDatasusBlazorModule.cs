using Microsoft.Extensions.DependencyInjection;
using NecnatAbp.Br.GeDatasus.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace NecnatAbp.Br.GeDatasus.Blazor;

[DependsOn(
    typeof(GeDatasusApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule)
    )]
public class GeDatasusBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<GeDatasusBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<GeDatasusBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new GeDatasusMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(GeDatasusBlazorModule).Assembly);
        });
    }
}
