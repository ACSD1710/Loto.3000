


using System.Runtime.Serialization;

namespace Loto3000Application.Exeption
{
    public class ValidationException : Exception
    {
        public ValidationException()
        {

        }
        public ValidationException(string? message): base(message)
        {

        }
        public ValidationException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
        public ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
