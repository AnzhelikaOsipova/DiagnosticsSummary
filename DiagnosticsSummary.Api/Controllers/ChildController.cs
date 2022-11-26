using DiagnosticsSummary.Common.Models;
using Microsoft.AspNetCore.Mvc;
using DiagnosticsSummary.Api.Models;
using LanguageExt;
using DiagnosticsSummary.Services.Contracts;

namespace DiagnosticsSummary.Api.Controllers
{
    [ApiController]
    [Route(ApiRoutesConstants.ControllersRoutes.ChildApi)]
    public class ChildController : Controller
    {
        private readonly IChildService _childService;
        private readonly IDiagnosticService _diagnosticService;

        public ChildController(IChildService childService, IDiagnosticService diagnosticService)
        {
            _childService = childService;
            _diagnosticService = diagnosticService;
        }

        [HttpGet(ApiRoutesConstants.Child.Find)]
        public async Task<IActionResult> FindChildrenAsync([FromQuery] ChildFilter filter)
        {
            var childrenResult = await _childService.FindChildrenAsync(filter);
            return childrenResult.Match<IActionResult>(
                lch => Ok(lch),
                e => BadRequest(e.Message)
                );
        }

        [HttpPost(ApiRoutesConstants.Child.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] Child newChild)
        {
            var createResult = await _childService.CreateAsync(newChild);
            return createResult.Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);
        }

        [HttpPut(ApiRoutesConstants.Child.Update)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ChildFilter updatedChild)
        {
            var updateResult = await _childService.UpdateAsync(id, updatedChild);
            return updateResult.Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);
        }

        [HttpDelete(ApiRoutesConstants.Child.Delete)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var deleteResult = await _diagnosticService.FindDiagnosticsAsync(new DiagnosticFilter() { ChildId = id })
                .MapAsync(ld => ld.Select(d => d.Id))
                .MapAsync(ids => _diagnosticService.DeleteManyAsync(ids));

            var deleteMessage = deleteResult.Match<string>(
                oe => oe.Match(e => e.Message, ""),
                e => e.Message
                );

            if (!string.IsNullOrEmpty(deleteMessage))
            {
                return BadRequest(deleteMessage);
            }

            return (await _childService.DeleteAsync(id)).Match<IActionResult>(
                e => BadRequest(e.Message),
                Ok);
        }
    }
}
