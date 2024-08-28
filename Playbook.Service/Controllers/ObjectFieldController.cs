using Microsoft.AspNetCore.Mvc;
using Playbook.Bussiness.Model;
using Playbook.Instrumentation.Web;
using Playbook.Service.Contracts;
using Playbook.Service.Filters;
using Playbook.Service.Handlers;
using Swashbuckle.AspNetCore.Annotations;
using Playbook.Web.Extension;
using System.Net;
using Playbook.Web;

namespace Playbook.Service.Controllers
{
    [ApiVersion("1.0")]
    public class ObjectFieldController : BaseController
    {
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ApiNameFilter("Playbook.Api.ObjectsFieldList")]
        [Route(Routes.ObjectsFieldList)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [ProducesResponseType(typeof(ObjectFieldResponse), 200)]
        [SwaggerOperation(Summary = "Object Fields", Tags = new[] { "Object Field API" })]
        public async Task<IActionResult> ListObjectFieldAsync(string? sort_order, string? sort_column, string? search_term,
            int skip = 0, int limit = 10)
        {
            var handler = new ObjectFieldHandler();
            var response = await handler.ProcessGetObjectFieldRequestAsync(sort_order, sort_column, search_term,
                skip, limit);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [ApiNameFilter("Playbook.Api.ObjectsFieldDetail")]
        [MapToApiVersion("1.0")]
        [Route(Routes.ObjectsFieldDetail)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [ProducesResponseType(typeof(ObjectFieldResponse), 200)]
        [SwaggerOperation(Summary = "Object Field Detail", Tags = new[] { "Object Field API" })]
        public async Task<IActionResult> GetObjectFieldAsync(string id)
        {
            var handler = new ObjectFieldHandler();
            var response = await handler.ProcessGetObjectFieldRequestAsync(id);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [ApiNameFilter("Playbook.Api.ObjectsFieldCreate")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200)]
        [Route(Routes.ObjectsFieldCreate)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [ProducesResponseType(typeof(ObjectField), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [SwaggerOperation(Summary = "Object Field Add", Tags = new[] { "Object Field API" })]
        public async Task<IActionResult> AddObjectFieldAsync(ObjectFieldRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return await this.CreateMissingParameterErrorResponseAsync(HttpStatusCode.BadRequest, nameof(request.Name));
            }
            if (string.IsNullOrEmpty(request.Label))
            {
                return await this.CreateMissingParameterErrorResponseAsync(HttpStatusCode.BadRequest, nameof(request.Label));
            }
            var handler = new ObjectFieldHandler();
            var response = await handler.ProcessCreateObjectFieldRequestAsync(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut]
        [ApiNameFilter("Playbook.Api.ObjectsFieldUpdate")]
        [MapToApiVersion("1.0")]
        [Route(Routes.ObjectsFieldUpdate)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [SwaggerOperation(Summary = "Object Field Update", Tags = new[] { "Object Field API" })]
        public async Task<IActionResult> UpdatedObjectFieldAsync(ObjectFieldRequest request, string id)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return await this.CreateMissingParameterErrorResponseAsync(HttpStatusCode.BadRequest, nameof(request.Name));
            }
            if (string.IsNullOrEmpty(request.Label))
            {
                return await this.CreateMissingParameterErrorResponseAsync(HttpStatusCode.BadRequest, nameof(request.Label));
            }
            var handler = new ObjectFieldHandler();
            var response = await handler.ProcessUpdateObjectFieldRequestAsync(request, id);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpDelete]
        [ApiNameFilter("Playbook.Api.ObjectsFieldDelete")]
        [MapToApiVersion("1.0")]
        [Route(Routes.ObjectsFieldDelete)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [SwaggerOperation(Summary = "Object Field Delete", Tags = new[] { "Object Field API" })]
        public async Task<IActionResult> DeleteObjectFieldAsync(string id)
        {
            var handler = new ObjectFieldHandler();
            var response = await handler.ProcessDeleteObjectFieldRequestAsync(id);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }
    }
}
