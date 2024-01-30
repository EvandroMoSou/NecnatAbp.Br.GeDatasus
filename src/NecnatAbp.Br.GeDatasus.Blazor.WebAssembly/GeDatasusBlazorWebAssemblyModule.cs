using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace NecnatAbp.Br.GeDatasus.Blazor.WebAssembly;

[DependsOn(
    typeof(GeDatasusBlazorModule),
    typeof(GeDatasusHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class GeDatasusBlazorWebAssemblyModule : AbpModule
{

}
