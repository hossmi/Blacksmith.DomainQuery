using System;
using System.Runtime.Serialization;

namespace Blacksmith.PagedEnumerable.Exceptions
{
    public class PagedEnumerableException : Exception
    {
        public PagedEnumerableException(string message) : base(message)
        {
        }

        public PagedEnumerableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PagedEnumerableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}