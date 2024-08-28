using System.Net;
using Microsoft.AspNetCore.Mvc;
using Playbook.Business.Model;
using Playbook.Service.Filters;
using Playbook.Service.Handlers;
using Playbook.Web;
using Swashbuckle.AspNetCore.Annotations;
using Playbook.Web.Extension;
using Playbook.Instrumentation.Web;
using Playbook.Bussiness.Model;

namespace Playbook.Service.Controllers
{
    [ApiVersion("1.0")]
    public class DBConfigurationController : BaseController
    {

        [HttpGet]
        [ApiNameFilter("DB.Api.DBInformationGet")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200)]
        [Route(Routes.CredentialGet)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [ProducesResponseType(typeof(DBInformation), 200)]
        [SwaggerOperation(Summary = "DB Information Get", Tags = new[] { "DB Configuration" })]
        public async Task<IActionResult> AddDBInformationAsync(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new ArgumentNullException(nameof(Name));
            }
            var handler = new DBConfigurationHandler();
            var response = await handler.ProcessGetDBInformationRequestAsync(Name);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [ApiNameFilter("DB.Api.DBInformationCreate")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200)]
        [Route(Routes.CredentialAdd)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [ProducesResponseType(typeof(DBInformation), 200)]
        [SwaggerOperation(Summary = "DB Information Add", Tags = new[] { "DB Configuration" })]
        public async Task<IActionResult> AddDBInformationAsync(DBInformation request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new ArgumentNullException(nameof(request.Name));
            }
            if (string.IsNullOrEmpty(request.Shard))
            {
                throw new ArgumentNullException(nameof(request.Shard));
            }
            var handler = new DBConfigurationHandler();
            var response = await handler.ProcessCreateDBInformationRequestAsync(request);
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpPut]
        [ApiNameFilter("DB.Api.DBInformationUpdate")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200)]
        [Route(Routes.CredentialUpdate)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [SwaggerOperation(Summary = "DB Information Update", Tags = new[] { "DB Configuration" })]
        public async Task<IActionResult> UpdateDBInformationAsync(string Name, DBInformation request)
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new ArgumentNullException(nameof(Name));
            }

            var handler = new DBConfigurationHandler();
            var response = await handler.ProcessUpdateDBInformationRequestAsync(Name,request);

            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        [HttpDelete]
        [ApiNameFilter("DB.Api.DBInformationDelete")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200)]
        [Route(Routes.CredentialDelete)]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [SwaggerOperation(Summary = "DB Information Delete", Tags = new[] { "DB Configuration" })]
        public async Task<IActionResult> DeleteDBInformationAsync(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new ArgumentNullException(nameof(Name));
            }

            var handler = new DBConfigurationHandler();
            var response = await handler.ProcessDeleteDBInformationRequestAsync(Name);

            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

    }
}
