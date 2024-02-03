using NecnatAbp.Extensions;
using NecnatAbp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace NecnatAbp.Br.GeGeocodificacao.DataSeedContributors
{
    public partial class IbgeDtbDataSeederContributor : ITransientDependency
    {
        private const int _massaTeste = 1000; // TODO: Apagar
        private const bool _force = false;
        private const string _municipioResourceName = "NecnatAbp.Br.GeGeocodificacao.DataSeedContributors.IbgeDtb.Date2022.RELATORIO_DTB_BRASIL_MUNICIPIO.csv";
        private const string _distritoResourceName = "NecnatAbp.Br.GeGeocodificacao.DataSeedContributors.IbgeDtb.Date2022.RELATORIO_DTB_BRASIL_DISTRITO.csv";
        private const string _subdistritooResourceName = "NecnatAbp.Br.GeGeocodificacao.DataSeedContributors.IbgeDtb.Date2022.RELATORIO_DTB_BRASIL_SUBDISTRITO.csv";
        private const char _delimitador = ',';
        private string _logName = string.Empty;

        private readonly ICidadeMunicipioRepository _cidadeMunicipioRepository;
        private readonly IBairroDistritoRepository _bairroDistritoRepository;
        private readonly ISubdistritoRepository _subdistritoRepository;

        public IbgeDtbDataSeederContributor(
            ICidadeMunicipioRepository cidadeMunicipioRepository,
            IBairroDistritoRepository bairroDistritoRepository,
            ISubdistritoRepository subdistritoRepository)
        {
            _cidadeMunicipioRepository = cidadeMunicipioRepository;
            _bairroDistritoRepository = bairroDistritoRepository;
            _subdistritoRepository = subdistritoRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedMunicipio();
            await SeedDistrito();
            await SeedSubdistrito();
        }

        private async Task SeedMunicipio()
        {
            _logName = "Municipio";

            if ((!_force) && await _cidadeMunicipioRepository.GetCountAsync() > 0)
            {
                Console.WriteLine($"[{_logName}] Seed cancelado. Já existem registros na tabela.");
                return;
            }

            Console.WriteLine($"[{_logName}] Seed iniciado.");

            using (Stream? stream = Assembly.GetAssembly(typeof(IbgeDtbDataSeederContributor))?.GetManifestResourceStream(_municipioResourceName))
            {
                if (stream == null)
                {
                    Console.WriteLine($"[{_logName}] Seed cancelado. Arquivo não encontrado.");
                    return;
                }

                var count = await _cidadeMunicipioRepository.UpdateAllAtivoAsync(false);
                Console.WriteLine($"[{_logName}] Atualmente existem {count} registros. Todos eles foram inativados.");

                using (StreamReader sr = new StreamReader(stream))
                {
                    await sr.ReadLineAsync(); //Linha de cabecalho
                    Console.Write($"[{_logName}] Percorrendo Arquivo de Municípios... ");

                    var i = 0;
                    string? currentLine;
                    using (var progress = new ConsoleBarHelper())
                    {
                        while ((currentLine = await sr.ReadLineAsync()) != null)
                        {
                            progress.Report(i++);
                            if (string.IsNullOrWhiteSpace(currentLine))
                                continue;
                            var split = currentLine.Split(_delimitador);

                            var codigoIbge = split[MunicipiosDict["Código Município Completo"]].RemoveQuotationMarks();
                            var unidadeFederativa = (UnidadeFederativa)int.Parse(split[MunicipiosDict["UF"]].RemoveQuotationMarks());
                            var nome = split[MunicipiosDict["Nome_Município"]].RemoveQuotationMarks();
                            if (codigoIbge != null)
                            {
                                var isInsert = false;
                                var eDb = await _cidadeMunicipioRepository.GetByCodigoIbgeAsync(codigoIbge);
                                if (eDb == null)
                                {
                                    var eDbSearch = await _cidadeMunicipioRepository.GetByUnidadeFederativaAndNomeAsync(unidadeFederativa, nome);
                                    if (eDbSearch != null && eDbSearch.Origem != (int)OrigemGeocodificacao.Ibge)
                                        eDb = eDbSearch;
                                }

                                if (eDb == null)
                                {
                                    eDb = new CidadeMunicipio();
                                    isInsert = true;
                                }

                                eDb.UnidadeFederativa = unidadeFederativa;
                                eDb.CodigoIbge = codigoIbge;
                                eDb.Nome = nome;
                                eDb.InAtivo = true;
                                eDb.Origem = (int)OrigemGeocodificacao.Ibge;

                                if (isInsert)
                                    await _cidadeMunicipioRepository.InsertAsync(eDb, true);
                                else
                                    await _cidadeMunicipioRepository.UpdateAsync(eDb, true);
                            }
                        }
                    }

                    Console.WriteLine($"[{_logName}] Seed finalizado.");
                }
            }
        }

        private async Task SeedDistrito()
        {
            _logName = "Distrito";

            if ((!_force) && await _bairroDistritoRepository.GetCountAsync() > 0)
            {
                Console.WriteLine($"[{_logName}] Seed cancelado. Já existem registros na tabela.");
                return;
            }

            Console.WriteLine($"[{_logName}] Seed iniciado.");

            using (Stream? stream = Assembly.GetAssembly(typeof(IbgeDtbDataSeederContributor))?.GetManifestResourceStream(_distritoResourceName))
            {
                if (stream == null)
                {
                    Console.WriteLine($"[{_logName}] Seed cancelado. Arquivo não encontrado.");
                    return;
                }

                var count = await _bairroDistritoRepository.UpdateAllAtivoAsync(false);
                Console.WriteLine($"[{_logName}] Atualmente existem {count} registros. Todos eles foram inativados.");

                using (StreamReader sr = new StreamReader(stream))
                {
                    await sr.ReadLineAsync(); //Linha de cabecalho
                    Console.Write($"[{_logName}] Percorrendo Arquivo de Distritos... ");

                    var i = 0;
                    string? currentLine;
                    using (var progress = new ConsoleBarHelper())
                    {
                        while ((currentLine = await sr.ReadLineAsync()) != null)
                        {
                            progress.Report(i++);
                            if (string.IsNullOrWhiteSpace(currentLine))
                                continue;
                            var split = currentLine.Split(_delimitador);

                            var codigoIbge = split[DistritosDict["Código de Distrito Completo"]].RemoveQuotationMarks();
                            var codigoIbgeMunicipio = split[DistritosDict["Código Município Completo"]].RemoveQuotationMarks();
                            var nome = split[DistritosDict["Nome_Distrito"]].RemoveQuotationMarks();
                            if (codigoIbge != null)
                            {
                                var cidadeMunicipio = await _cidadeMunicipioRepository.GetByCodigoIbgeAsync(codigoIbgeMunicipio);
                                if (cidadeMunicipio == null)
                                    continue;

                                var isInsert = false;
                                var eDb = await _bairroDistritoRepository.GetByCodigoIbgeAsync(codigoIbge);
                                if (eDb == null)
                                {
                                    var eDbSearch = await _bairroDistritoRepository.GetByCidadeMunicipioIdAndNomeAsync(cidadeMunicipio.Id, nome);
                                    if (eDbSearch != null && eDbSearch.Origem != (int)OrigemGeocodificacao.Ibge)
                                        eDb = eDbSearch;
                                }

                                if (eDb == null)
                                {
                                    eDb = new BairroDistrito();
                                    isInsert = true;
                                }

                                eDb.CidadeMunicipioId = cidadeMunicipio.Id;
                                eDb.CodigoIbge = codigoIbge;
                                eDb.Nome = nome;
                                eDb.InAtivo = true;
                                eDb.Origem = (int)OrigemGeocodificacao.Ibge;

                                if (isInsert)
                                    await _bairroDistritoRepository.InsertAsync(eDb, true);
                                else
                                    await _bairroDistritoRepository.UpdateAsync(eDb, true);
                            }

                            if (i >= _massaTeste)
                                break;
                        }
                    }

                    Console.WriteLine($"[{_logName}] Seed finalizado.");
                }
            }
        }

        private async Task SeedSubdistrito()
        {
            _logName = "Subdistrito";

            if ((!_force) && await _subdistritoRepository.GetCountAsync() > 0)
            {
                Console.WriteLine($"[{_logName}] Seed cancelado. Já existem registros na tabela.");
                return;
            }

            Console.WriteLine($"[{_logName}] Seed iniciado.");

            using (Stream? stream = Assembly.GetAssembly(typeof(IbgeDtbDataSeederContributor))?.GetManifestResourceStream(_subdistritooResourceName))
            {
                if (stream == null)
                {
                    Console.WriteLine($"[{_logName}] Seed cancelado. Arquivo não encontrado.");
                    return;
                }

                var count = await _subdistritoRepository.UpdateAllAtivoAsync(false);
                Console.WriteLine($"[{_logName}] Atualmente existem {count} registros. Todos eles foram inativados.");

                using (StreamReader sr = new StreamReader(stream))
                {
                    await sr.ReadLineAsync(); //Linha de cabecalho
                    Console.Write($"[{_logName}] Percorrendo Arquivo de Subdistritos... ");

                    var i = 0;
                    string? currentLine;
                    using (var progress = new ConsoleBarHelper())
                    {
                        while ((currentLine = await sr.ReadLineAsync()) != null)
                        {
                            progress.Report(i++);
                            if (string.IsNullOrWhiteSpace(currentLine))
                                continue;
                            var split = currentLine.Split(_delimitador);

                            var codigoIbge = split[SubdistritosDict["Código de Subdistrito Completo"]].RemoveQuotationMarks();
                            var codigoIbgeDistrito = split[SubdistritosDict["Código de Distrito Completo"]].RemoveQuotationMarks();
                            var nome = split[SubdistritosDict["Nome_Subdistrito"]].RemoveQuotationMarks();
                            if (codigoIbge != null)
                            {
                                var bairroDistrito = await _bairroDistritoRepository.GetByCodigoIbgeAsync(codigoIbgeDistrito);
                                if (bairroDistrito == null)
                                    continue;

                                var isInsert = false;
                                var eDb = await _subdistritoRepository.GetByCodigoIbgeAsync(codigoIbge);
                                if (eDb == null)
                                {
                                    var eDbSearch = await _subdistritoRepository.GetByBairroDistritoIdAndNomeAsync(bairroDistrito.Id, nome);
                                    if (eDbSearch != null && eDbSearch.Origem != (int)OrigemGeocodificacao.Ibge)
                                        eDb = eDbSearch;
                                }

                                if (eDb == null)
                                {
                                    eDb = new Subdistrito();
                                    isInsert = true;
                                }

                                eDb.BairroDistritoId = bairroDistrito.Id;
                                eDb.CodigoIbge = codigoIbge;
                                eDb.Nome = nome;
                                eDb.InAtivo = true;
                                eDb.Origem = (int)OrigemGeocodificacao.Ibge;

                                if (isInsert)
                                    await _subdistritoRepository.InsertAsync(eDb, true);
                                else
                                    await _subdistritoRepository.UpdateAsync(eDb, true);
                            }

                            if (i >= _massaTeste)
                                break;
                        }
                    }

                    Console.WriteLine($"[{_logName}] Seed finalizado.");
                }
            }
        }

        #region DePara

        private Dictionary<string, int> MunicipiosDict = new Dictionary<string, int>
        {
            { "UF", 0 },
            { "Nome_UF", 1 },
            { "Região Geográfica Intermediária", 2 },
            { "Nome Região Geográfica Intermediária", 3 },
            { "Região Geográfica Imediata", 4 },
            { "Nome Região Geográfica Imediata", 5 },
            { "Mesorregião Geográfica", 6 },
            { "Nome_Mesorregião", 7 },
            { "Microrregião Geográfica", 8 },
            { "Nome_Microrregião", 9 },
            { "Município", 10 },
            { "Código Município Completo", 11 },
            { "Nome_Município", 12 }
        };

        private Dictionary<string, int> DistritosDict = new Dictionary<string, int>
        {
            { "UF", 0 },
            { "Nome_UF", 1 },
            { "Região Geográfica Intermediária", 2 },
            { "Nome Região Geográfica Intermediária", 3 },
            { "Região Geográfica Imediata", 4 },
            { "Nome Região Geográfica Imediata", 5 },
            { "Mesorregião Geográfica", 6 },
            { "Nome_Mesorregião", 7 },
            { "Microrregião Geográfica", 8 },
            { "Nome_Microrregião", 9 },
            { "Município", 10 },
            { "Código Município Completo", 11 },
            { "Nome_Município", 12 },
            { "Distrito", 13 },
            { "Código de Distrito Completo", 14 },
            { "Nome_Distrito", 15 }
        };

        private Dictionary<string, int> SubdistritosDict = new Dictionary<string, int>
        {
            { "UF", 0 },
            { "Nome_UF", 1 },
            { "Região Geográfica Intermediária", 2 },
            { "Nome Região Geográfica Intermediária", 3 },
            { "Região Geográfica Imediata", 4 },
            { "Nome Região Geográfica Imediata", 5 },
            { "Mesorregião Geográfica", 6 },
            { "Nome_Mesorregião", 7 },
            { "Microrregião Geográfica", 8 },
            { "Nome_Microrregião", 9 },
            { "Município", 10 },
            { "Código Município Completo", 11 },
            { "Nome_Município", 12 },
            { "Distrito", 13 },
            { "Código de Distrito Completo", 14 },
            { "Nome_Distrito", 15 },
            { "Subdistrito", 16 },
            { "Código de Subdistrito Completo", 17 },
            { "Nome_Subdistrito", 18 },
        };

        #endregion
    }
}
