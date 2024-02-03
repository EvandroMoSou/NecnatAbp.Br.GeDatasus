using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface ILogradouroRepository : IRepository<Logradouro, Guid>
    {
        Task<Logradouro?> GetByCepAsync(int cep);
        Task<List<Logradouro>> SearchByCidadeMunicipioIdAndNomeContainsAsync(Guid cidadeMunicipioId, string nomeContains);
    }
}
