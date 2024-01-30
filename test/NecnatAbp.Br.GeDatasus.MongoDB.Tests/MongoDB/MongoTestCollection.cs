using Xunit;

namespace NecnatAbp.Br.GeDatasus.MongoDB;

[CollectionDefinition(Name)]
public class MongoTestCollection : ICollectionFixture<MongoDbFixture>
{
    public const string Name = "MongoDB Collection";
}
