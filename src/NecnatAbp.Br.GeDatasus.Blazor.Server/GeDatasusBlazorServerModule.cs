using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace NecnatAbp.Br.GeDatasus.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(GeDatasusBlazorModule)
    )]
public class GeDatasusBlazorServerModule : AbpModule
{

}
