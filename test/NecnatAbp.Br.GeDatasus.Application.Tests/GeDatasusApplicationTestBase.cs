using Volo.Abp.Modularity;

namespace NecnatAbp.Br.GeDatasus;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class GeDatasusApplicationTestBase<TStartupModule> : GeDatasusTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
