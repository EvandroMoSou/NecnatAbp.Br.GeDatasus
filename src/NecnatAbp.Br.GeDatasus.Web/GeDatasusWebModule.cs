using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using NecnatAbp.Br.GeDatasus.Localization;
using NecnatAbp.Br.GeDatasus.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using NecnatAbp.Br.GeDatasus.Permissions;

namespace NecnatAbp.Br.GeDatasus.Web;

[DependsOn(
    typeof(GeDatasusApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
    )]
public class GeDatasusWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(GeDatasusResource), typeof(GeDatasusWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(GeDatasusWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new GeDatasusMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<GeDatasusWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<GeDatasusWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<GeDatasusWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
                //Configure authorization.
            });
    }
}
