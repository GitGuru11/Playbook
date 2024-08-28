using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using ClickHouse.Client.ADO;
using Microsoft.AspNetCore.Mvc;
using Playbook.Bussiness.Model;
using Playbook.Common.Tenant;
using Playbook.Instrumentation.Web;
using Playbook.Service.Contracts;
using Playbook.Service.Filters;
using Playbook.Service.Handlers;
using Swashbuckle.AspNetCore.Annotations;
using Playbook.Web.Extension;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Playbook.Service.Controllers
{

    [ApiVersion("1.0")]
    public class ObjectConfigurationController : BaseController
    {
        [HttpGet]
        [ApiNameFilter("Playbook.Api.ObjectType")]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [Route(Routes.ObjectType)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Supported Objects in playbook.", Tags = new[] { "Object Configuration" })]
        public async Task<IActionResult> GetObjectTypeAsync()
        {
            var handler = new ObjectConfigurationHandler();
            var response = await handler.ProcessGetSupportedObjectTypeRequestAsync();
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }
    }
}