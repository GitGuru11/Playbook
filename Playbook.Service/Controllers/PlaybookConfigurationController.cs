using System.Net;
using Microsoft.AspNetCore.Mvc;
using Playbook.Service.Contracts;
using Playbook.Service.Filters;
using Playbook.Service.Handlers;
using Playbook.Web;
using Swashbuckle.AspNetCore.Annotations;
using Playbook.Web.Extension;
using Playbook.Instrumentation.Web;

namespace Playbook.Service.Controllers
{
    [ApiVersion("1.0")]
    public class PlaybookManagementController : BaseController
    {
        [HttpGet]
        [ApiNameFilter("Playbook.Api.PlaybookList")]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaybookList)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [ProducesResponseType(typeof(PlaybookResponse), 200)]
        [SwaggerOperation(Summary = "Playbooks", Tags = new[] { "Playbook Configuration" })]
        public async Task<IActionResult> ListPlaybooksAsync(string? sort_order, string? sort_column, string? search_term,
            int skip = 0, int limit = 10)
        {
            var handler = new PlaybookConfigurationHandler();
            var response = await handler.ProcessGetPlaybooksRequestAsync(sort_order, sort_column, search_term,
                skip, limit);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [ApiNameFilter("Playbook.Api.PlaybookDetail")]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaybookDetail)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [ProducesResponseType(typeof(PlaybookResponse), 200)]
        [SwaggerOperation(Summary = "Playbook Detail", Tags = new[] { "Playbook Configuration" })]
        public async Task<IActionResult> GetPlaybookAsync(string id)
        {
            var handler = new PlaybookConfigurationHandler();
            var response = await handler.ProcessGetPlaybookRequestAsync(id);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost]
        [ApiNameFilter("Playbook.Api.PlaybookCreate")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200)]
        [Route(Routes.PlaybookCreate)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [ProducesResponseType(typeof(Bussiness.Model.Playbook), 200)]
        [SwaggerOperation(Summary = "Playbook Add", Tags = new[] { "Playbook Configuration" })]
        public async Task<IActionResult> AddPlaybokAsync(PlaybookRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new ArgumentNullException(nameof(request.Name));
            }
            if (string.IsNullOrEmpty(request.Description))
            {
                throw new ArgumentNullException(nameof(request.Description));
            }
            var handler = new PlaybookConfigurationHandler();
            var response = await handler.ProcessCreatePlaybookRequestAsync(request);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpPut]
        [ApiNameFilter("Playbook.Api.PlaybookUpdate")]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaybookUpdate)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [SwaggerOperation(Summary = "Playbook Update", Tags = new[] { "Playbook Configuration" })]
        public async Task<IActionResult> UpdatedPlaybookAsync(PlaybookRequest request, string id)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new ArgumentNullException(nameof(request.Name));
            }
            if (string.IsNullOrEmpty(request.Description))
            {
                throw new ArgumentNullException(nameof(request.Description));
            }
            var handler = new PlaybookConfigurationHandler();
            var response = await handler.ProcessUpdatePlaybookRequestAsync(request, id);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpDelete]
        [ApiNameFilter("Playbook.Api.PlaybookDelete")]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaybookDelete)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [SwaggerOperation(Summary = "Playbook Delete", Tags = new[] { "Playbook Configuration" })]
        public async Task<IActionResult> DeletePlaybookAsync(string id)
        {
            var handler = new PlaybookConfigurationHandler();
            var response = await handler.ProcessDeletePlaybookRequestAsync(id);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpPut]
        [ApiNameFilter("Playbook.Api.PlaybookEnable")]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaybookEnable)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [SwaggerOperation(Summary = "Playbook Enable", Tags = new[] { "Playbook Configuration" })]
        public async Task<IActionResult> EnablePlaybookAsync(string id, bool enable)
        {
            var handler = new PlaybookConfigurationHandler();
            var response = await handler.ProcessEnablePlaybookRequestAsync(id, enable);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [ApiNameFilter("Playbook.Api.PlaybookVersions")]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaybookVersions)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [SwaggerOperation(Summary = "Playbook Versions", Tags = new[] { "Playbook Configuration" })]
        public async Task<IActionResult> ListPlaybookVersionsAsync(string id)
        {
            var handler = new PlaybookConfigurationHandler();
            var response = await handler.ProcessGetPlaybookVersionsRequestAsync(id);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }
        
    }
}
