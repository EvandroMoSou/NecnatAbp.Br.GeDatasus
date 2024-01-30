using Volo.Abp.Modularity;

namespace NecnatAbp.Br.GeDatasus;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class GeDatasusDomainTestBase<TStartupModule> : GeDatasusTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
