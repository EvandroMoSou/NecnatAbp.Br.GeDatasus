using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace NecnatAbp.Br.GeDatasus;

[DependsOn(
    typeof(GeDatasusApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class GeDatasusHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(GeDatasusApplicationContractsModule).Assembly,
            GeDatasusRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<GeDatasusHttpApiClientModule>();
        });

    }
}
