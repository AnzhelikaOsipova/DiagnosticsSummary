using DiagnosticsSummary.Api.Models;
using DiagnosticsSummary.Api.Services;
using DiagnosticsSummary.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiagnosticsSummary.Api.Controllers
{
    [ApiController]
    [Route(ApiRoutesConstants.ControllersRoutes.DiagnosticInfoApi)]
    public class DiagnosticInfoController : Controller
    {
        private readonly DiagnosticInfoService diagnosticInfoService;
        private readonly DiagnosticService diagnosticService;

        public DiagnosticInfoController(DiagnosticInfoService diagnosticInfoService, DiagnosticService diagnosticService)
        {
            this.diagnosticInfoService = diagnosticInfoService;
            this.diagnosticService = diagnosticService;
        }

        [HttpGet(ApiRoutesConstants.DiagnosticInfo.Find)]
        public async Task<IActionResult> FindAsync(string name) =>
            (await diagnosticInfoService.FindAsync(name)).Match<IActionResult>(
                d => Ok(d),
                e => BadRequest(e.Message)
                );

        [HttpGet(ApiRoutesConstants.DiagnosticInfo.GetAll)]
        public async Task<IActionResult> GetAllAsync() =>
            (await diagnosticInfoService.GetAllAsync()).Match<IActionResult>(
                ld => Ok(ld),
                e => BadRequest(e.Message)
                );

        [HttpPut(ApiRoutesConstants.DiagnosticInfo.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] DiagnosticInfo newDiagnostic) =>
            (await diagnosticInfoService.CreateAsync(newDiagnostic)).Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);

        [HttpPost(ApiRoutesConstants.DiagnosticInfo.Update)]
        public async Task<IActionResult> UpdateChildAsync([FromBody] DiagnosticInfo updatedDiagnostic) =>
            (await diagnosticInfoService.UpdateAsync(updatedDiagnostic)).Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);

        [HttpDelete(ApiRoutesConstants.DiagnosticInfo.Delete)]
        public async Task<IActionResult> DeleteChildAsync(string name)
        {
            var dels = (await diagnosticService.FindDiagnosticsAsync(new Diagnostic() { Name = name })
                .MapAsync(ld => ld.Select(d => d.Id))
                .MapAsync(ids => diagnosticService.DeleteManyAsync(ids)))
                .Match<string>(
                oe => oe.Match(e => e.Message, ""),
                e => e.Message
                );
            if (!String.IsNullOrEmpty(dels)) return BadRequest(dels);
            return (await diagnosticInfoService.DeleteAsync(name)).Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);
        }
    }
}
