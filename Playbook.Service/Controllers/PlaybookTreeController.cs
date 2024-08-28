using System.Net;
using Microsoft.AspNetCore.Mvc;
using Playbook.Instrumentation.Web;
using Playbook.Service.Filters;
using Playbook.Service.Handlers;
using Swashbuckle.AspNetCore.Annotations;
//using Playbook.Service.Contracts.PlaybookTree;
using Playbook.Web.Extension;

namespace Playbook.Service.Controllers
{
    /// <summary>
    /// Handles operations related to field types and their rule options.
    /// </summary>
    [ApiVersion("1.0")]
    public class PlaybookTreeController : BaseController
    {

        // [HttpPost]
        // [MapToApiVersion("1.0")]
        // [Route(Routes.PlaybookNode)]
        // [ApiNameFilter("Playbook.Api.AddNode")]
        // [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        // [SwaggerOperation(Summary = "Playbook Tree", Tags = new[] { "Playbook Tree" })]
        // public async Task<IActionResult> AddNodeAsync(string playbook_id, PlaybookNodeRequest request)
        // {
        //     var handler = new PlaybookTreeHandler();
        //     var response = await handler.ProcessCreateUpdatePlaybookTreeRequestAsync(playbook_id, request);
        //     return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        //
        // }

        // [HttpGet]
        // [MapToApiVersion("1.0")]
        // [Route(Routes.PlaybookNode)]
        // [ApiNameFilter("Playbook.Api.PlaybookVersions")]
        // [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        // [SwaggerOperation(Summary = "Playbook Tree", Tags = new[] { "Playbook Tree" })]
        // public async Task<IActionResult> GetPlaybookTreeAsync(string playbook_id)
        // {
        //     var handler = new PlaybookTreeHandler();
        //     var response = await handler.ProcessGetPlaybookTreeRequestAsync(playbook_id);
        //     return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        // }

    }
}
