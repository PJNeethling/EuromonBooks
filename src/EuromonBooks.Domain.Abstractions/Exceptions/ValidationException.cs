using EuromonBooks.Domain.Abstractions.Models.ExceptionErrors;
using System.Runtime.Serialization;

namespace EuromonBooks.Domain.Abstractions.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ValidationException(string message, List<ErrorDetails> errors) : base(message)
        {
            Errors = errors;
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public List<ErrorDetails> Errors { get; set; }
        public string ErrorType { get; set; } = null;
    }
}