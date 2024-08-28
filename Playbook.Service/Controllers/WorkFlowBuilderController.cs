using System.ComponentModel;
using System.Net;
using JsonApiSerializer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Playbook.Bussiness.Model;
using Playbook.Instrumentation.Web;
using Playbook.Service.Filters;
using Playbook.Service.Handlers;
using Swashbuckle.AspNetCore.Annotations;
using Playbook.Web.Extension;
using Playbook.Engine;
using Query = Playbook.Engine.Query;
using QueryCondition = Playbook.Engine.QueryCondition;

namespace Playbook.Service.Controllers
{
    /// <summary>
    /// Handles operations related to field types and their rule options.
    /// </summary>
    [ApiVersion("1.0")]
    public class WorkFlowBuilderController : BaseController
    {

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaybookNode)]
        [ApiNameFilter("Playbook.Api.AddNode")]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [SwaggerOperation(Summary = "Playbook Tree", Tags = new[] { "Playbook Tree" })]
        public async Task<IActionResult> AddNodeAsync(string playbookid, WorkFlow request)
        {
            var handler = new WorkFlowHandler();
            var response = await handler.AddUpdateNode(playbookid, request);
            return await Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaybookNode)]
        [ApiNameFilter("Playbook.Api.PlaybookVersions")]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [SwaggerOperation(Summary = "Playbook Tree", Tags = new[] { "Playbook Tree" })]
        public async Task<IActionResult> GetPlaybookTreeAsync(string playbookid)
        {
            var handler = new WorkFlowHandler();
            var workFlow = await handler.GetPlaybookNodes(playbookid);
            var res = JsonConvert.SerializeObject(workFlow, new JsonApiSerializerSettings());
            return this.StatusCode(StatusCodes.Status200OK, res);
        }
    }
}
