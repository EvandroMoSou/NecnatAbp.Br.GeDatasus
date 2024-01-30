using Volo.Abp.Modularity;

namespace NecnatAbp.Br.GeDatasus;

[DependsOn(
    typeof(GeDatasusDomainModule),
    typeof(GeDatasusTestBaseModule)
)]
public class GeDatasusDomainTestModule : AbpModule
{

}
