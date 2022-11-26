using DiagnosticsSummary.Api.Models;
using DiagnosticsSummary.Common.Models;
using Microsoft.AspNetCore.Mvc;
using DiagnosticsSummary.Services.Contracts;

namespace DiagnosticsSummary.Api.Controllers
{
    [ApiController]
    [Route(ApiRoutesConstants.ControllersRoutes.DiagnosticApi)]
    public class DiagnosticController: Controller
    {
        private readonly IDiagnosticInfoService _diagnosticInfoService;
        private readonly IDiagnosticService _diagnosticService;

        public DiagnosticController(IDiagnosticService diagnosticService, IDiagnosticInfoService diagnosticInfoService)
        {
            _diagnosticService = diagnosticService;
            _diagnosticInfoService = diagnosticInfoService;
        }

        [HttpGet(ApiRoutesConstants.Diagnostic.Find)]
        public async Task<IActionResult> FindDiagnosticsAsync([FromQuery] DiagnosticFilter filter)
        {
            var diasnosticsInterpretedResult = await _diagnosticInfoService.GetAllAsync()
                .BindAsync(ldi => _diagnosticService.FindDiagnosticsAsync(filter)
                    .MapAsync(ld =>
                     {
                        ld.ForEach(d => d.InterpretedValue =
                            ldi.FirstOrDefault(di => di.Name == d.Name)!.Interpret(d.Value));
                        return ld;
                     }));
            return diasnosticsInterpretedResult
                .Match<IActionResult>(
                    ld => Ok(ld),
                    e => BadRequest(e.Message)
                );
        }

        [HttpPost(ApiRoutesConstants.Diagnostic.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] Diagnostic newDiagnostic)
        {
            var createResult = await _diagnosticService.CreateAsync(newDiagnostic);
            return createResult.Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);
        }

        [HttpPut(ApiRoutesConstants.Diagnostic.Update)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] DiagnosticFilter updates)
        {
            var updateResult = await _diagnosticService.UpdateAsync(id, updates);
            return updateResult.Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);
        }

        [HttpDelete(ApiRoutesConstants.Diagnostic.Delete)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResult = await _diagnosticService.DeleteAsync(id);
            return deleteResult.Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);
        }
    }
}
