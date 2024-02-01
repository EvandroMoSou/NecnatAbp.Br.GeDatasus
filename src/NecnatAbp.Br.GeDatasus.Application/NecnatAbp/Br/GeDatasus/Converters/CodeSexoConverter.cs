using System;

namespace NecnatAbp.Br.GeDatasus.Converters
{
    public class CodeSexoConverter : ISexoConverter
    {
        public GePessoaFisica.Sexo FromCodigoCadSusDoc(string codigoDataSusDoc)
        {
            if (string.IsNullOrWhiteSpace(codigoDataSusDoc))
                throw new NotImplementedException();

            switch (codigoDataSusDoc)
            {
                case "M":
                    return GePessoaFisica.Sexo.Masculino;
                case "F":
                    return GePessoaFisica.Sexo.Feminino;
                case "UN":
                    return GePessoaFisica.Sexo.NaoInformado;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
