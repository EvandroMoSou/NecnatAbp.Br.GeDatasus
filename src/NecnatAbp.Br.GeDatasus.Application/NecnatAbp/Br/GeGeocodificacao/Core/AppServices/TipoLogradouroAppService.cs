using NecnatAbp.AppServices;
using NecnatAbp.Br.GeGeocodificacao.Permissions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class TipoLogradouroAppService :
        CrudsAppService<
            TipoLogradouro,
            TipoLogradouroDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateTipoLogradouroDto,
            TipoLogradouroResultRequestDto>,
        ITipoLogradouroAppService
    {
        public TipoLogradouroAppService(ITipoLogradouroRepository repository) : base(repository)
        {
            GetPolicyName = GeGeocodificacaoPermissions.TiposLogradouro.Default;
            GetListPolicyName = GeGeocodificacaoPermissions.TiposLogradouro.Default;
            CreatePolicyName = GeGeocodificacaoPermissions.TiposLogradouro.Create;
            UpdatePolicyName = GeGeocodificacaoPermissions.TiposLogradouro.Update;
            DeletePolicyName = GeGeocodificacaoPermissions.TiposLogradouro.Delete;
        }

        /// <summary>
        /// O filtro GenericSearch apresenta a seguinte linha de acao:
        ///     - 4+ caracteres alfanumericos: Realiza a busca conjuta pelo NomeContains.
        /// </summary>
        protected override async Task<IQueryable<TipoLogradouro>> CreateFilteredQuerySearchAsync(TipoLogradouroResultRequestDto input)
        {
            var q = await ReadOnlyRepository.GetQueryableAsync();

            if (input.Sigla != null)
                q = q.Where(x => x.Sigla == input.Sigla);

            if (!string.IsNullOrWhiteSpace(input.NomeContains))
            {
                if (input.NomeContains.Length < 4)
                    throw new UserFriendlyException("O filtro NomeContains deve conter no mínimo 4 caracteres.");

                q = q.Where(x => x.Nome.Contains(input.NomeContains));
            }

            if (input.InAtivo != null)
                q = q.Where(x => x.InAtivo == input.InAtivo);

            return q;
        }
    }
}
