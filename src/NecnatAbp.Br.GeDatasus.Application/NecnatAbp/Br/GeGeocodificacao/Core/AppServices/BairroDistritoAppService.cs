using NecnatAbp.AppServices;
using NecnatAbp.Br.GeGeocodificacao.Permissions;
using NecnatAbp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class BairroDistritoAppService :
        CrudsAppService<
            BairroDistrito,
            BairroDistritoDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateBairroDistritoDto,
            BairroDistritoResultRequestDto>,
        IBairroDistritoAppService
    {
        protected IBairroDistritoRepository TypedRepository { get; }
        protected ISubdistritoRepository SubdistritoRepository { get; }

        public BairroDistritoAppService(IBairroDistritoRepository repository, ISubdistritoRepository subdistritoRepository) : base(repository)
        {
            TypedRepository = repository;
            SubdistritoRepository = subdistritoRepository;

            GetPolicyName = GeGeocodificacaoPermissions.BairrosDistritos.Default;
            GetListPolicyName = GeGeocodificacaoPermissions.BairrosDistritos.Default;
            CreatePolicyName = GeGeocodificacaoPermissions.BairrosDistritos.Create;
            UpdatePolicyName = GeGeocodificacaoPermissions.BairrosDistritos.Update;
            DeletePolicyName = GeGeocodificacaoPermissions.BairrosDistritos.Delete;
        }

        /// <summary>
        /// O filtro GenericSearch apresenta a seguinte linha de acao:
        ///     - 4+ caracteres alfanumericos: Realiza a busca conjuta pelo NomeContains.
        ///     - 9 caracteres numericos: Realiza a busca pelo CodigoIbge.
        /// </summary>
        protected override async Task<IQueryable<BairroDistrito>> CreateFilteredQuerySearchAsync(BairroDistritoResultRequestDto input)
        {
            var q = await ReadOnlyRepository.GetQueryableAsync();

            if (!string.IsNullOrWhiteSpace(input.GenericSearch))
            {
                if (input.GenericSearch.All(char.IsDigit))
                    input.CodigoIbge = input.GenericSearch;
                else
                    input.NomeContains = input.GenericSearch;
            }

            if (!string.IsNullOrWhiteSpace(input.CodigoIbge))
            {
                input.CodigoIbge = input.CodigoIbge.OnlyDigits();
                if (input.CodigoIbge.Length != BairroDistritoConsts.MaxCodigoIbgeLength)
                    throw new UserFriendlyException($"O filtro CodigoIbge deve conter {BairroDistritoConsts.MaxCodigoIbgeLength} caracteres numéricos.");

                q = q.Where(x => x.CodigoIbge != null && x.CodigoIbge == input.CodigoIbge);
            }

            if (!string.IsNullOrWhiteSpace(input.NomeContains))
            {
                if (input.CidadeMunicipioId == null || input.CidadeMunicipioId == Guid.Empty)
                    throw new UserFriendlyException("O filtro CidadeMunicipioId é obrigatório para essa pesquisa.");

                if (input.NomeContains.Length < 4)
                    throw new UserFriendlyException("O filtro NomeContains deve conter no mínimo 4 caracteres.");

                q = q.Where(x => x.CidadeMunicipioId == input.CidadeMunicipioId && x.Nome.Contains(input.NomeContains));
            }

            if (input.InAtivo != null)
                q = q.Where(x => x.InAtivo == input.InAtivo);

            return q;
        }

        public async Task<List<BairroDistritoDto>> SearchFallbackSubdistritoAsync(BairroDistritoFallbackResultRequestDto input)
        {
            if (input.GenericSearch == null || input.GenericSearch.Length < 4)
                throw new UserFriendlyException("O filtro GenericSearch deve conter no mínimo 4 caracteres.");

            if (input.GenericSearch.All(char.IsDigit))
            {
                if (input.GenericSearch.Length == 9)
                {
                    var e = await TypedRepository.GetByCodigoIbgeAsync(input.GenericSearch);
                    if (e == null)
                        return new List<BairroDistritoDto>();
                    else
                        return new List<BairroDistritoDto> { MapToGetListOutputDto(e) };
                }
                else if (input.GenericSearch.Length == 11)
                {
                    var e = await SubdistritoRepository.GetByCodigoIbgeWithBairroDistritoAsync(input.GenericSearch);
                    if (e == null)
                        return new List<BairroDistritoDto>();
                    else
                        return new List<BairroDistritoDto> { MapToGetListOutputDto(e) };
                }
                else
                    throw new UserFriendlyException("Caso seja numérico, o filtro GenericSearch deve conter 9 ou 11 caracteres numéricos.");
            }

            if (input.CidadeMunicipioId == null || input.CidadeMunicipioId == Guid.Empty)
                throw new UserFriendlyException("Caso seja alfanumérico, O filtro CidadeMunicipioId é obrigatório para essa pesquisa.");

            var l = new List<BairroDistritoDto>();
            var lBairroDistrito = await TypedRepository.SearchByCidadeMunicipioIdAndNomeContainsAsync((Guid)input.CidadeMunicipioId, input.GenericSearch);
            foreach (var iBairroDistrito in lBairroDistrito)
                l.Add(MapToGetListOutputDto(iBairroDistrito));

            if (l.Count() > input.ActiveFallbackCount)
                return l;

            var lSubdistrito = await SubdistritoRepository.SearchByCidadeMunicipioIdAndNomeContainsWithBairroDistritoAsync((Guid)input.CidadeMunicipioId, input.GenericSearch);
            foreach (var iSubdistrito in lSubdistrito)
                l.Add(MapToGetListOutputDto(iSubdistrito));

            return l;
        }

        protected BairroDistritoDto MapToGetListOutputDto(Subdistrito entity)
        {
            var dto = MapToGetListOutputDto(entity.BairroDistrito!);
            dto.Subdistrito = ObjectMapper.Map<Subdistrito, SubdistritoDto>(entity);

            return dto;
        }
    }
}
