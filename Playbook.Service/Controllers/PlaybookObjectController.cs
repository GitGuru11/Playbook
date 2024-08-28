using System.Net;
using Microsoft.AspNetCore.Mvc;
using Playbook.Bussiness.Model;
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
    public class PlaybookObjectController : BaseController
    {
        [HttpGet]
        [ApiNameFilter("Playbook.Api.PlaybookObjectsList")]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaybookObjectsList)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [ProducesResponseType(typeof(PlaybookObjectResponse), 200)]
        [SwaggerOperation(Summary = "Playbook Objects List", Tags = new[] { "Playbook Object API" })]
        public async Task<IActionResult> ListPlaybookObjectAsync(string? sort_order, string? sort_column, string? search_term,
            int skip = 0, int limit = 10)
        {
            var handler = new PlaybookObjectHandler();
            var response = await handler.ProcessGetPlaybookObjectRequestAsync(sort_order, sort_column, search_term,
                skip, limit);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [ApiNameFilter("Playbook.Api.PlaybookObjectDetail")]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaybookObjectDetail)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [ProducesResponseType(typeof(PlaybookObject), 200)]
        [SwaggerOperation(Summary = "Playbook Object Detail", Tags = new[] { "Playbook Object API" })]
        public async Task<IActionResult> GetPlaybookObjectAsync(string id)
        {
            var handler = new PlaybookObjectHandler();
            var response = await handler.ProcessGetPlaybookObjectRequestAsync(id);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [ApiNameFilter("Playbook.Api.PlaybookObjectCreate")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200)]
        [Route(Routes.PlaybookObjectCreate)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [ProducesResponseType(typeof(PlaybookObject), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [SwaggerOperation(Summary = "Playbook Object Add", Tags = new[] { "Playbook Object API" })]
        public async Task<IActionResult> AddPlaybookObjectAsync(PlaybookObjectRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return await this.CreateMissingParameterErrorResponseAsync(HttpStatusCode.BadRequest, nameof(request.Name));
            }
            if (string.IsNullOrEmpty(request.Description))
            {
                return await this.CreateMissingParameterErrorResponseAsync(HttpStatusCode.BadRequest, nameof(request.Description));
            }
            if (request.ObjectTypeId != 1 && request.ObjectTypeId != 2 && request.ObjectTypeId != 3 && request.ObjectTypeId != 4)
            {
                return await this.CreateMissingParameterErrorResponseAsync(HttpStatusCode.BadRequest, nameof(request.ObjectTypeId));
            }
            if (string.IsNullOrEmpty(request.Category) ||
                                        !(Enum.TryParse(typeof(FieldCategory), request.Category.ToUpper(), true, out var parsedCategory)
                                        && Enum.IsDefined(typeof(FieldCategory), parsedCategory))) 
            {
                return await this.CreateMissingParameterErrorResponseAsync(HttpStatusCode.BadRequest, nameof(request.Category));
            }
            var handler = new PlaybookObjectHandler();
            var response = await handler.ProcessCreatePlaybookObjectRequestAsync(request);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpPut]
        [ApiNameFilter("Playbook.Api.PlaybookObjectUpdate")]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaybookObjectUpdate)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [SwaggerOperation(Summary = "Playbook Object Update", Tags = new[] { "Playbook Object API" })]
        public async Task<IActionResult> UpdatePlaybookObjectAsync(PlaybookObjectRequest request, string id)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return await this.CreateMissingParameterErrorResponseAsync(HttpStatusCode.BadRequest, nameof(request.Name));
            }
            if (string.IsNullOrEmpty(request.Description))
            {
                return await this.CreateMissingParameterErrorResponseAsync(HttpStatusCode.BadRequest, nameof(request.Description));
            }
            if (request.ObjectTypeId != 1 && request.ObjectTypeId != 2 && request.ObjectTypeId != 3 && request.ObjectTypeId != 4)
            {
                return await this.CreateMissingParameterErrorResponseAsync(HttpStatusCode.BadRequest, nameof(request.ObjectTypeId));
            }
            if (string.IsNullOrEmpty(request.Category) ||
                                        !(Enum.TryParse(typeof(FieldCategory), request.Category.ToUpper(), true, out var parsedCategory)
                                        && Enum.IsDefined(typeof(FieldCategory), parsedCategory)))
            {
                return await this.CreateMissingParameterErrorResponseAsync(HttpStatusCode.BadRequest, nameof(request.Category));
            }
            var handler = new PlaybookObjectHandler();
            var response = await handler.ProcessUpdatePlaybookObjectRequestAsync(request, id);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpDelete]
        [ApiNameFilter("Playbook.Api.PlaybookObjectDelete")]
        [MapToApiVersion("1.0")]
        [Route(Routes.PlaybookObjectDelete)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [SwaggerOperation(Summary = "Playbook Object Delete", Tags = new[] { "Playbook Object API" })]
        public async Task<IActionResult> DeletePlaybookObjectAsync(string id)
        {
            var handler = new PlaybookObjectHandler();
            var response = await handler.ProcessDeletePlaybookObjectRequestAsync(id);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }
    }
}
