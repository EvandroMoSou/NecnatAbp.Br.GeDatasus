using NecnatAbp.AppServices;
using NecnatAbp.Br.GeGeocodificacao.Permissions;
using NecnatAbp.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class PaisAppService :
        CrudsAppService<
            Pais,
            PaisDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdatePaisDto,
            PaisResultRequestDto>,
        IPaisAppService
    {
        public PaisAppService(IPaisRepository repository) : base(repository)
        {
            GetPolicyName = GeGeocodificacaoPermissions.Paises.Default;
            GetListPolicyName = GeGeocodificacaoPermissions.Paises.Default;
            CreatePolicyName = GeGeocodificacaoPermissions.Paises.Create;
            UpdatePolicyName = GeGeocodificacaoPermissions.Paises.Update;
            DeletePolicyName = GeGeocodificacaoPermissions.Paises.Delete;
        }

        /// <summary>
        /// O filtro GenericSearch apresenta a seguinte linha de acao:
        ///     - 2 caracteres: Realiza a busca pelo CodigoIso3166Alpha2.
        ///     - 3 caracteres numericos: Realiza a busca pelo CodigoIso3166Numeric.
        ///     - 3 caracteres alfanumericos: Realiza a busca pelo CodigoIso3166Alpha3.
        ///     - 4+ caracteres alfanumericos: Realiza a busca conjuta pelo NomeContains, NomeInglesContains e NomeFrancesContains.
        /// </summary>
        protected override async Task<IQueryable<Pais>> CreateFilteredQuerySearchAsync(PaisResultRequestDto input)
        {
            var q = await ReadOnlyRepository.GetQueryableAsync();

            if (!string.IsNullOrWhiteSpace(input.GenericSearch))
            {
                if (input.GenericSearch.Length < 2)
                    throw new UserFriendlyException("O filtro GenericSearch deve conter no mínimo 2 caracteres.");
                if (input.GenericSearch.Length == PaisConsts.MaxCodigoIso3166Alpha2Length)
                    input.CodigoIso3166Alpha2 = input.GenericSearch;
                else if (input.GenericSearch.Length == 3)
                {
                    if (input.GenericSearch.All(char.IsDigit))
                        input.CodigoIso3166Numeric = input.GenericSearch;
                    else
                        input.CodigoIso3166Alpha3 = input.GenericSearch;
                }
                else
                {
                    q = q.Where(x => x.Nome.Contains(input.GenericSearch)
                    || (x.NomeIngles != null && x.NomeIngles.Contains(input.GenericSearch))
                    || (x.NomeFrances != null && x.NomeFrances.Contains(input.GenericSearch)));
                }
            }

            if (!string.IsNullOrWhiteSpace(input.NomeContains))
            {
                if (input.NomeContains.Length < 3)
                    throw new UserFriendlyException("O filtro NomeContains deve conter no mínimo 3 caracteres.");

                q = q.Where(x => x.Nome.Contains(input.NomeContains));
            }

            if (!string.IsNullOrWhiteSpace(input.NomeInglesContains))
            {
                if (input.NomeInglesContains.Length < 3)
                    throw new UserFriendlyException("O filtro NomeInglesContains deve conter no mínimo 3 caracteres.");

                q = q.Where(x => x.NomeIngles != null && x.NomeIngles.Contains(input.NomeInglesContains));
            }

            if (!string.IsNullOrWhiteSpace(input.NomeFrancesContains))
            {
                if (input.NomeFrancesContains.Length < 3)
                    throw new UserFriendlyException("O filtro NomeFrancesContains deve conter no mínimo 3 caracteres.");

                q = q.Where(x => x.NomeFrances != null && x.NomeFrances.Contains(input.NomeFrancesContains));
            }

            if (!string.IsNullOrWhiteSpace(input.CodigoIso3166Alpha2))
            {
                input.CodigoIso3166Alpha2 = input.CodigoIso3166Alpha2.OnlyLetters();
                if (input.CodigoIso3166Alpha2.Length != PaisConsts.MaxCodigoIso3166Alpha2Length)
                    throw new UserFriendlyException($"O filtro CodigoIso3166Alpha2 deve conter {PaisConsts.MaxCodigoIso3166Alpha2Length} caracteres alfabéticos.");

                q = q.Where(x => x.CodigoIso3166Alpha2 != null && x.CodigoIso3166Alpha2 == input.CodigoIso3166Alpha2);
            }

            if (!string.IsNullOrWhiteSpace(input.CodigoIso3166Alpha3))
            {
                input.CodigoIso3166Alpha3 = input.CodigoIso3166Alpha3.OnlyLetters();
                if (input.CodigoIso3166Alpha3.Length != PaisConsts.MaxCodigoIso3166Alpha3Length)
                    throw new UserFriendlyException($"O filtro CodigoIso3166Alpha3 deve conter {PaisConsts.MaxCodigoIso3166Alpha3Length} caracteres alfabéticos.");

                q = q.Where(x => x.CodigoIso3166Alpha3 != null && x.CodigoIso3166Alpha3 == input.CodigoIso3166Alpha3);
            }

            if (!string.IsNullOrWhiteSpace(input.CodigoIso3166Numeric))
            {
                input.CodigoIso3166Numeric = input.CodigoIso3166Numeric.OnlyDigits();
                if (input.CodigoIso3166Numeric.Length != PaisConsts.MaxCodigoIso3166NumericLength)
                    throw new UserFriendlyException($"O filtro CodigoIso3166Numeric deve conter {PaisConsts.MaxCodigoIso3166NumericLength} caracteres numéricos.");

                q = q.Where(x => x.CodigoIso3166Numeric != null && x.CodigoIso3166Numeric == input.CodigoIso3166Numeric);
            }

            if (input.InAtivo != null)
                q = q.Where(x => x.InAtivo == input.InAtivo);

            return q;
        }
    }
}
