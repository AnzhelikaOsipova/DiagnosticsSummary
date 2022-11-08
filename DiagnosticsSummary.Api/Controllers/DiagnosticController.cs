using DiagnosticsSummary.Api.Models;
using DiagnosticsSummary.Api.Services;
using DiagnosticsSummary.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiagnosticsSummary.Api.Controllers
{
    [ApiController]
    [Route(ApiRoutesConstants.ControllersRoutes.DiagnosticApi)]
    public class DiagnosticController: Controller
    {
        private readonly DiagnosticInfoService diagnosticInfoService;
        private readonly DiagnosticService diagnosticService;

        public DiagnosticController(DiagnosticService diagnosticService, DiagnosticInfoService diagnosticInfoService)
        {
            this.diagnosticService = diagnosticService;
            this.diagnosticInfoService = diagnosticInfoService;
        }

        [HttpPost(ApiRoutesConstants.Diagnostic.Find)]
        public async Task<IActionResult> FindDiagnosticsAsync([FromBody] Diagnostic filter) =>
            (await diagnosticInfoService.GetAllAsync()
            .BindAsync(ldi => diagnosticService.FindDiagnosticsAsync(filter)
                    .MapAsync(ld =>
                    {
                        ld.ForEach(d => d.InterpretedValue =
                            ldi.FirstOrDefault(di => di.Name == d.Name)!.Interpret(d.Value!.Value));
                        return ld;
                    })))
             .Match<IActionResult>(
                ld => Ok(ld),
                e => BadRequest(e.Message)
                );

            //return (await diagnosticService.FindDiagnosticsAsync(filter))
            // .Match<IActionResult>(
            //    ld => Ok(ld),
            //    e => BadRequest(e.Message)
            //    );
        

        [HttpPut(ApiRoutesConstants.Diagnostic.Create)]
        public async Task<IActionResult> CreateDiagnosticAsync([FromBody] Diagnostic newDiagnostic) =>
            (await diagnosticService.CreateAsync(newDiagnostic)).Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);

        [HttpPost(ApiRoutesConstants.Diagnostic.Update)]
        public async Task<IActionResult> UpdateDiagnosticAsync(Diagnostic updatedDiagnostic) =>
            (await diagnosticService.UpdateAsync(updatedDiagnostic)).Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);

        [HttpDelete(ApiRoutesConstants.Diagnostic.Delete)]
        public async Task<IActionResult> DeleteDiagnosticAsync(int id) =>
            (await diagnosticService.DeleteAsync(id)).Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);
    }
}
