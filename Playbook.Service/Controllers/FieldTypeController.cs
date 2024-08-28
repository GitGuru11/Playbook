using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Playbook.Bussiness.Model;
using Playbook.Instrumentation.Web;
using Playbook.Service.Contracts;
using Playbook.Service.Filters;
using Playbook.Service.Handlers;
using Swashbuckle.AspNetCore.Annotations;
using Playbook.Web.Extension;


namespace Playbook.Service.Controllers
{
    /// <summary>
    /// Handles operations related to field types and their rule options.
    /// </summary>
    [ApiVersion("1.0")]
    public class FieldTypeController : BaseController
    {

        // Instantiate the handler to process the request
        FieldTypeHandler fieldTypeHandler = new FieldTypeHandler();

        /// <summary>
        /// Gets all field types and their corresponding rule options.
        /// </summary>
        /// <returns>A list of field types with their rule options.</returns>
        [HttpGet]
        [ApiNameFilter("Playbook.Api.FieldTypes")]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [Route(Routes.FieldTypes)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Retrieve all field types", Tags = new[] { "Field Type Apies" })]
        public async Task<IActionResult> GetFieldTypesAsync()
        {
            // Call the handler method to get the field types and rules
            var response = await fieldTypeHandler.ProcessFieldTypesRequestAsync();

            // Return the response with HTTP status 200 OK
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }


        /// <summary>
        /// Gets all field types and their corresponding rule options.
        /// </summary>
        /// <returns>A list of field types with their rule options.</returns>
        [HttpGet]
        [ApiNameFilter("Playbook.Api.FieldTypeRules")]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [Route(Routes.FieldTypeRules)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Retrieve all field types and their rule options", Tags = new[] { "Field Type Apies" })]
        public async Task<IActionResult> GetFieldTypeRulesAsync()
        {
            // Call the handler method to get the field types and rules
            var response = await fieldTypeHandler.ProcessFieldTypesRulesRequestAsync();

            // Return the response with HTTP status 200 OK
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);
        }

        /// <summary>
        /// Gets all field types and their corresponding rule options.
        /// </summary>
        /// <returns>A list of field types with their rule options.</returns>
        [HttpGet]
        [ApiNameFilter("Playbook.Api.RulesByFieldType")]
        [SwaggerOperationFilter(typeof(SwaggerHeaderFilter))]
        [Route(Routes.RulesByFieldType)]
        [MapToApiVersion("1.0")]
        [SwaggerOperation(Summary = "Retrieve all rules for field type", Tags = new[] { "Field Type Apies" })]
        public async Task<IActionResult> GetRulesByFieldTypeAsync(string fieldtype)
        {
            // Call the handler method to get the field types and rules
            var response = await fieldTypeHandler.ProcessRulesByFieldTypeequestAsync(fieldtype);

            // Return the response with HTTP status 200 OK
            return await this.Request.CreateResponseAsync(HttpStatusCode.OK, response);

        }
    }
}
