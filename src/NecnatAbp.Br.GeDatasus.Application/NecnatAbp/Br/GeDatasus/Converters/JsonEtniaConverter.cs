using System;
using System.Linq;

namespace NecnatAbp.Br.GeDatasus.Converters
{
    public class JsonEtniaConverter : IEtniaConverter
    {
        const string _embeddedResourcePath = "NecnatAbp.Br.GeDatasus.DataSeedContributors.Etnia.json";

        public GePessoaFisica.EtniaDto FromCodigoCadSus(string codigoCadSus)
        {
            if (string.IsNullOrWhiteSpace(codigoCadSus))
                throw new NotImplementedException();

            var list = EmbeddedResourceUtil.ToListOfAsync<Etnia>(typeof(GeDatasusDomainModule), _embeddedResourcePath);
            
            var e = list.Where(x => x.CodigoCadSus == codigoCadSus).FirstOrDefault();
            if (e == null)
                throw new NotImplementedException();

            return new GePessoaFisica.EtniaDto { Nome = e.Nome };
        }
    }
}