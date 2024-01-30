using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace NecnatAbp.Br.GeDatasus;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class GeDatasusInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<GeDatasusInstallerModule>();
        });
    }
}
