using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace NecnatAbp.Br.GeDatasus;

[DependsOn(
    typeof(GeDatasusDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class GeDatasusApplicationContractsModule : AbpModule
{

}
