using System;
using System.Linq;

namespace NecnatAbp.Br.GeDatasus.Converters
{
    public class JsonPaisConverter : IPaisConverter
    {
        const string _embeddedResourcePath = "NecnatAbp.Br.GeDatasus.DataSeedContributors.Pais.json";

        public GeGeocodificacao.PaisDto FromCodigoCadSus(string codigoCadSus)
        {
            if (string.IsNullOrWhiteSpace(codigoCadSus))
                throw new NotImplementedException();

            var list = EmbeddedResourceUtil.ToListOfAsync<Pais>(typeof(GeDatasusDomainModule), _embeddedResourcePath);

            var e = list.Where(x => x.CodigoCadSus == codigoCadSus).FirstOrDefault();
            if (e == null)
                throw new NotImplementedException();

            return new GeGeocodificacao.PaisDto { Nome = e.Nome };
        }
    }
}
