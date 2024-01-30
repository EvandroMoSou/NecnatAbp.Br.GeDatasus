using NecnatAbp.Br.GeDatasus.MongoDB;
using NecnatAbp.Br.GeDatasus.Samples;
using Xunit;

namespace NecnatAbp.Br.GeDatasus.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<GeDatasusMongoDbTestModule>
{

}
