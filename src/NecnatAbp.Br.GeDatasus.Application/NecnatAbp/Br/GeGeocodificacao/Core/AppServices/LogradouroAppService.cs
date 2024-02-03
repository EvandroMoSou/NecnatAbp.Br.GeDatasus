using NecnatAbp.AppServices;
using NecnatAbp.Br.GeGeocodificacao.DmGoogleGeocoding;
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
    public partial class LogradouroAppService :
        CrudsAppService<
            Logradouro,
            LogradouroDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateLogradouroDto,
            LogradouroResultRequestDto>,
        ILogradouroAppService
    {
        protected ILogradouroRepository TypedRepository { get; }
        protected IGoogleGeocodingRepository GoogleGeocodingRepository { get; }

        public LogradouroAppService(ILogradouroRepository repository, IGoogleGeocodingRepository googleGeocodingRepository) : base(repository)
        {
            TypedRepository = repository;
            GoogleGeocodingRepository = googleGeocodingRepository;

            GetPolicyName = GeGeocodificacaoPermissions.Logradouros.Default;
            GetListPolicyName = GeGeocodificacaoPermissions.Logradouros.Default;
            CreatePolicyName = GeGeocodificacaoPermissions.Logradouros.Create;
            UpdatePolicyName = GeGeocodificacaoPermissions.Logradouros.Update;
            DeletePolicyName = GeGeocodificacaoPermissions.Logradouros.Delete;
        }

        /// <summary>
        /// O filtro GenericSearch apresenta a seguinte linha de acao:
        ///     - 4+ caracteres alfanumericos: Realiza a busca conjuta pelo NomeContains.
        ///     - 8 caracteres numericos: Realiza a busca pelo Cep.
        /// </summary>
        protected override async Task<IQueryable<Logradouro>> CreateFilteredQuerySearchAsync(LogradouroResultRequestDto input)
        {
            var q = await ReadOnlyRepository.GetQueryableAsync();

            if (!string.IsNullOrWhiteSpace(input.GenericSearch))
            {
                if (input.GenericSearch.All(x => char.IsDigit(x) || x == '-'))
                    input.Cep = input.GenericSearch.OnlyDigits();
                else
                    input.NomeContains = input.GenericSearch;
            }

            if (!string.IsNullOrWhiteSpace(input.Cep))
            {
                input.Cep = input.Cep.OnlyDigits();
                if (input.Cep.Length != LogradouroConsts.MaxCepLength)
                    throw new UserFriendlyException($"O filtro Cep deve conter {LogradouroConsts.MaxCepLength} caracteres numéricos.");

                q = q.Where(x => x.Cep == int.Parse(input.Cep));
            }
            else if (!string.IsNullOrWhiteSpace(input.NomeContains))
            {
                input.NomeContains = TratarNomeLogradouro(input.NomeContains);

                if (input.CidadeMunicipioId == null || input.CidadeMunicipioId == Guid.Empty)
                    throw new UserFriendlyException("O filtro CidadeMunicipioId é obrigatório para essa pesquisa.");

                if (input.NomeContains.Length < 4)
                    throw new UserFriendlyException("O filtro NomeContains deve conter no mínimo 4 caracteres.");

                q = q.Where(x => x.Nome.Contains(input.NomeContains));
            }
            else
                throw new UserFriendlyException("O filtro GenericSearch, Cep ou NomeContains é obrigatório.");

            if (input.UnidadeFederativa == null || (int)input.UnidadeFederativa == 0)
                q = q.Where(x => x.UnidadeFederativa == input.UnidadeFederativa);

            if (input.CidadeMunicipioId != null && input.CidadeMunicipioId != Guid.Empty)
                q = q.Where(x => x.CidadeMunicipioId == input.CidadeMunicipioId);

            if (input.BairroDistritoId != null && input.BairroDistritoId != Guid.Empty)
                q = q.Where(x => x.BairroDistritoId == input.BairroDistritoId);

            if (input.InAtivo != null)
                q = q.Where(x => x.InAtivo == input.InAtivo);

            return q;
        }

        public async Task<List<LogradouroDto>> SearchFallbackGoogleAsync(LogradouroFallbackResultRequestDto input)
        {
            if (input.GenericSearch == null || input.GenericSearch.Length < 3)
                throw new UserFriendlyException("O filtro GenericSearch deve conter no mínimo 3 caracteres.");

            if (input.GenericSearch.All(x => char.IsDigit(x) || x == '-'))
            {
                if (input.GenericSearch.OnlyDigits().Length == 8)
                {
                    var e = await TypedRepository.GetByCepAsync(int.Parse(input.GenericSearch.OnlyDigits()));
                    if (e != null)
                        return new List<LogradouroDto> { MapToGetListOutputDto(e) };
                    else
                    {
                        e = await GoogleGeocodingRepository.GetLogradouroByCepAsync(input.GenericSearch.OnlyDigits());
                        if (e != null)
                            return new List<LogradouroDto> { MapToGetListOutputDto(e) };
                        else
                            return new List<LogradouroDto>();
                    }
                }
                else
                    throw new UserFriendlyException($"Caso seja numérico, o filtro GenericSearch deve conter {LogradouroConsts.MaxCepLength} caracteres numéricos.");
            }

            if (input.CidadeMunicipioId == null || input.CidadeMunicipioId == Guid.Empty)
                throw new UserFriendlyException("Caso seja alfanumérico, O filtro CidadeMunicipioId é obrigatório para essa pesquisa.");

            var genericSearchTratado = TratarNomeLogradouro(input.GenericSearch);

            var lLogradouro = await TypedRepository.SearchByCidadeMunicipioIdAndNomeContainsAsync((Guid)input.CidadeMunicipioId, genericSearchTratado);

            if (lLogradouro.Count() < input.ActiveFallbackCount)
            {
                var lGoogle = await GoogleGeocodingRepository.SearchLogradouroByCidadeMunicipioIdAndNomeContainsAndNumeroAsync((Guid)input.CidadeMunicipioId, input.GenericSearch, input.Numero);
                foreach (var iGoogle in lGoogle)
                    if (!lLogradouro.Any(x => x.Cep == iGoogle.Cep))
                        lLogradouro.Add(iGoogle);
            }

            var l = new List<LogradouroDto>();
            foreach (var iLogradouro in lLogradouro)
                l.Add(MapToGetListOutputDto(iLogradouro));

            return l;
        }

        private string TratarNomeLogradouro(string nomeLogradouro)
        {
            nomeLogradouro = nomeLogradouro.ToUpper().RemoveAccents().OnlyLettersOrSpace().RemoveDuplicateSpaces().Trim();

            foreach (var iLogradouroTipoAbreviatura in LogradouroConsts.DictionaryLogradouroTipoAbreviatura)
                if (nomeLogradouro.StartsWith(iLogradouroTipoAbreviatura.Key + " "))
                    nomeLogradouro = nomeLogradouro.ReplaceFirst(iLogradouroTipoAbreviatura.Key, iLogradouroTipoAbreviatura.Value);

            return nomeLogradouro;
        }
    }
}
