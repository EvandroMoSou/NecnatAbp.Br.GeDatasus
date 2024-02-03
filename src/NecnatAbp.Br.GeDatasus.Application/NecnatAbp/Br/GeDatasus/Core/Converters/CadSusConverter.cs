using Microsoft.Extensions.DependencyInjection;
using NecnatAbp.Br.GeDatasus.Core.Converters;
using NecnatAbp.Br.GeGeocodificacao;
using NecnatAbp.Br.GePessoaFisica;
using NecnatAbp.Br.GePessoaFisica.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace NecnatAbp.Br.GeDatasus
{
    public class CadSusConverter : ICadSusConverter
    {
        public IAbpLazyServiceProvider LazyServiceProvider { get; set; } = default!;

        protected Type? ObjectMapperContext { get; set; }
        protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetService<IObjectMapper>(provider =>
            ObjectMapperContext == null
                ? provider.GetRequiredService<IObjectMapper>()
                : (IObjectMapper)provider.GetRequiredService(typeof(IObjectMapper<>).MakeGenericType(ObjectMapperContext)));

        private readonly IBuscaFonetica _buscaFonetica;
        private readonly IBairroDistritoRepository _bairroDistritoRepository;
        private readonly ICidadeMunicipioRepository _cidadeMunicipioRepository;
        private readonly IEtniaRepository _etniaRepository;
        private readonly IOrgaoEmissorRepository _orgaoEmissorRepository;
        private readonly IPaisRepository _paisRepository;
        private readonly IPortariaNaturalizacaoRepository _portariaNaturalizacaoRepository;
        private readonly ITipoLogradouroRepository _tipoLogradouroRepository;

        public CadSusConverter(IBuscaFonetica buscaFonetica,
            IBairroDistritoRepository bairroDistritoRepository,
            ICidadeMunicipioRepository cidadeMunicipioRepository,
            IEtniaRepository etniaRepository,
            IOrgaoEmissorRepository orgaoEmissorRepository,
            IPaisRepository paisRepository,
            IPortariaNaturalizacaoRepository portariaNaturalizacaoRepository,
            ITipoLogradouroRepository tipoLogradouroRepository)
        {
            _buscaFonetica = buscaFonetica;
            _bairroDistritoRepository = bairroDistritoRepository;
            _cidadeMunicipioRepository = cidadeMunicipioRepository;
            _etniaRepository = etniaRepository;
            _orgaoEmissorRepository = orgaoEmissorRepository;
            _paisRepository = paisRepository;
            _portariaNaturalizacaoRepository = portariaNaturalizacaoRepository;
            _tipoLogradouroRepository = tipoLogradouroRepository;
        }

        public async Task<List<PessoaFisicaDto>> ConvertAsync(string source)
        {
            var responseXml = new XmlDocument();
            responseXml.LoadXml(source);

            var xnList = responseXml.GetElementsByTagName("patientPerson");

            if (xnList.Count < 1)
                return new List<PessoaFisicaDto>();

            var lDestination = new List<PessoaFisicaDto>();
            foreach (XmlNode xn in xnList)
            {
                var destination = new PessoaFisicaDto();

                foreach (XmlNode iChildNodes in xn.ChildNodes)
                {
                    if (iChildNodes.Name == "addr")
                        destination = await TratarAddrAsync(destination, iChildNodes);

                    else if (iChildNodes.Name == "administrativeGenderCode")
                        destination = TratarAdministrativeGenderCode(destination, iChildNodes);

                    else if (iChildNodes.Name == "asCitizen")
                        destination = TratarAsCitizen(destination, iChildNodes);

                    else if (iChildNodes.Name == "asOtherIDs")
                        destination = await TratarAsOtherIDsAsync(destination, iChildNodes);

                    else if (iChildNodes.Name == "birthPlace")
                        destination = await TratarBirthPlaceAsync(destination, iChildNodes);

                    else if (iChildNodes.Name == "birthTime")
                        destination = TratarBirthTime(destination, iChildNodes);

                    else if (iChildNodes.Name == "deceasedInd")
                        destination = TratarDeceasedInd(destination, iChildNodes);

                    else if (iChildNodes.Name == "ethnicGroupCode")
                        destination = await TratarEthnicGroupCodeAsync(destination, iChildNodes);

                    else if (iChildNodes.Name == "name")
                        destination = TratarName(destination, iChildNodes);

                    else if (iChildNodes.Name == "personalRelationship")
                        destination = TratarPersonalRelationship(destination, iChildNodes);

                    else if (iChildNodes.Name == "raceCode")
                        destination = TratarRaceCode(destination, iChildNodes);

                    else if (iChildNodes.Name == "telecom")
                        destination = TratarTelecom(destination, iChildNodes);
                }

                lDestination.Add(destination);
            }

            return lDestination;
        }

        private async Task<PessoaFisicaDto> TratarAddrAsync(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            if (pessoaFisica.ListaPessoaFisicaEndereco == null)
                pessoaFisica.ListaPessoaFisicaEndereco = new List<PessoaFisicaEnderecoDto>();

            var pessoaFisicaEndereco = new PessoaFisicaEnderecoDto();
            pessoaFisicaEndereco.Geolocalizacao = new GeolocalizacaoDto();
            pessoaFisicaEndereco.Geolocalizacao.Logradouro = new LogradouroDto();

            var city = xmlNode["city"];
            if (city != null)
            {
                var cidadeMunicipio = await _cidadeMunicipioRepository.GetByCodigoIbgeAsync(city.InnerText);
                if (cidadeMunicipio == null)
                    throw new NotImplementedException();
                pessoaFisicaEndereco.Geolocalizacao.Logradouro.CidadeMunicipio = ObjectMapper.Map<CidadeMunicipio, CidadeMunicipioDto>(cidadeMunicipio);
            }

            var state = xmlNode["state"];
            if (state != null)
            {
                if (!string.IsNullOrEmpty(state.InnerText) && state.InnerText != "XX")
                    pessoaFisicaEndereco.Geolocalizacao.Logradouro.UnidadeFederativa = ConverterUnidadeFederativa(state.InnerText);
            }

            var postalCode = xmlNode["postalCode"];
            if (postalCode != null)
            {
                pessoaFisicaEndereco.Geolocalizacao.Logradouro.Cep = postalCode.InnerText;
            }

            var country = xmlNode["country"];
            if (country != null)
            {
                var pais = await _paisRepository.GetByCodigoCadSusAsync(country.InnerText);
                if (pais == null)
                    throw new NotImplementedException();
                pessoaFisicaEndereco.Geolocalizacao.Logradouro.Pais = ObjectMapper.Map<Pais, PaisDto>(pais);
            }

            var houseNumber = xmlNode["houseNumber"];
            if (houseNumber != null && houseNumber.InnerText != "S/N")
                pessoaFisicaEndereco.Geolocalizacao.Numero = int.Parse(houseNumber.InnerText);

            var streetName = xmlNode["streetName"];
            if (streetName != null)
                pessoaFisicaEndereco.Geolocalizacao.Logradouro.Nome = streetName.InnerText;

            var streetNameType = xmlNode["streetNameType"];
            if (streetNameType != null)
            {
                var tipoLogradouro = await _tipoLogradouroRepository.GetByCodigoCadSusAsync(streetNameType.InnerText);
                if (tipoLogradouro == null)
                    throw new NotImplementedException();
                pessoaFisicaEndereco.Geolocalizacao.Logradouro.TipoLogradouro = ObjectMapper.Map<TipoLogradouro, TipoLogradouroDto>(tipoLogradouro);
            }

            var additionalLocator = xmlNode["additionalLocator"];
            if (additionalLocator != null)
            {
                var bairroDistrito = await _bairroDistritoRepository.GetByCodigoIbgeAsync(additionalLocator.InnerText);
                if (bairroDistrito == null)
                    throw new NotImplementedException();
                pessoaFisicaEndereco.Geolocalizacao.Logradouro.BairroDistrito = ObjectMapper.Map<BairroDistrito, BairroDistritoDto>(bairroDistrito);
            }

            var unitID = xmlNode["unitID"];
            if (unitID != null)
                pessoaFisicaEndereco.Complemento = unitID.InnerText;

            pessoaFisica.ListaPessoaFisicaEndereco.Add(pessoaFisicaEndereco);

            return pessoaFisica;
        }

        private PessoaFisicaDto TratarAdministrativeGenderCode(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            if (string.IsNullOrEmpty(xmlNode.Attributes?["code"]?.Value))
                return pessoaFisica;

            pessoaFisica.Sexo = ConverterSexo(xmlNode.Attributes!["code"]!.Value);
            return pessoaFisica;
        }

        private PessoaFisicaDto TratarAsCitizen(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            if (pessoaFisica.ListaPassaporte == null)
                pessoaFisica.ListaPassaporte = new List<PassaporteDto>();

            var passaporte = new PassaporteDto();
            var idAsCitizen = xmlNode["id"];
            if (idAsCitizen != null)
            {
                var extensionAttribute = idAsCitizen.Attributes["extension"];
                if (extensionAttribute != null)
                {
                    passaporte.Numero = extensionAttribute.Value;
                }
            }

            var effectiveTimeAsCitizen = xmlNode["effectiveTime"];
            if (effectiveTimeAsCitizen != null)
            {
                var extensionAttribute = effectiveTimeAsCitizen.Attributes["value"];
                if (extensionAttribute != null)
                {
                    passaporte.DataEmissao = ParaDateTime(extensionAttribute.Value);
                }
            }

            var highAsCitizen = xmlNode["high"];
            if (highAsCitizen != null)
            {
                var extensionAttribute = highAsCitizen.Attributes["value"];
                if (extensionAttribute != null)
                {
                    passaporte.DataValidade = ParaDateTime(extensionAttribute.Value);
                }
            }

            if (!string.IsNullOrWhiteSpace(passaporte.Numero))
                pessoaFisica.ListaPassaporte.Add(passaporte);

            return pessoaFisica;
        }

        private async Task<PessoaFisicaDto> TratarAsOtherIDsAsync(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            var lChildNodes = xmlNode.ChildNodes;
            foreach (XmlNode iChildNode in lChildNodes)
            {
                if (iChildNode.Attributes?["root"] != null)
                {
                    var root = iChildNode.Attributes["root"]?.Value;
                    if (string.IsNullOrWhiteSpace(root))
                        continue;

                    //CNS
                    if (root == "2.16.840.1.113883.13.236")
                    {
                        pessoaFisica.ListaCns = ConverterCns(pessoaFisica.ListaCns, lChildNodes);
                        continue;
                    }
                    //CPF
                    else if (root == "2.16.840.1.113883.13.237")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            pessoaFisica.Cpf = extensionAttribute.Value;
                        continue;
                    }
                    //Nacionalidade
                    else if (root.StartsWith("2.16.840.1.113883.4.713"))
                    {
                        pessoaFisica = await ConverterNacionalidadeAsync(pessoaFisica, lChildNodes);
                        continue;
                    }
                    //RG
                    else if (root == "2.16.840.1.113883.13.243")
                    {
                        pessoaFisica.ListaRg = await ConverterRgAsync(pessoaFisica.ListaRg, lChildNodes);
                        continue;
                    }
                    //CTPS
                    else if (root == "2.16.840.1.113883.13.244")
                    {
                        pessoaFisica.ListaCtps = ConverterCtps(pessoaFisica.ListaCtps, lChildNodes);
                        continue;
                    }
                    //CNH
                    else if (root == "2.16.840.1.113883.13.238")
                    {
                        pessoaFisica.ListaCnh = ConverterCnh(pessoaFisica.ListaCnh, lChildNodes);
                        continue;
                    }
                    //Título de Eleitor
                    else if (root == "2.16.840.1.113883.13.239")
                    {
                        pessoaFisica.ListaTituloEleitor = ConverterTituloEleitor(pessoaFisica.ListaTituloEleitor, lChildNodes);
                        continue;
                    }
                    //NIS
                    else if (root == "2.16.840.1.113883.13.240")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                        {
                            if (pessoaFisica.ListaPisPasepNisNit == null)
                                pessoaFisica.ListaPisPasepNisNit = new List<PisPasepNisNitDto>();

                            pessoaFisica.ListaPisPasepNisNit.Add(new PisPasepNisNitDto { Numero = extensionAttribute.Value });
                        }
                        continue;
                    }
                    //RIC
                    else if (root == "2.16.840.1.113883.3.3024")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                        {
                            if (pessoaFisica.ListaRic == null)
                                pessoaFisica.ListaRic = new List<RicDto>();

                            pessoaFisica.ListaRic.Add(new RicDto { Numero = extensionAttribute.Value });
                        }
                        continue;
                    }
                    //DNV
                    else if (root == "2.16.840.1.113883.13.242")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                        {
                            if (pessoaFisica.ListaDnv == null)
                                pessoaFisica.ListaDnv = new List<DnvDto>();

                            pessoaFisica.ListaDnv.Add(new DnvDto { Numero = extensionAttribute.Value });
                        }
                        continue;
                    }
                    //Certidoes
                    else if (root.StartsWith("2.16.840.1.113883.4.706"))
                    {
                        pessoaFisica.ListaCertidao = ConverterCertidao(pessoaFisica.ListaCertidao, lChildNodes);
                        continue;
                    }
                }
            }

            return pessoaFisica;
        }

        private async Task<PessoaFisicaDto> TratarBirthPlaceAsync(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            var addrBirthPlace = xmlNode["addr"];
            if (addrBirthPlace != null)
            {
                var countryBirthPlace = addrBirthPlace["country"];
                if (countryBirthPlace != null)
                {
                    var pais = await _paisRepository.GetByCodigoCadSusAsync(countryBirthPlace.InnerText);
                    if (pais == null)
                        throw new NotImplementedException();
                    pessoaFisica.NacionalidadeIdPais = ObjectMapper.Map<Pais, PaisDto>(pais);
                    if (countryBirthPlace.InnerText.Equals("010"))
                    {
                        pessoaFisica.TipoNacionalidade = GePessoaFisica.TipoNacionalidade.Brasileiro;
                        var cityBirthPlace = addrBirthPlace["city"];
                        if (cityBirthPlace != null)
                        {
                            var cidadeMunicipio = await _cidadeMunicipioRepository.GetByCodigoIbgeAsync(cityBirthPlace.InnerText);
                            if (cidadeMunicipio == null)
                                throw new NotImplementedException();
                            pessoaFisica.NaturalidadeIdCidadeMunicipio = ObjectMapper.Map<CidadeMunicipio, CidadeMunicipioDto>(cidadeMunicipio);
                        }
                    }
                    else if (pessoaFisica.TipoNacionalidade == null)
                        pessoaFisica.TipoNacionalidade = GePessoaFisica.TipoNacionalidade.Estrangeiro;
                }
            }

            return pessoaFisica;
        }

        private PessoaFisicaDto TratarBirthTime(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            pessoaFisica.DataNascimento = ParaDateTime(xmlNode.Attributes?["value"]?.Value);

            return pessoaFisica;
        }

        private PessoaFisicaDto TratarDeceasedInd(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            var valueAttribute = xmlNode.Attributes?["value"];
            if (valueAttribute != null)
                pessoaFisica.InObito = valueAttribute.Value == "True" || valueAttribute.Value == "true";

            var deceasedTime = xmlNode["deceasedTime"];
            if (deceasedTime != null)
            {
                pessoaFisica.DataObito = ParaDateTime(deceasedTime.InnerText);
                pessoaFisica.InObito = true;
            }

            return pessoaFisica;
        }

        private async Task<PessoaFisicaDto> TratarEthnicGroupCodeAsync(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            if (string.IsNullOrEmpty(xmlNode.Attributes?["code"]?.Value))
                return pessoaFisica;

            var etnia = await _etniaRepository.GetByCodigoCadSusAsync(xmlNode.Attributes!["code"]!.Value);
            if (etnia == null)
                throw new NotImplementedException();
            pessoaFisica.Etnia = ObjectMapper.Map<Etnia, EtniaDto>(etnia);
            return pessoaFisica;
        }

        private PessoaFisicaDto TratarName(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            var use = xmlNode.Attributes?["use"]?.Value;

            if (use == "L")
            {
                var given = xmlNode["given"];
                if (given != null)
                {
                    pessoaFisica.Nome = given.InnerText;
                    pessoaFisica.NomeFonetico = _buscaFonetica.Fonetizar(pessoaFisica.Nome);
                }
            }
            else if (use == "ASGN")
            {
                var given = xmlNode["given"];
                if (given != null)
                {
                    pessoaFisica.NomeSocial = given.InnerText;
                    pessoaFisica.NomeSocialFonetico = _buscaFonetica.Fonetizar(pessoaFisica.NomeSocial);
                }
            }

            return pessoaFisica;
        }

        private PessoaFisicaDto TratarPersonalRelationship(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            var code = xmlNode["code"]?.GetAttribute("code");

            if (code == "PRN")
            {
                pessoaFisica.NomeMae = xmlNode["relationshipHolder1"]?["name"]?["given"]?.InnerText;
                pessoaFisica.NomeMaeFonetico = _buscaFonetica.Fonetizar(pessoaFisica.NomeMae);
            }
            else if (code == "NPRN")
            {
                pessoaFisica.NomePai = xmlNode["relationshipHolder1"]?["name"]?["given"]?.InnerText;
                pessoaFisica.NomePaiFonetico = _buscaFonetica.Fonetizar(pessoaFisica.NomePai);
            }

            return pessoaFisica;
        }

        private PessoaFisicaDto TratarRaceCode(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            if (string.IsNullOrEmpty(xmlNode.Attributes?["code"]?.Value))
                return pessoaFisica;

            pessoaFisica.CorRaca = ConverterCorRaca(xmlNode.Attributes!["code"]!.Value);
            return pessoaFisica;
        }

        private PessoaFisicaDto TratarTelecom(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            var use = xmlNode.Attributes?["use"]?.Value;

            if (use == "NET")
            {
                var valueAttribute = xmlNode.Attributes?["value"];
                if (valueAttribute != null)
                {
                    if (pessoaFisica.ListaPessoaFisicaEmail == null)
                        pessoaFisica.ListaPessoaFisicaEmail = new List<PessoaFisicaEmailDto>();

                    pessoaFisica.ListaPessoaFisicaEmail.Add(new PessoaFisicaEmailDto { TipoPessoaFisicaEmail = TipoPessoaFisicaEmail.NaoInformado, Email = valueAttribute.Value });
                }
            }
            else if (use == "ASN")
            {
                var valueAttribute = xmlNode.Attributes?["value"];
                if (valueAttribute != null)
                {
                    if (pessoaFisica.ListaPessoaFisicaTelefone == null)
                        pessoaFisica.ListaPessoaFisicaTelefone = new List<PessoaFisicaTelefoneDto>();

                    var split = valueAttribute.Value.Split('-');
                    pessoaFisica.ListaPessoaFisicaTelefone.Add(new PessoaFisicaTelefoneDto { TipoPessoaFisicaTelefone = TipoPessoaFisicaTelefone.NaoInformado, Ddi = split[0], Ddd = split[1], Numero = split[2], InPrincipal = false });
                }
            }
            else if (use == "BPN")
            {
                var valueAttribute = xmlNode.Attributes?["value"];
                if (valueAttribute != null)
                {
                    if (pessoaFisica.ListaPessoaFisicaTelefone == null)
                        pessoaFisica.ListaPessoaFisicaTelefone = new List<PessoaFisicaTelefoneDto>();

                    var split = valueAttribute.Value.Split('-');
                    pessoaFisica.ListaPessoaFisicaTelefone.Add(new PessoaFisicaTelefoneDto { TipoPessoaFisicaTelefone = TipoPessoaFisicaTelefone.BipPager, Ddi = split[0], Ddd = split[1], Numero = split[2], InPrincipal = false });
                }
            }
            else if (use == "EMR")
            {
                var valueAttribute = xmlNode.Attributes?["value"];
                if (valueAttribute != null)
                {
                    if (pessoaFisica.ListaPessoaFisicaTelefone == null)
                        pessoaFisica.ListaPessoaFisicaTelefone = new List<PessoaFisicaTelefoneDto>();

                    var split = valueAttribute.Value.Split('-');
                    pessoaFisica.ListaPessoaFisicaTelefone.Add(new PessoaFisicaTelefoneDto { TipoPessoaFisicaTelefone = TipoPessoaFisicaTelefone.Emergencia, Ddi = split[0], Ddd = split[1], Numero = split[2], InPrincipal = false });
                }
            }
            else if (use == "ORN")
            {
                var valueAttribute = xmlNode.Attributes?["value"];
                if (valueAttribute != null)
                {
                    if (pessoaFisica.ListaPessoaFisicaTelefone == null)
                        pessoaFisica.ListaPessoaFisicaTelefone = new List<PessoaFisicaTelefoneDto>();

                    var split = valueAttribute.Value.Split('-');
                    pessoaFisica.ListaPessoaFisicaTelefone.Add(new PessoaFisicaTelefoneDto { TipoPessoaFisicaTelefone = TipoPessoaFisicaTelefone.Residencial, Ddi = split[0], Ddd = split[1], Numero = split[2], InPrincipal = false });
                }
            }
            else if (use == "PRN")
            {
                var valueAttribute = xmlNode.Attributes?["value"];
                if (valueAttribute != null)
                {
                    if (pessoaFisica.ListaPessoaFisicaTelefone == null)
                        pessoaFisica.ListaPessoaFisicaTelefone = new List<PessoaFisicaTelefoneDto>();

                    var split = valueAttribute.Value.Split('-');
                    pessoaFisica.ListaPessoaFisicaTelefone.Add(new PessoaFisicaTelefoneDto { TipoPessoaFisicaTelefone = TipoPessoaFisicaTelefone.Residencial, Ddi = split[0], Ddd = split[1], Numero = split[2], InPrincipal = true });
                }
            }
            else if (use == "PRS")
            {
                var valueAttribute = xmlNode.Attributes?["value"];
                if (valueAttribute != null)
                {
                    if (pessoaFisica.ListaPessoaFisicaTelefone == null)
                        pessoaFisica.ListaPessoaFisicaTelefone = new List<PessoaFisicaTelefoneDto>();

                    var split = valueAttribute.Value.Split('-');
                    pessoaFisica.ListaPessoaFisicaTelefone.Add(new PessoaFisicaTelefoneDto { TipoPessoaFisicaTelefone = TipoPessoaFisicaTelefone.Celular, Ddi = split[0], Ddd = split[1], Numero = split[2], InPrincipal = false });
                }
            }
            else if (use == "VHN")
            {
                var valueAttribute = xmlNode.Attributes?["value"];
                if (valueAttribute != null)
                {
                    if (pessoaFisica.ListaPessoaFisicaTelefone == null)
                        pessoaFisica.ListaPessoaFisicaTelefone = new List<PessoaFisicaTelefoneDto>();

                    var split = valueAttribute.Value.Split('-');
                    pessoaFisica.ListaPessoaFisicaTelefone.Add(new PessoaFisicaTelefoneDto { TipoPessoaFisicaTelefone = TipoPessoaFisicaTelefone.Ferias, Ddi = split[0], Ddd = split[1], Numero = split[2], InPrincipal = false });
                }
            }
            else if (use == "WPN")
            {
                var valueAttribute = xmlNode.Attributes?["value"];
                if (valueAttribute != null)
                {
                    if (pessoaFisica.ListaPessoaFisicaTelefone == null)
                        pessoaFisica.ListaPessoaFisicaTelefone = new List<PessoaFisicaTelefoneDto>();

                    var split = valueAttribute.Value.Split('-');
                    pessoaFisica.ListaPessoaFisicaTelefone.Add(new PessoaFisicaTelefoneDto { TipoPessoaFisicaTelefone = TipoPessoaFisicaTelefone.Comercial, Ddi = split[0], Ddd = split[1], Numero = split[2], InPrincipal = false });
                }
            }

            return pessoaFisica;
        }

        private ICollection<CertidaoDto> ConverterCertidao(ICollection<CertidaoDto>? lCertidao, XmlNodeList xmlNodeList)
        {
            if (lCertidao == null)
                lCertidao = new List<CertidaoDto>();

            var certidao = new CertidaoDto { TipoCertidao = GePessoaFisica.TipoCertidao.NaoInformado };
            foreach (XmlNode iXmlNode in xmlNodeList)
            {
                if (iXmlNode.Attributes?["root"] != null)
                {
                    var root = iXmlNode.Attributes["root"]?.Value;
                    if (root == "2.16.840.1.113883.13.241.2" || root == "2.16.840.1.113883.13.241.1")
                    {
                        certidao.TipoCertidao = GePessoaFisica.TipoCertidao.Nascimento;
                    }
                    else if (root == "2.16.840.1.113883.13.241.4" || root == "22.16.840.1.113883.13.241.3")
                    {
                        certidao.TipoCertidao = GePessoaFisica.TipoCertidao.Casamento;
                    }
                    else if (root == "- 2.16.840.1.113883.13.241.6" || root == "2.16.840.1.113883.13.241.5")
                    {
                        certidao.TipoCertidao = GePessoaFisica.TipoCertidao.Divorcio;
                    }
                    else if (root == "2.16.840.1.113883.13.241.8" || root == "2.16.840.1.113883.13.241.7")
                    {
                        certidao.TipoCertidao = GePessoaFisica.TipoCertidao.Indigena;
                    }
                    else if (root == "2.16.840.1.113883.13.241.10" || root == "2.16.840.1.113883.13.241.9")
                    {
                        certidao.TipoCertidao = GePessoaFisica.TipoCertidao.Obito;
                    }
                    else if (root == "2.16.840.1.113883.4.706.1")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            certidao.NomeCartorio = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.4.706.2")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            certidao.Livro = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.4.706.3")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            certidao.Folha = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.4.706.4")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            certidao.Termo = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.4.706")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            certidao.Matricula = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.4.706.5")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            certidao.DataEmissao = ParaDateTime(extensionAttribute.Value);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(certidao.Termo))
            {
                certidao.ModeloCertidao = ModeloCertidao.Antigo;
                lCertidao.Add(certidao);
            }
            if(!string.IsNullOrWhiteSpace(certidao.Matricula))
            {
                certidao.ModeloCertidao = ModeloCertidao.Novo;
                lCertidao.Add(certidao);
            }

            return lCertidao;
        }

        private ICollection<CnhDto> ConverterCnh(ICollection<CnhDto>? lCnh, XmlNodeList xmlNodeList)
        {
            if (lCnh == null)
                lCnh = new List<CnhDto>();

            var cnh = new CnhDto();
            foreach (XmlNode iXmlNode in xmlNodeList)
            {
                if (iXmlNode.Attributes?["root"] != null)
                {
                    var root = iXmlNode.Attributes["root"]?.Value;
                    if (root == "2.16.840.1.113883.13.238")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            cnh.Numero = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.4.707")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            cnh.UnidadeFederativa = ConverterUnidadeFederativa(extensionAttribute.Value);
                    }
                    else if (root == "2.16.840.1.113883.13.238.1")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            cnh.DataEmissao = ParaDateTime(extensionAttribute.Value);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(cnh.Numero))
                lCnh.Add(cnh);

            return lCnh;
        }

        private ICollection<CnsDto> ConverterCns(ICollection<CnsDto>? lCns, XmlNodeList xmlNodeList)
        {
            if (lCns == null)
                lCns = new List<CnsDto>();

            var cns = new CnsDto { TipoCns = TipoCns.NaoInformado };
            foreach (XmlNode iXmlNode in xmlNodeList)
            {
                if (iXmlNode.Attributes?["root"] != null)
                {
                    var root = iXmlNode.Attributes["root"]?.Value;
                    if (root == "2.16.840.1.113883.13.236")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            cns.Numero = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.236.1")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                        {
                            if (extensionAttribute.Value == "D")
                                cns.TipoCns = TipoCns.Definitivo;
                            else if (extensionAttribute.Value == "P")
                                cns.TipoCns = TipoCns.Definitivo;
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(cns.Numero))
                lCns.Add(cns);

            return lCns;
        }

        private GePessoaFisica.CorRaca ConverterCorRaca(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new NotImplementedException();

            switch (codigo)
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

        private ICollection<CtpsDto> ConverterCtps(ICollection<CtpsDto>? lCtps, XmlNodeList xmlNodeList)
        {
            if (lCtps == null)
                lCtps = new List<CtpsDto>();

            var ctps = new CtpsDto();
            foreach (XmlNode iXmlNode in xmlNodeList)
            {
                if (iXmlNode.Attributes?["root"] != null)
                {
                    var root = iXmlNode.Attributes["root"]?.Value;
                    if (root == "2.16.840.1.113883.13.244")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            ctps.Numero = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.244.1")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            ctps.Serie = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.244.2")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            ctps.DataEmissao = ParaDateTime(extensionAttribute.Value);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(ctps.Numero))
                lCtps.Add(ctps);

            return lCtps;
        }

        private async Task<PessoaFisicaDto> ConverterNacionalidadeAsync(PessoaFisicaDto pessoaFisica, XmlNodeList xmlNodeList)
        {
            foreach (XmlNode iXmlNode in xmlNodeList)
            {
                if (iXmlNode.Attributes?["root"] != null)
                {
                    var root = iXmlNode.Attributes["root"]?.Value;
                    if (root == "2.16.840.1.113883.4.713")
                    {
                        pessoaFisica.TipoNacionalidade = GePessoaFisica.TipoNacionalidade.Naturalizado;
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                        {
                            var portariaNaturalizacao = await _portariaNaturalizacaoRepository.GetByNomeIncompletoAsync(extensionAttribute.Value);
                            if (portariaNaturalizacao == null)
                                throw new NotImplementedException();
                            pessoaFisica.PortariaNaturalizacao = ObjectMapper.Map<PortariaNaturalizacao, PortariaNaturalizacaoDto>(portariaNaturalizacao);
                        }
                    }
                    else if (root == "2.16.840.1.113883.4.713.1")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            pessoaFisica.DataEntradaBrasil = ParaDateTime(extensionAttribute.Value);
                    }
                    else if (root == "2.16.840.1.113883.4.713.2")
                    {
                        pessoaFisica.TipoNacionalidade = GePessoaFisica.TipoNacionalidade.Naturalizado;
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            pessoaFisica.DataNaturalizacao = ParaDateTime(extensionAttribute.Value);
                    }
                }
            }

            return pessoaFisica;
        }

        private async Task<ICollection<RgDto>> ConverterRgAsync(ICollection<RgDto>? lRg, XmlNodeList xmlNodeList)
        {
            if (lRg == null)
                lRg = new List<RgDto>();

            var rg = new RgDto();
            foreach (XmlNode iXmlNode in xmlNodeList)
            {
                if (iXmlNode.Attributes?["root"] != null)
                {
                    var root = iXmlNode.Attributes["root"]?.Value;
                    if (root == "2.16.840.1.113883.13.243")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            rg.Numero = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.243.1")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            rg.DataEmissao = ParaDateTime(extensionAttribute.Value);
                    }
                    else if (root == "2.16.840.1.113883.4.707")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            rg.UnidadeFederativa = ConverterUnidadeFederativa(extensionAttribute.Value);
                    }
                    else if (root == "2.16.840.1.113883.13.245")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                        {
                            var orgaoEmissor = await _orgaoEmissorRepository.GetByCodigoCadSusAsync(extensionAttribute.Value);
                            if (orgaoEmissor == null)
                                throw new NotImplementedException();
                            rg.OrgaoEmissor = ObjectMapper.Map<OrgaoEmissor, OrgaoEmissorDto>(orgaoEmissor);
                        }

                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(rg.Numero))
                lRg.Add(rg);

            return lRg;
        }

        private GePessoaFisica.Sexo ConverterSexo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new NotImplementedException();

            switch (codigo)
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

        private ICollection<TituloEleitorDto> ConverterTituloEleitor(ICollection<TituloEleitorDto>? lTituloEleitor, XmlNodeList xmlNodeList)
        {
            if (lTituloEleitor == null)
                lTituloEleitor = new List<TituloEleitorDto>();

            var tituloEleitor = new TituloEleitorDto();
            foreach (XmlNode iXmlNode in xmlNodeList)
            {
                if (iXmlNode.Attributes?["root"] != null)
                {
                    var root = iXmlNode.Attributes["root"]?.Value;
                    if (root == "2.16.840.1.113883.13.239")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            tituloEleitor.Numero = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.239.1")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            tituloEleitor.Zona = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.239.2")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            tituloEleitor.Secao = extensionAttribute.Value;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(tituloEleitor.Numero))
                lTituloEleitor.Add(tituloEleitor);

            return lTituloEleitor;
        }

        private GeGeocodificacao.UnidadeFederativa ConverterUnidadeFederativa(string sigla)
        {
            if (string.IsNullOrWhiteSpace(sigla))
                throw new NotImplementedException();

            switch (sigla)
            {
                case "RO":
                    return GeGeocodificacao.UnidadeFederativa.RO;
                case "AC":
                    return GeGeocodificacao.UnidadeFederativa.AC;
                case "AM":
                    return GeGeocodificacao.UnidadeFederativa.AM;
                case "RR":
                    return GeGeocodificacao.UnidadeFederativa.RR;
                case "PA":
                    return GeGeocodificacao.UnidadeFederativa.PA;
                case "AP":
                    return GeGeocodificacao.UnidadeFederativa.AP;
                case "TO":
                    return GeGeocodificacao.UnidadeFederativa.TO;
                case "MA":
                    return GeGeocodificacao.UnidadeFederativa.MA;
                case "PI":
                    return GeGeocodificacao.UnidadeFederativa.PI;
                case "CE":
                    return GeGeocodificacao.UnidadeFederativa.CE;
                case "RN":
                    return GeGeocodificacao.UnidadeFederativa.RN;
                case "PB":
                    return GeGeocodificacao.UnidadeFederativa.PB;
                case "PE":
                    return GeGeocodificacao.UnidadeFederativa.PE;
                case "AL":
                    return GeGeocodificacao.UnidadeFederativa.AL;
                case "SE":
                    return GeGeocodificacao.UnidadeFederativa.SE;
                case "BA":
                    return GeGeocodificacao.UnidadeFederativa.BA;
                case "MG":
                    return GeGeocodificacao.UnidadeFederativa.MG;
                case "ES":
                    return GeGeocodificacao.UnidadeFederativa.ES;
                case "RJ":
                    return GeGeocodificacao.UnidadeFederativa.RJ;
                case "SP":
                    return GeGeocodificacao.UnidadeFederativa.SP;
                case "PR":
                    return GeGeocodificacao.UnidadeFederativa.PR;
                case "SC":
                    return GeGeocodificacao.UnidadeFederativa.SC;
                case "RS":
                    return GeGeocodificacao.UnidadeFederativa.RS;
                case "MS":
                    return GeGeocodificacao.UnidadeFederativa.MS;
                case "MT":
                    return GeGeocodificacao.UnidadeFederativa.MT;
                case "GO":
                    return GeGeocodificacao.UnidadeFederativa.GO;
                case "DF":
                    return GeGeocodificacao.UnidadeFederativa.DF;
                default:
                    throw new NotImplementedException();
            }
        }

        private DateTime? ParaDateTime(string? s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return null;

            if (s.Length == 8)
                return DateTime.ParseExact(s, "yyyyMMdd", CultureInfo.InvariantCulture);
            else if (s.Length == 14)
                return DateTime.ParseExact(s, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

            return null;
        }

    }
}
