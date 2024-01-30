using NecnatAbp.Br.GeDatasus.Samples;
using Xunit;

namespace NecnatAbp.Br.GeDatasus.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<GeDatasusMongoDbTestModule>
{

}
