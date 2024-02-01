using NecnatAbp.Br.GeGeocodificacao;
using NecnatAbp.Br.GePessoaFisica;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;
using NecnatAbp.Extensions;
using NecnatAbp.Br.GeDatasus.Converters;

namespace NecnatAbp.Br.GeDatasus.Mapper
{
    public class FromCadSusMapper : IObjectMapper<string, List<PessoaFisicaDto>>, ITransientDependency
    {
        private readonly IBuscaFonetica _buscaFonetica;
        private readonly ISexoConverter _sexoConverter;
        private readonly ICorRacaConverter _corRacaConverter;
        private readonly IEtniaConverter _etniaConverter;
        private readonly IPaisConverter _paisConverter;

        public FromCadSusMapper(IBuscaFonetica buscaFonetica,
            ISexoConverter sexoConverter,
            ICorRacaConverter corRacaConverter,
            IEtniaConverter etniaConverter,
            IPaisConverter paisConverter)
        {
            _buscaFonetica = buscaFonetica;
            _sexoConverter = sexoConverter;
            _corRacaConverter = corRacaConverter;
            _etniaConverter = etniaConverter;
            _paisConverter = paisConverter;
        }

        public List<PessoaFisicaDto> Map(string source)
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
                    if (iChildNodes.Name == "name")
                        destination = TratarName(destination, iChildNodes);

                    else if (iChildNodes.Name == "telecom")
                        destination = TratarTelecom(destination, iChildNodes);

                    else if (iChildNodes.Name == "administrativeGenderCode")
                        destination = TratarAdministrativeGenderCode(destination, iChildNodes);

                    else if (iChildNodes.Name == "birthTime")
                        destination = TratarBirthTime(destination, iChildNodes);

                    else if (iChildNodes.Name == "addr")
                        destination = TratarAddr(destination, iChildNodes);

                    else if (iChildNodes.Name == "raceCode")
                        destination = TratarRaceCode(destination, iChildNodes);

                    else if (iChildNodes.Name == "ethnicGroupCode")
                        destination = TratarEthnicGroupCode(destination, iChildNodes);

                    else if (iChildNodes.Name == "asCitizen")
                        destination = TratarAsCitizen(destination, iChildNodes);

                    else if (iChildNodes.Name == "asOtherIDs")
                        destination = TratarAsOtherIDs(destination, iChildNodes);

                    else if (iChildNodes.Name == "personalRelationship")
                        destination = TratarPersonalRelationship(destination, iChildNodes);

                    else if (iChildNodes.Name == "birthPlace")
                        destination = TratarBirthPlace(destination, iChildNodes);

                    else if (iChildNodes.Name == "deceasedInd")
                        destination = TratarDeceasedInd(destination, iChildNodes);
                }

                lDestination.Add(destination);
            }

            return lDestination;
        }

        public List<PessoaFisicaDto> Map(string source, List<PessoaFisicaDto> destination)
        {
            throw new NotImplementedException();
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

        private PessoaFisicaDto TratarAdministrativeGenderCode(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            if (string.IsNullOrEmpty(xmlNode.Attributes?["code"]?.Value))
                return pessoaFisica;

            pessoaFisica.Sexo = _sexoConverter.FromCodigoCadSusDoc(xmlNode.Attributes!["code"]!.Value);
            return pessoaFisica;
        }

        private PessoaFisicaDto TratarBirthTime(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            pessoaFisica.DataNascimento = ConverterParaDateTime(xmlNode.Attributes?["value"]?.Value);

            return pessoaFisica;
        }

        private PessoaFisicaDto TratarAddr(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            if (pessoaFisica.ListaPessoaFisicaEndereco == null)
                pessoaFisica.ListaPessoaFisicaEndereco = new List<PessoaFisicaEnderecoDto>();

            var endereco = new PessoaFisicaEnderecoDto();
            endereco.Endereco = new EnderecoDto();
            endereco.Endereco.Logradouro = new LogradouroDto();

            var city = xmlNode["city"];
            if (city != null)
                endereco.Endereco.Logradouro.CidadeMunicipio = new CidadeMunicipioDto { CodigoIbge = city.InnerText };

            var state = xmlNode["state"];
            if (state != null)
            {
                if (!string.IsNullOrEmpty(state.InnerText) && state.InnerText != "XX")
                    endereco.Endereco.Logradouro.UnidadeFederativa = (UnidadeFederativa)Enum.Parse(typeof(UnidadeFederativa), state.InnerText);
            }

            var postalCode = xmlNode["postalCode"];
            if (postalCode != null)
            {
                endereco.Endereco.Logradouro.Cep = postalCode.InnerText;
            }

            var country = xmlNode["country"];
            if (country != null)
                endereco.Endereco.Logradouro.Pais = new PaisDto { CodigoNacionalidade = country.InnerText };

            var houseNumber = xmlNode["houseNumber"];
            if (houseNumber != null && houseNumber.InnerText != "S/N")
                endereco.Endereco.Numero = int.Parse(houseNumber.InnerText);

            var streetName = xmlNode["streetName"];
            if (streetName != null)
                endereco.Endereco.Logradouro.Nome = streetName.InnerText;

            var streetNameType = xmlNode["streetNameType"];
            if (streetNameType != null)
                endereco.Endereco.Logradouro.TipoLogradouro = new TipoLogradouroDto { CodigoCorreios = streetNameType.InnerText };

            var additionalLocator = xmlNode["additionalLocator"];
            if (additionalLocator != null)
                endereco.Endereco.Logradouro.BairroDistrito = new BairroDistritoDto { Nome = additionalLocator.InnerText };

            var unitID = xmlNode["unitID"];
            if (unitID != null)
                endereco.Complemento = unitID.InnerText;

            pessoaFisica.ListaPessoaFisicaEndereco.Add(endereco);

            return pessoaFisica;
        }

        private PessoaFisicaDto TratarRaceCode(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            if (string.IsNullOrEmpty(xmlNode.Attributes?["code"]?.Value))
                return pessoaFisica;

            pessoaFisica.CorRaca = _corRacaConverter.FromCodigoCadSus(xmlNode.Attributes!["code"]!.Value);
            return pessoaFisica;
        }

        private PessoaFisicaDto TratarEthnicGroupCode(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            if (string.IsNullOrEmpty(xmlNode.Attributes?["code"]?.Value))
                return pessoaFisica;

            pessoaFisica.Etnia = _etniaConverter.FromCodigoCadSus(xmlNode.Attributes!["code"]!.Value);
            return pessoaFisica;
        }

        private PessoaFisicaDto TratarAsCitizen(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            pessoaFisica.Passaporte = new PassaporteDto();

            var idAsCitizen = xmlNode["id"];
            if (idAsCitizen != null)
            {
                var extensionAttribute = idAsCitizen.Attributes["extension"];
                if (extensionAttribute != null)
                {
                    pessoaFisica.Passaporte.Numero = extensionAttribute.Value;
                }
            }

            var effectiveTimeAsCitizen = xmlNode["effectiveTime"];
            if (effectiveTimeAsCitizen != null)
            {
                var extensionAttribute = effectiveTimeAsCitizen.Attributes["value"];
                if (extensionAttribute != null)
                {
                    pessoaFisica.Passaporte.DataEmissao = ConverterParaDateTime(extensionAttribute.Value);
                }
            }

            var highAsCitizen = xmlNode["high"];
            if (highAsCitizen != null)
            {
                var extensionAttribute = highAsCitizen.Attributes["value"];
                if (extensionAttribute != null)
                {
                    pessoaFisica.Passaporte.DataValidade = ConverterParaDateTime(extensionAttribute.Value);
                }
            }

            return pessoaFisica;
        }

        private PessoaFisicaDto TratarAsOtherIDs(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
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
                        pessoaFisica.ListaCns = TratarCns(pessoaFisica.ListaCns, lChildNodes);
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
                        pessoaFisica = TratarNacionalidade(pessoaFisica, lChildNodes);
                        continue;
                    }

                }
            }





            TipoCertidao? tipoCertidao = null;
            var certidaoAntigaNomeCartorio = string.Empty;
            var certidaoAntigaLivro = string.Empty;
            var certidaoAntigaFolha = string.Empty;
            var certidaoAntigaTermo = string.Empty;
            var certidaoAntigaDataEmissao = string.Empty;

            var certidaoNovaDados = string.Empty;
            var certidaoNovaDataEmissao = string.Empty;

            var cnhNumero = string.Empty;
            var cnhUf = string.Empty;
            var cnhDataEmissao = string.Empty;

            var ctpsNumero = string.Empty;
            var ctpsSerie = string.Empty;
            var ctpsDataEmissao = string.Empty;

            var rgNumero = string.Empty;
            var rgDataEmissao = string.Empty;
            var rgUf = string.Empty;
            var rgOrgaoEmissor = string.Empty;

            var tituloEleitorNumero = string.Empty;
            var tituloEleitorZona = string.Empty;
            var tituloEleitorSecao = string.Empty;

            var lChildNodes = xmlNode.ChildNodes;

            foreach (XmlNode iChildNode in lChildNodes)
            {
                if (iChildNode.Attributes?["root"] != null)
                {
                    var root = iChildNode.Attributes["root"]?.Value;

                    //Naturalizado
                    if (root == "2.16.840.1.113883.4.713")
                    {
                        pessoaFisica.TipoNacionalidade = TipoNacionalidade.Naturalizado;
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            pessoaFisica.PortariaNaturalizacao = new PortariaNaturalizacaoDto { Nome = extensionAttribute.Value };
                    }
                    else if (root == "2.16.840.1.113883.4.713.1")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            pessoaFisica.DataEntradaBrasil = ConverterParaDateTime(extensionAttribute.Value);
                    }
                    else if (root == "2.16.840.1.113883.4.713.2")
                    {
                        pessoaFisica.TipoNacionalidade = TipoNacionalidade.Naturalizado;
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            pessoaFisica.DataNaturalizacao = ConverterParaDateTime(extensionAttribute.Value);
                    }

                    //Documentos
                    if (root == "2.16.840.1.113883.13.236")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            cnsNumero = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.236.1")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            cnsSituacao = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.237")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            pessoaFisica.Cpf = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.243")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            rgNumero = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.243.1")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            rgDataEmissao = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.4.707")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            rgUf = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.245")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            rgOrgaoEmissor = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.244")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            ctpsNumero = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.244.1")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            ctpsSerie = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.244.2")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            ctpsDataEmissao = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.238")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            cnhNumero = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.4.707")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            cnhUf = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.238.1")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            cnhDataEmissao = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.239")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            tituloEleitorNumero = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.239.1")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            tituloEleitorZona = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.239.2")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            tituloEleitorSecao = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.13.240")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            pessoaFisica.PisPasepNisNit = new PisPasepNisNitDto { Numero = extensionAttribute.Value };
                    }
                    else if (root == "2.16.840.1.113883.3.3024")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            pessoaFisica.Ric = new RicDto { Numero = extensionAttribute.Value };
                    }
                    else if (root == "2.16.840.1.113883.13.244")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            pessoaFisica.Dnv = new DnvDto { Numero = extensionAttribute.Value };
                    }
                    else if (root == "2.16.840.1.113883.13.241.2" || root == "2.16.840.1.113883.13.241.1")
                    {
                        tipoCertidao = TipoCertidao.Nascimento;
                    }
                    else if (root == "2.16.840.1.113883.13.241.4" || root == "22.16.840.1.113883.13.241.3")
                    {
                        tipoCertidao = TipoCertidao.Casamento;
                    }
                    else if (root == "- 2.16.840.1.113883.13.241.6" || root == "2.16.840.1.113883.13.241.5")
                    {
                        tipoCertidao = TipoCertidao.Divorcio;
                    }
                    else if (root == "2.16.840.1.113883.13.241.8" || root == "2.16.840.1.113883.13.241.7")
                    {
                        tipoCertidao = TipoCertidao.Indigena;
                    }
                    else if (root == "2.16.840.1.113883.13.241.10" || root == "2.16.840.1.113883.13.241.9")
                    {
                        tipoCertidao = TipoCertidao.Obito;
                    }
                    else if (root == "2.16.840.1.113883.4.706.1")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            certidaoAntigaNomeCartorio = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.4.706.2")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            certidaoAntigaLivro = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.4.706.3")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            certidaoAntigaFolha = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.4.706.4")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            certidaoAntigaTermo = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.4.706.5")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            certidaoAntigaDataEmissao = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.4.706")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            certidaoNovaDados = extensionAttribute.Value;
                    }
                    else if (root == "2.16.840.1.113883.4.706.5")
                    {
                        var extensionAttribute = iChildNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            certidaoNovaDataEmissao = extensionAttribute.Value;
                    }
                }
            }

            //RG
            if (!string.IsNullOrEmpty(rgNumero))
            {
                pessoaFisica.Rg = new RgDto();
                pessoaFisica.Rg.Numero = rgNumero;

                if (!string.IsNullOrEmpty(rgDataEmissao))
                    pessoaFisica.Rg.DataEmissao = ConverterParaDateTime(rgDataEmissao);

                if (!string.IsNullOrEmpty(rgUf))
                    pessoaFisica.Rg.UnidadeFederativa = (UnidadeFederativa)Enum.Parse(typeof(UnidadeFederativa), rgUf);

                if (!string.IsNullOrEmpty(rgOrgaoEmissor))
                    pessoaFisica.Rg.OrgaoEmissor = new OrgaoEmissorDto { CodigoDataSus = rgOrgaoEmissor };

                rgNumero = string.Empty;
                rgDataEmissao = string.Empty;
                rgUf = string.Empty;
                rgOrgaoEmissor = string.Empty;
            }

            //CTPS
            if (!string.IsNullOrEmpty(ctpsNumero))
            {
                pessoaFisica.Ctps = new CtpsDto();
                pessoaFisica.Ctps.Numero = ctpsNumero;

                if (!string.IsNullOrEmpty(ctpsSerie))
                    pessoaFisica.Ctps.Serie = ctpsSerie;

                if (!string.IsNullOrEmpty(ctpsDataEmissao))
                    pessoaFisica.Ctps.DataEmissao = ConverterParaDateTime(ctpsDataEmissao);

                ctpsNumero = string.Empty;
                ctpsSerie = string.Empty;
                ctpsDataEmissao = string.Empty;
            }

            //CNH
            if (!string.IsNullOrEmpty(cnhNumero))
            {
                pessoaFisica.Cnh = new CnhDto();
                pessoaFisica.Cnh.Numero = cnhNumero;

                if (!string.IsNullOrEmpty(cnhUf))
                    pessoaFisica.Cnh.UnidadeFederativa = (UnidadeFederativa)Enum.Parse(typeof(UnidadeFederativa), cnhUf);

                if (!string.IsNullOrEmpty(cnhDataEmissao))
                    pessoaFisica.Cnh.DataEmissao = ConverterParaDateTime(cnhDataEmissao);

                cnhNumero = string.Empty;
                cnhUf = string.Empty;
                cnhDataEmissao = string.Empty;
            }

            //Titulo Eleitor
            if (!string.IsNullOrEmpty(tituloEleitorNumero))
            {
                pessoaFisica.TituloEleitor = new TituloEleitorDto();
                pessoaFisica.TituloEleitor.Numero = tituloEleitorNumero;

                if (!string.IsNullOrEmpty(tituloEleitorZona))
                    pessoaFisica.TituloEleitor.Zona = tituloEleitorZona;

                if (!string.IsNullOrEmpty(tituloEleitorSecao))
                    pessoaFisica.TituloEleitor.Secao = tituloEleitorSecao;

                tituloEleitorNumero = string.Empty;
                tituloEleitorSecao = string.Empty;
                tituloEleitorZona = string.Empty;
            }

            //Certidao
            if (tipoCertidao != null)
            {
                var certidaoDto = new CertidaoDto();

                certidaoDto.TipoCertidao = tipoCertidao;

                if (!string.IsNullOrEmpty(certidaoAntigaNomeCartorio))
                    certidaoDto.AntigoNomeCartorio = certidaoAntigaNomeCartorio;

                if (!string.IsNullOrEmpty(certidaoAntigaLivro))
                    certidaoDto.AntigoNumeroLivro = certidaoAntigaLivro;

                if (!string.IsNullOrEmpty(certidaoAntigaFolha))
                    certidaoDto.AntigoNumeroFolha = certidaoAntigaFolha;

                if (!string.IsNullOrEmpty(certidaoAntigaTermo))
                    certidaoDto.AntigoNumeroTermo = certidaoAntigaTermo;

                if (!string.IsNullOrEmpty(certidaoAntigaDataEmissao))
                    certidaoDto.AntigoDataEmissao = ConverterParaDateTime(certidaoAntigaDataEmissao);

                if (!string.IsNullOrEmpty(certidaoNovaDados))
                    certidaoDto.NovoNumeroMatricula = certidaoNovaDados;

                if (!string.IsNullOrEmpty(certidaoNovaDataEmissao))
                    certidaoDto.NovoDataEmissao = ConverterParaDateTime(certidaoNovaDataEmissao);

                tipoCertidao = null;
                certidaoAntigaNomeCartorio = string.Empty;
                certidaoAntigaLivro = string.Empty;
                certidaoAntigaFolha = string.Empty;
                certidaoAntigaTermo = string.Empty;
                certidaoAntigaDataEmissao = string.Empty;

                certidaoNovaDados = string.Empty;
                certidaoNovaDataEmissao = string.Empty;

                pessoaFisica.ListaCertidao!.Add(certidaoDto);
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

        private PessoaFisicaDto TratarBirthPlace(PessoaFisicaDto pessoaFisica, XmlNode xmlNode)
        {
            var addrBirthPlace = xmlNode["addr"];
            if (addrBirthPlace != null)
            {
                var countryBirthPlace = addrBirthPlace["country"];
                if (countryBirthPlace != null)
                {
                    pessoaFisica.NacionalidadeIdPais = _paisConverter.FromCodigoCadSus(countryBirthPlace.InnerText);
                    if (countryBirthPlace.InnerText.Equals("010"))
                    {
                        pessoaFisica.TipoNacionalidade = GePessoaFisica.TipoNacionalidade.Brasileiro;
                        var cityBirthPlace = addrBirthPlace["city"];
                        if (cityBirthPlace != null)
                        {
                            pessoaFisica.NaturalidadeIdCidadeMunicipio = new CidadeMunicipioDto { CodigoIbge = cityBirthPlace.InnerText };
                        }
                    }
                    else if (pessoaFisica.TipoNacionalidade == null)
                        pessoaFisica.TipoNacionalidade = GePessoaFisica.TipoNacionalidade.Estrangeiro;
                }
            }

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
                pessoaFisica.DataObito = ConverterParaDateTime(deceasedTime.InnerText);
                pessoaFisica.InObito = true;
            }

            return pessoaFisica;
        }

        private ICollection<CnsDto> TratarCns(ICollection<CnsDto>? lCns, XmlNodeList xmlNodeList)
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

        private PessoaFisicaDto TratarNacionalidade(PessoaFisicaDto pessoaFisica, XmlNodeList xmlNodeList)
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
                            pessoaFisica.PortariaNaturalizacao = new PortariaNaturalizacaoDto { Nome = extensionAttribute.Value };
                    }
                    else if (root == "2.16.840.1.113883.4.713.1")
                    {
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            pessoaFisica.DataEntradaBrasil = ConverterParaDateTime(extensionAttribute.Value);
                    }
                    else if (root == "2.16.840.1.113883.4.713.2")
                    {
                        pessoaFisica.TipoNacionalidade = GePessoaFisica.TipoNacionalidade.Naturalizado;
                        var extensionAttribute = iXmlNode.Attributes["extension"];
                        if (extensionAttribute != null)
                            pessoaFisica.DataNaturalizacao = ConverterParaDateTime(extensionAttribute.Value);
                    }
                }
            }

            return pessoaFisica;
        }

        private DateTime? ConverterParaDateTime(string? s)
        {
            if (s == null)
                return null;

            if (s.Length == 8)
                return DateTime.ParseExact(s, "yyyyMMdd", CultureInfo.InvariantCulture);
            else if (s.Length == 14)
                return DateTime.ParseExact(s, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

            return null;
        }
    }
}
