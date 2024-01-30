using NecnatAbp.Br.GeDatasus.Localization;
using Volo.Abp.Application.Services;

namespace NecnatAbp.Br.GeDatasus;

public abstract class GeDatasusAppService : ApplicationService
{
    protected GeDatasusAppService()
    {
        LocalizationResource = typeof(GeDatasusResource);
        ObjectMapperContext = typeof(GeDatasusApplicationModule);
    }
}
