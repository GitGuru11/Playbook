
namespace Playbook.Web.Extension
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Threading.Tasks;

    public static class HttpRequestExtenstion
    {

        // <summary>
        /// Get uri from the input Http request
        /// </summary>
        /// <param name="request">Http Request</param>
        /// <returns>Uri formulated from the request</returns>
        public static Uri GetUriFromRequest(this HttpRequest request)
        {
            Uri uri = null;

            if (request != null)
            {
                Uri.TryCreate(string.Format(CultureInfo.InvariantCulture, "{0}://{1}{2}{3}", request.Scheme, request.Host, request.Path, request.QueryString), UriKind.RelativeOrAbsolute, out uri);
            }

            return uri;
        }


        // <summary>
        /// Get uri from the input Http request
        /// </summary>
        /// <param name="request">Http Request</param>
        /// <returns>Uri formulated from the request</returns>
        public static Uri GetHostFromRequest(this HttpRequest request)
        {
            Uri uri = null;

            if (request != null)
            {
                Uri.TryCreate(string.Format(CultureInfo.InvariantCulture, "{0}://{1}", request.Scheme, request.Host), UriKind.RelativeOrAbsolute, out uri);
            }

            return uri;
        }

        public async static Task<IActionResult> CreateResponseAsync(this HttpRequest request, HttpStatusCode statusCode, string reasonPhrase = null)
        {
            var result = new ObjectResult(reasonPhrase ?? string.Empty)
            {
                StatusCode = (int)statusCode
            };
            return await Task.FromResult(result);
        }

        public static IActionResult CreateResponse(this HttpRequest request, HttpStatusCode statusCode, string reasonPhrase = null)
        {
            var result = new ObjectResult(reasonPhrase ?? string.Empty)
            {
                StatusCode = (int)statusCode
            };
            return result;
        }

        public async static Task<IActionResult> CreateResponseAsync(this HttpRequest request, HttpStatusCode statusCode, object result)
        {
            if (result != null)
            {
                var response = new ObjectResult(result)
                {
                    StatusCode = (int)statusCode
                };
                return await Task.FromResult(response);
            }

            return await Task.FromResult(new StatusCodeResult((int)statusCode));
        }

        public static IActionResult CreateResponse(this HttpRequest request, HttpStatusCode statusCode, object result)
        {
            if (result != null)
            {
                var response = new ObjectResult(result)
                {
                    StatusCode = (int)statusCode
                };
                return response;
            }

            return new StatusCodeResult((int)statusCode);
        }

        public async static Task<IActionResult> CreateErrorResponseAsync(this HttpRequest request, HttpStatusCode statusCode, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var result = new { error = message };
                var response = new ObjectResult(result)
                {
                    StatusCode = (int)statusCode
                };
                return await Task.FromResult(response);
            }

            return await Task.FromResult(new StatusCodeResult((int)statusCode));
        }


        public static IActionResult CreateErrorResponse(this HttpRequest request, HttpStatusCode statusCode, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var result = new { error = message };
                var response = new ObjectResult(result)
                {
                    StatusCode = (int)statusCode
                };
                return response;
            }

            return new StatusCodeResult((int)statusCode);
        }
    }
}
