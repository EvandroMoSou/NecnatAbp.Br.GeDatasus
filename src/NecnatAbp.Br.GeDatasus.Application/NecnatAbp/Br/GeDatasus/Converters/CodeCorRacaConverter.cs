using System;

namespace NecnatAbp.Br.GeDatasus.Converters
{
    public class CodeCorRacaConverter : ICorRacaConverter
    {
        public GePessoaFisica.CorRaca FromCodigoCadSus(string codigoDataSus)
        {
            if (string.IsNullOrWhiteSpace(codigoDataSus))
                throw new NotImplementedException();

            switch (codigoDataSus)
            {
                case "01":
                    return GePessoaFisica.CorRaca.Branca;
                case "02":
                    return GePessoaFisica.CorRaca.Preta;
                case "03":
                    return GePessoaFisica.CorRaca.Parda;
                case "04":
                    return GePessoaFisica.CorRaca.Amararela;
                case "05":
                    return GePessoaFisica.CorRaca.Indigena;
                case "99":
                    return GePessoaFisica.CorRaca.NaoInformado;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
