using Playbook.Service.Contracts;
using Playbook.Web;
using System.Net;

namespace Playbook.Service
{
    public class PlaybookErrorResponseException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; private set; }
        public ErrorResponse ErrorResponse { get; private set; }

        public string ReasonPharse { get; private set; }

        public PlaybookErrorResponseException(HttpStatusCode httpStatusCode, ErrorResponse errorResponse) : base(errorResponse.ErrorMessage)
        {
            this.HttpStatusCode = httpStatusCode;
            this.ErrorResponse = errorResponse;
            this.ReasonPharse = errorResponse.ErrorMessage;
        }

        public PlaybookErrorResponseException(HttpStatusCode httpStatusCode, ErrorResponse errorResponse, string reasonPharse) : base(reasonPharse)
        {
            this.HttpStatusCode = httpStatusCode;
            this.ErrorResponse = errorResponse;
            this.ReasonPharse = reasonPharse;
        }
    }
}
