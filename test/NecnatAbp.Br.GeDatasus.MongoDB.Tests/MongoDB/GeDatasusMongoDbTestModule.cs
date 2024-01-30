using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace NecnatAbp.Br.GeDatasus.MongoDB;

[DependsOn(
    typeof(GeDatasusApplicationTestModule),
    typeof(GeDatasusMongoDbModule)
)]
public class GeDatasusMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
