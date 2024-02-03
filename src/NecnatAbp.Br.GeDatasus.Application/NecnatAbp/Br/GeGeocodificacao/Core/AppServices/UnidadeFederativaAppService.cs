using EnumsNET;
using NecnatAbp.Br.GeDatasus;
using NecnatAbp.Br.GeGeocodificacao.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class UnidadeFederativaAppService : GeDatasusAppService, IUnidadeFederativaAppService
    {
        public async Task<List<UnidadeFederativaDto>> GetAsync()
        {
            await CheckGetListPolicyAsync();

            var l = new List<UnidadeFederativaDto>();
            foreach (UnidadeFederativa iUnidadeFederativa in Enum.GetValues(typeof(UnidadeFederativa)))
                l.Add(new UnidadeFederativaDto
                {
                    CodigoIbge = Convert.ToInt16((int)iUnidadeFederativa),
                    Sigla = iUnidadeFederativa.ToString(),
                    Nome = (iUnidadeFederativa).AsString(EnumFormat.Description)
                });

            return l;
        }

        protected virtual async Task CheckGetListPolicyAsync()
        {
            await CheckPolicyAsync(GeGeocodificacaoPermissions.UnidadesFederativas.Default);
        }
    }
}
