using NecnatAbp.Br.GePessoaFisica;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NecnatAbp.Br.GeDatasus.Core.Converters
{
    public interface ICadSusConverter
    {
        Task<List<PessoaFisicaDto>> ConvertAsync(string source);
    }
}
