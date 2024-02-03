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
    public partial class GeolocalizacaoAppService :
        CrudsAppService<
            Geolocalizacao,
            GeolocalizacaoDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateGeolocalizacaoDto,
            GeolocalizacaoResultRequestDto>,
        IGeolocalizacaoAppService
    {
        protected IGeolocalizacaoRepository TypedRepository { get; }
        protected ILogradouroRepository LogradouroRepository { get; }
        protected IGoogleGeocodingRepository GoogleGeocodingRepository { get; }

        public GeolocalizacaoAppService(IGeolocalizacaoRepository repository, ILogradouroRepository logradouroRepository, IGoogleGeocodingRepository googleGeocodingRepository) : base(repository)
        {
            TypedRepository = repository;
            LogradouroRepository = logradouroRepository;
            GoogleGeocodingRepository = googleGeocodingRepository;

            GetPolicyName = GeGeocodificacaoPermissions.Geolocalizacoes.Default;
            GetListPolicyName = GeGeocodificacaoPermissions.Geolocalizacoes.Default;
            CreatePolicyName = GeGeocodificacaoPermissions.Geolocalizacoes.Create;
            UpdatePolicyName = GeGeocodificacaoPermissions.Geolocalizacoes.Update;
            DeletePolicyName = GeGeocodificacaoPermissions.Geolocalizacoes.Delete;
        }

        protected override async Task<IQueryable<Geolocalizacao>> CreateFilteredQuerySearchAsync(GeolocalizacaoResultRequestDto input)
        {
            var q = await ReadOnlyRepository.GetQueryableAsync();

            if (string.IsNullOrWhiteSpace(input.Cep))
                throw new UserFriendlyException("O filtro Cep é obrigatório para essa pesquisa.");

            input.Cep = input.Cep.OnlyDigits();
            if (input.Cep.Length != LogradouroConsts.MaxCepLength)
                throw new UserFriendlyException($"O filtro Cep deve conter {LogradouroConsts.MaxCepLength} caracteres numéricos.");

            q = q.Where(x => x.Logradouro!.Cep == int.Parse(input.Cep.OnlyDigits()));
            q = q.Where(x => x.Numero == input.Numero);

            if (input.InAtivo != null)
                q = q.Where(x => x.InAtivo == input.InAtivo);

            return q;
        }

        public async Task<GeolocalizacaoDto?> GetByCepAndNumeroFallbackGoogleAsync(GeolocalizacaoFallbackResultRequestDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Cep))
                throw new UserFriendlyException("O filtro Cep é obrigatório para essa pesquisa.");

            input.Cep = input.Cep.OnlyDigits();
            if (input.Cep.Length != LogradouroConsts.MaxCepLength)
                throw new UserFriendlyException($"O filtro Cep deve conter {LogradouroConsts.MaxCepLength} caracteres numéricos.");

            var l = new List<GeolocalizacaoDto>();
            var geolocalizacao = await TypedRepository.GetByCepAndNumeroWithLogradouroAsync(int.Parse(input.Cep), input.Numero);

            if (geolocalizacao == null)
            {
                var logradouro = await LogradouroRepository.GetByCepAsync(int.Parse(input.Cep));
                if (logradouro == null)
                    logradouro = await GoogleGeocodingRepository.GetLogradouroByCepAsync(input.Cep);
                if (logradouro == null)
                    return null;

                geolocalizacao = await GoogleGeocodingRepository.GetGeolocalizacaoByLogradouroAndNumeroAsync(logradouro, input.Numero);
            }

            if (geolocalizacao == null)
                return null;

            return MapToGetListOutputDto(geolocalizacao);
        }

        protected override GeolocalizacaoDto MapToGetListOutputDto(Geolocalizacao entity)
        {
            var dto = base.MapToGetListOutputDto(entity);
            if (entity.Logradouro != null)
                dto.Logradouro = ObjectMapper.Map<Logradouro, LogradouroDto>(entity.Logradouro);

            return dto;
        }
    }
}
