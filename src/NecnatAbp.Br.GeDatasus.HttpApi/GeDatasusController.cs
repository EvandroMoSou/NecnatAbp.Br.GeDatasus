using NecnatAbp.Br.GeDatasus.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace NecnatAbp.Br.GeDatasus;

public abstract class GeDatasusController : AbpControllerBase
{
    protected GeDatasusController()
    {
        LocalizationResource = typeof(GeDatasusResource);
    }
}
