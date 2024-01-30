using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace NecnatAbp.Br.GeDatasus;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(GeDatasusDomainSharedModule)
)]
public class GeDatasusDomainModule : AbpModule
{

}
