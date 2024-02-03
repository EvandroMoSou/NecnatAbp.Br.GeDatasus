using Microsoft.Extensions.Configuration;
using NecnatAbp.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace NecnatAbp.Br.GeGeocodificacao.DmGoogleGeocoding
{
    public partial class GoogleGeocodingRepository : IGoogleGeocodingRepository
    {
        public bool? IsChangeTrackingEnabled => throw new NotImplementedException();

        private readonly IConfiguration Configuration;
        protected ICidadeMunicipioRepository CidadeMunicipioRepository { get; }
        protected IBairroDistritoRepository BairroDistritoRepository { get; }
        protected ILogradouroRepository LogradouroRepository { get; }
        protected IGeolocalizacaoRepository GeolocalizacaoRepository { get; }

        public GoogleGeocodingRepository(IConfiguration configuration,
            ICidadeMunicipioRepository cidadeMunicipioRepository,
            IBairroDistritoRepository bairroDistritoRepository,
            ILogradouroRepository logradouroRepository,
            IGeolocalizacaoRepository geolocalizacaoRepository)
        {
            Configuration = configuration;
            LogradouroRepository = logradouroRepository;
            CidadeMunicipioRepository = cidadeMunicipioRepository;
            BairroDistritoRepository = bairroDistritoRepository;
            GeolocalizacaoRepository = geolocalizacaoRepository;
        }

        public async Task<Logradouro?> GetLogradouroByCepAsync(string cep)
        {
            cep = cep.OnlyDigits().Trim();

            var targetUrl = $"https://maps.googleapis.com/maps/api/geocode/json" +
                $"?key={Configuration["Google:ApiKeyGeocoding"]}" +
                $"&components=postal_code:{cep}" +
                "&components=country:BR";

            var lGeolocalizacao = await CallGoogleGeocodingAsync(targetUrl);

            if (lGeolocalizacao.Count < 1)
                return null;

            return lGeolocalizacao.First().Logradouro;
        }

        public async Task<List<Logradouro>> SearchLogradouroByCidadeMunicipioIdAndNomeContainsAndNumeroAsync(Guid cidadeMunicipioId, string nomeContains, int? numero = null)
        {
            var cidadeMunicipio = await CidadeMunicipioRepository.GetAsync(cidadeMunicipioId);

            var targetUrl = $"https://maps.googleapis.com/maps/api/geocode/json" +
                $"?key={Configuration["Google:ApiKeyGeocoding"]}" +
                $"&address={HttpUtility.UrlEncode(nomeContains + ", " + (numero == null ? string.Empty : numero + ", ") + cidadeMunicipio.Nome)}";

            var lGeolocalizacao = await CallGoogleGeocodingAsync(targetUrl);
            return lGeolocalizacao.Select(x => x.Logradouro!).ToList();
        }

        public async Task<Geolocalizacao?> GetGeolocalizacaoByLogradouroAndNumeroAsync(Logradouro logradouro, int? numero)
        {
            var targetUrl = $"https://maps.googleapis.com/maps/api/geocode/json" +
                $"?key={Configuration["Google:ApiKeyGeocoding"]}" +
                $"&address={HttpUtility.UrlEncode(logradouro.Nome + (numero == null ? string.Empty : ", " + numero))}";

            var lGeolocalizacao = await CallGoogleGeocodingAsync(targetUrl);
            return lGeolocalizacao.Where(x => x.Logradouro!.Cep == logradouro.Cep && x.Numero == numero).FirstOrDefault();
        }

        private async Task<List<Geolocalizacao>> CallGoogleGeocodingAsync(string targetUrl)
        {
            try
            {
                using var client = new HttpClient();
                using var request = new HttpRequestMessage(HttpMethod.Get, targetUrl);
                using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                if (!response.IsSuccessStatusCode)
                    return new List<Geolocalizacao>();

                var googleGeocodeResponse = JsonConvert.DeserializeObject<GgResponse>(await response.Content.ReadAsStringAsync());

                if (googleGeocodeResponse?.Results == null || googleGeocodeResponse.Results.Count < 1)
                    return new List<Geolocalizacao>();

                var l = new List<Geolocalizacao>();
                foreach (var iResult in googleGeocodeResponse.Results)
                {
                    if (iResult.AddressComponents == null)
                        continue;

                    var country = iResult.AddressComponents.Where(x => x.Types!.Contains("country")).FirstOrDefault()?.ShortName;
                    if (country == null || country != "BR")
                        continue;

                    var dto = new Geolocalizacao();
                    dto.Logradouro = new Logradouro();
                    dto.Logradouro.CidadeMunicipio = new CidadeMunicipio();
                    dto.Logradouro.BairroDistrito = new BairroDistrito();

                    var cep = iResult.AddressComponents.Where(x => x.Types!.Contains("postal_code")).FirstOrDefault()?.LongName;
                    if (string.IsNullOrWhiteSpace(cep))
                        continue;
                    dto.Logradouro.Cep = int.Parse(cep.OnlyDigits());

                    var nome = iResult.AddressComponents.Where(x => x.Types!.Contains("route")).FirstOrDefault()?.LongName;
                    if (string.IsNullOrWhiteSpace(nome))
                        continue;
                    dto.Logradouro.Nome = nome;

                    dto.Logradouro.NomeAbreviado = iResult.AddressComponents.Where(x => x.Types!.Contains("route")).FirstOrDefault()?.ShortName;

                    var administrativeAreaLevel1 = iResult.AddressComponents.Where(x => x.Types!.Contains("administrative_area_level_1")).FirstOrDefault();
                    if (administrativeAreaLevel1 != null)
                        dto.Logradouro.UnidadeFederativa = (UnidadeFederativa)Enum.Parse(typeof(UnidadeFederativa), administrativeAreaLevel1.ShortName!);

                    var administrativeAreaLevel2 = iResult.AddressComponents.Where(x => x.Types!.Contains("administrative_area_level_2")).FirstOrDefault();
                    if (administrativeAreaLevel2 != null)
                        dto.Logradouro.CidadeMunicipio.Nome = administrativeAreaLevel2.LongName!;

                    var sublocalityLevel1 = iResult.AddressComponents.Where(x => x.Types!.Contains("sublocality_level_1")).FirstOrDefault();
                    if (sublocalityLevel1 != null)
                        dto.Logradouro.BairroDistrito.Nome = sublocalityLevel1.LongName!;

                    var streetNumber = iResult.AddressComponents.Where(x => x.Types!.Contains("street_number")).FirstOrDefault();
                    if (streetNumber != null && streetNumber.LongName != null)
                        if (streetNumber.LongName == streetNumber.LongName.OnlyDigits())
                            dto.Numero = int.Parse(streetNumber.LongName);
                        else
                            dto.Logradouro.Complemento = streetNumber.LongName;

                    var geometryLocation = iResult.Geometry?.Location;
                    if (geometryLocation != null)
                    {
                        dto.Latitude = geometryLocation.Latitude ?? 0;
                        dto.Longitude = geometryLocation.Longitude ?? 0;
                    }

                    l.Add(dto);
                }

                l = await StoreGoogleGeocodingAsync(l);

                return l;
            }
            catch
            {
                return new List<Geolocalizacao>();
            }
        }

        private async Task<List<Geolocalizacao>> StoreGoogleGeocodingAsync(List<Geolocalizacao> lGeolocalizacao)
        {
            var l = new List<Geolocalizacao>();
            foreach (var iGeolocalizacao in lGeolocalizacao)
            {
                if (iGeolocalizacao.Logradouro == null || iGeolocalizacao.Logradouro?.Cep == null)
                    continue;

                var logradouro = await LogradouroRepository.GetByCepAsync(iGeolocalizacao.Logradouro.Cep);
                if (logradouro == null)
                {
                    if (iGeolocalizacao.Logradouro?.Nome == null)
                        continue;

                    if (iGeolocalizacao.Logradouro.BairroDistrito?.Nome == null)
                        continue;

                    if (iGeolocalizacao.Logradouro.CidadeMunicipio?.Nome == null)
                        continue;

                    if (iGeolocalizacao.Logradouro.UnidadeFederativa == 0)
                        continue;

                    var insertLogradouro = new Logradouro();
                    insertLogradouro.Cep = iGeolocalizacao.Logradouro.Cep;
                    insertLogradouro.Nome = iGeolocalizacao.Logradouro.Nome;
                    insertLogradouro.NomeAbreviado = iGeolocalizacao.Logradouro.NomeAbreviado;
                    insertLogradouro.Complemento = iGeolocalizacao.Logradouro.Complemento;

                    var cidadeMunicipio = await CidadeMunicipioRepository.MaintainAsync(
                        new CidadeMunicipio
                        {
                            UnidadeFederativa = iGeolocalizacao.Logradouro.UnidadeFederativa,
                            Nome = iGeolocalizacao.Logradouro.CidadeMunicipio.Nome,
                            Origem = (int)OrigemGeocodificacao.Google,
                            InAtivo = true
                        }, true);
                    insertLogradouro.CidadeMunicipioId = cidadeMunicipio.Id;

                    var bairro = await BairroDistritoRepository.MaintainAsync(
                        new BairroDistrito
                        {
                            CidadeMunicipioId = cidadeMunicipio.Id,
                            Nome = iGeolocalizacao.Logradouro.BairroDistrito.Nome,
                            Origem = (int)OrigemGeocodificacao.Google,
                            InAtivo = true
                        }, true);
                    insertLogradouro.BairroDistritoId = bairro.Id;

                    insertLogradouro.UnidadeFederativa = iGeolocalizacao.Logradouro.UnidadeFederativa;
                    insertLogradouro.Origem = (int)OrigemGeocodificacao.Google;

                    iGeolocalizacao.Logradouro = await LogradouroRepository.InsertAsync(insertLogradouro, true);
                }
                else
                    iGeolocalizacao.Logradouro = logradouro;

                if (iGeolocalizacao.Latitude != 0 && iGeolocalizacao.Longitude != 0)
                    l.Add(await GeolocalizacaoRepository.MaintainAsync(
                        new Geolocalizacao
                        {
                            LogradouroId = iGeolocalizacao.Logradouro.Id,
                            Numero = iGeolocalizacao.Numero,
                            Latitude = iGeolocalizacao.Latitude,
                            Longitude = iGeolocalizacao.Longitude,
                            Origem = (int)OrigemGeocodificacao.Google,
                            InAtivo = true
                        }, true));
                else
                    l.Add(iGeolocalizacao);
            }

            return l;
        }
    }
}
