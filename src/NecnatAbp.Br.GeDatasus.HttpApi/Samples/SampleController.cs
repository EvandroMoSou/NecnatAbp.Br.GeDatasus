using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace NecnatAbp.Br.GeDatasus.Samples;

[Area(GeDatasusRemoteServiceConsts.ModuleName)]
[RemoteService(Name = GeDatasusRemoteServiceConsts.RemoteServiceName)]
[Route("api/GeDatasus/sample")]
public class SampleController : GeDatasusController, ISampleAppService
{
    private readonly ISampleAppService _sampleAppService;

    public SampleController(ISampleAppService sampleAppService)
    {
        _sampleAppService = sampleAppService;
    }

    [HttpGet]
    public async Task<SampleDto> GetAsync()
    {
        return await _sampleAppService.GetAsync();
    }

    [HttpGet]
    [Route("authorized")]
    [Authorize]
    public async Task<SampleDto> GetAuthorizedAsync()
    {
        return await _sampleAppService.GetAsync();
    }
}
