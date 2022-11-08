using DiagnosticsSummary.Api.Services;
using DiagnosticsSummary.Common.Models;
using Microsoft.AspNetCore.Mvc;
using DiagnosticsSummary.Api.Models;
using LanguageExt;

namespace DiagnosticsSummary.Api.Controllers
{
    [ApiController]
    [Route(ApiRoutesConstants.ControllersRoutes.ChildApi)]
    public class ChildController : Controller
    {
        private readonly ChildService childService;
        private readonly DiagnosticService diagnosticService;

        public ChildController(ChildService childService, DiagnosticService diagnosticService)
        {
            this.childService = childService;
            this.diagnosticService = diagnosticService;
        }

        [HttpPost(ApiRoutesConstants.Child.Find)]
        public async Task<IActionResult> FindChildrenAsync([FromBody] Child filter) =>
            (await childService.FindChildrenAsync(filter)).Match<IActionResult>(
                ch => Ok(ch),
                e => BadRequest(e.Message)
                );

        [HttpPut(ApiRoutesConstants.Child.Create)]
        public async Task<IActionResult> CreateChildAsync([FromBody] Child newChild) =>
            (await childService.CreateAsync(newChild)).Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);

        [HttpPost(ApiRoutesConstants.Child.Update)]
        public async Task<IActionResult> UpdateChildAsync(Child updatedChild) =>
            (await childService.UpdateAsync(updatedChild)).Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);

        [HttpDelete(ApiRoutesConstants.Child.Delete)]
        public async Task<IActionResult> DeleteChildAsync(int id)
        {
            var dels = (await diagnosticService.FindDiagnosticsAsync(new Diagnostic() { ChildId = id })
                .MapAsync(ld => ld.Select(d => d.Id))
                .MapAsync(ids => diagnosticService.DeleteManyAsync(ids)))
                .Match<string>(
                oe => oe.Match(e => e.Message, ""),
                e => e.Message
                );
            if (!String.IsNullOrEmpty(dels)) return BadRequest(dels);
            return (await childService.DeleteAsync(id)).Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);
        }
    }
}
