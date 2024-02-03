using NecnatAbp.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace NecnatAbp.Br.GeGeocodificacao.DataSeedContributors
{
    public partial class Iso3166DataSeederContributor : ITransientDependency
    {
        private const bool _force = false;
        private const string _logName = "Pais";
        private const string _resourceName = "NecnatAbp.Br.GeGeocodificacao.DataSeedContributors.Iso3166.Date20230921.ISO3166.json";
        private readonly IPaisRepository _paisRepository;

        public Iso3166DataSeederContributor(IPaisRepository paisRepository)
        {
            _paisRepository = paisRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if ((!_force) && await _paisRepository.GetCountAsync() > 0)
            {
                Console.WriteLine($"[{_logName}] Seed cancelado. Já existem registros na tabela.");
                return;
            }

            Console.WriteLine($"[{_logName}] Seed iniciado.");

            List<Pais>? lEntidade = null;
            using (Stream? stream = Assembly.GetAssembly(typeof(Iso3166DataSeederContributor))?.GetManifestResourceStream(_resourceName))
            {
                if (stream == null)
                {
                    Console.WriteLine($"[{_logName}] Seed cancelado. Arquivo não encontrado.");
                    return;
                }

                using (StreamReader sr = new StreamReader(stream))
                {
                    lEntidade = JsonConvert.DeserializeObject<List<Pais>>(await sr.ReadToEndAsync());
                }
            }

            if (lEntidade == null)
            {
                Console.WriteLine($"[{_logName}] Arquivo inválido.");
                return;
            }

            var count = await _paisRepository.UpdateAllAtivoAsync(false);
            Console.WriteLine($"[{_logName}] Atualmente existem {count} registros. Todos eles foram inativados.");
            Console.Write($"[{_logName}] Percorrendo Arquivo de Municípios... ");

            var i = 0;
            using (var progress = new ConsoleBarHelper())
            {
                foreach (var iEntidade in lEntidade)
                {
                    progress.Report(i++);

                    if (!string.IsNullOrWhiteSpace(iEntidade.CodigoIso3166Numeric))
                    {
                        var eDb = await _paisRepository.GetByCodigoIso3166NumericAsync(iEntidade.CodigoIso3166Numeric!);
                        if (eDb == null)
                        {
                            var eDbSearch = await _paisRepository.GetByNomeAsync(iEntidade.Nome);
                            if (eDbSearch != null && eDbSearch.Origem != (int)OrigemGeocodificacao.Iso3166)
                                eDb = eDbSearch;
                        }

                        if (eDb == null)
                        {
                            iEntidade.InAtivo = true;
                            await _paisRepository.InsertAsync(iEntidade);
                        }                            
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(iEntidade.Nome))
                                eDb.Nome = iEntidade.Nome;
                            eDb.NomeIngles = iEntidade.NomeIngles;
                            eDb.NomeFrances = iEntidade.NomeFrances;
                            eDb.CodigoIso3166Alpha2 = iEntidade.CodigoIso3166Alpha2;
                            eDb.CodigoIso3166Alpha3 = iEntidade.CodigoIso3166Alpha3;
                            eDb.CodigoIso3166Numeric = iEntidade.CodigoIso3166Numeric;
                            eDb.InAtivo = true;
                            eDb.Origem = (int)OrigemGeocodificacao.Iso3166;
                            await _paisRepository.UpdateAsync(iEntidade);
                        }
                    }
                }
            }

            Console.WriteLine($"[{_logName}] Seed finalizado.");
        }
    }
}
