using System.Net;
using System.Runtime.Serialization;

namespace EuromonBooks.Abstractions.Exceptions
{
    [Serializable]
    public class ApiException : Exception
    {
        public ApiException(int httpStatusCode)
        {
            this.StatusCode = httpStatusCode;
        }

        public ApiException(HttpStatusCode httpStatusCode) : base(httpStatusCode.ToString())
        {
            this.StatusCode = (int)httpStatusCode;
        }

        public ApiException(int httpStatusCode, string message) : base(message)
        {
            this.StatusCode = httpStatusCode;
        }

        public ApiException(HttpStatusCode httpStatusCode, string message) : base(message)
        {
            this.StatusCode = (int)httpStatusCode;
        }

        public ApiException(int httpStatusCode, string message, Exception inner) : base(message, inner)
        {
            this.StatusCode = httpStatusCode;
        }

        public ApiException(HttpStatusCode httpStatusCode, string message, Exception inner) : base(message, inner)
        {
            this.StatusCode = (int)httpStatusCode;
        }

        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int StatusCode { get; }
    }
}