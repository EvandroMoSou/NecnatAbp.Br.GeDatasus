using Volo.Abp.Modularity;

namespace NecnatAbp.Br.GeDatasus;

[DependsOn(
    typeof(GeDatasusApplicationModule),
    typeof(GeDatasusDomainTestModule)
    )]
public class GeDatasusApplicationTestModule : AbpModule
{

}
