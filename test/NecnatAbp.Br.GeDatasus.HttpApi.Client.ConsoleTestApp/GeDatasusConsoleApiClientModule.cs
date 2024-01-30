using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace NecnatAbp.Br.GeDatasus;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(GeDatasusHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class GeDatasusConsoleApiClientModule : AbpModule
{

}
