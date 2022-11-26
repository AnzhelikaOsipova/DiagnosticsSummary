using DiagnosticsSummary.Api.Models;
using DiagnosticsSummary.Common.Models;
using Microsoft.AspNetCore.Mvc;
using DiagnosticsSummary.Services.Contracts;

namespace DiagnosticsSummary.Api.Controllers
{
    [ApiController]
    [Route(ApiRoutesConstants.ControllersRoutes.DiagnosticInfoApi)]
    public class DiagnosticInfoController : Controller
    {
        private readonly IDiagnosticInfoService _diagnosticInfoService;
        private readonly IDiagnosticService _diagnosticService;

        public DiagnosticInfoController(IDiagnosticInfoService diagnosticInfoService, IDiagnosticService diagnosticService)
        {
            _diagnosticInfoService = diagnosticInfoService;
            _diagnosticService = diagnosticService;
        }

        [HttpGet(ApiRoutesConstants.DiagnosticInfo.Find)]
        public async Task<IActionResult> FindAsync(string name)
        {
            var diagnosticResult = await _diagnosticInfoService.FindAsync(name);
            return diagnosticResult.Match<IActionResult>(
                d => Ok(d),
                e => BadRequest(e.Message)
                );
        }

        [HttpGet(ApiRoutesConstants.DiagnosticInfo.GetAll)]
        public async Task<IActionResult> GetAllAsync()
        {
            var diagnosticsResult = await _diagnosticInfoService.GetAllAsync();
            return diagnosticsResult.Match<IActionResult>(
                ld => Ok(ld),
                e => BadRequest(e.Message)
                );
        }

        [HttpPost(ApiRoutesConstants.DiagnosticInfo.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] DiagnosticInfo newDiagnostic)
        {
            var createResult = await _diagnosticInfoService.CreateAsync(newDiagnostic);
            return createResult.Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);
        }

        [HttpPut(ApiRoutesConstants.DiagnosticInfo.Update)]
        public async Task<IActionResult> UpdateAsync([FromBody] DiagnosticInfo updatedDiagnostic)
        {
            var updateResult = await _diagnosticInfoService.UpdateAsync(updatedDiagnostic);
            return updateResult.Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);
        }

        [HttpDelete(ApiRoutesConstants.DiagnosticInfo.Delete)]
        public async Task<IActionResult> DeleteAsync(string name)
        {
            var deleteResult = await _diagnosticService.FindDiagnosticsAsync(new DiagnosticFilter() { Name = name })
                .MapAsync(ld => ld.Select(d => d.Id))
                .MapAsync(ids => _diagnosticService.DeleteManyAsync(ids));
            var deleteMessage = deleteResult
                .Match<string>(
                oe => oe.Match(e => e.Message, ""),
                e => e.Message
                );

            if (!String.IsNullOrEmpty(deleteMessage))
            {
                return BadRequest(deleteMessage);
            }

            return (await _diagnosticInfoService.DeleteAsync(name)).Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);
        }
    }
}
