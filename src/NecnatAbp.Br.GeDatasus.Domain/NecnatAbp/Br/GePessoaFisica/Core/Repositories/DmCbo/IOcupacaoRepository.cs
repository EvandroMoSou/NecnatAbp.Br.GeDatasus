using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GePessoaFisica.DmCbo
{
    public partial interface IOcupacaoRepository : IRepository<Ocupacao, Guid>
    {
        Task<Ocupacao?> GetByCodigoCboAsync(string codigoCbo);
        Task<List<Ocupacao>> SearchByFamiliaIdOptionalAndTituloContainsAsync(Guid? familiaId, string tituloContains);
        
    }
}
