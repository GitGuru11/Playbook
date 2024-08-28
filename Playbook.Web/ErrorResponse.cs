namespace Playbook.Web
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Enumeration of error codes that can be returned by the proxy.
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// An unspecified error has occurred. Please contact service support.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Unable to process request, precondition failed.
        /// </summary>
        UnableToProcessRequest = 1,

        /// <summary>
        /// The body of HTTP request hase some invalid or missing parameter.
        /// </summary>
        MissingOrInvalidRequestParameter = 2,
    }

    /// <summary>
    /// Represents an Playbook error message.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Map of error codes to textual messages for client developers.
        /// </summary>
        private static readonly Dictionary<ErrorCode, string> ErrorMessages = new Dictionary<ErrorCode, string>
        {
            { ErrorCode.Unspecified, "An unspecified error has occurred. Please contact service support." },
            { ErrorCode.UnableToProcessRequest ,"Unable to process request." },
            { ErrorCode.MissingOrInvalidRequestParameter, "The body of HTTP request hase invalid or missing parameter {0}"}
        };

        /// <summary>
        /// Prevents a default instance of the <see cref="ErrorResponse"/> class from being created
        /// </summary>
        private ErrorResponse()
        {
        }

        public static Dictionary<ErrorCode, string> ErrorMessages1 => ErrorMessages;

        /// <summary>
        /// Gets the error code to return to the client.
        /// </summary>
        public ErrorCode ErrorCode { get; private set; }

        /// <summary>
        /// Gets the error message to return to the client.
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Factory method to create an instance of <see cref="ErrorResponse"/>.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <param name="args">Optional error message format parameter.</param>
        /// <returns>An initialized <see cref="ErrorResponse"/> object.</returns>
        public static ErrorResponse Create(ErrorCode code, params object[]? args)
        {
            string? message;
            if (!ErrorMessages.TryGetValue(code, out message))
            {
                throw new ArgumentOutOfRangeException("code");
            }

            if (args != null && args.Length > 0)
            {
                message = string.Format(CultureInfo.InvariantCulture, message, args);
            }

            return new ErrorResponse { ErrorCode = code, ErrorMessage = message };
        }


        /// <summary>
        /// Factory method to create an instance of <see cref="ErrorResponse"/>.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <param name="args">Optional error message format parameter.</param>
        /// <returns>An initialized <see cref="ErrorResponse"/> object.</returns>
        public static ErrorResponse CreateFromException(ErrorCode code, Exception ex)
        {

            return new ErrorResponse { ErrorCode = code, ErrorMessage = string.Format("{0} {1}", ex.GetType().ToString(), ex.Message) };
        }

        /// <summary>
        /// Factory method to create an instance of <see cref="ErrorResponse"/>.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <param name="args">Optional error message format parameter.</param>
        /// <returns>An initialized <see cref="ErrorResponse"/> object.</returns>
        public static ErrorResponse Create(ErrorCode code)
        {
            return ErrorResponse.Create(code, null);
        }
    }
}