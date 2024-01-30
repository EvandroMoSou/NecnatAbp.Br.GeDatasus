using Volo.Abp;
using Volo.Abp.MongoDB;

namespace NecnatAbp.Br.GeDatasus.MongoDB;

public static class GeDatasusMongoDbContextExtensions
{
    public static void ConfigureGeDatasus(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
