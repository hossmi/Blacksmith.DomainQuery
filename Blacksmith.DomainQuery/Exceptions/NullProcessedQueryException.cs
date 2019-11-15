using System;
using System.Runtime.Serialization;

namespace Blacksmith.DomainQuery.Exceptions
{
    [Serializable]
    public class NullProcessedQueryException : InvalidOperationException
    {
        public NullProcessedQueryException()
            : base($"Null query returned from concrete class after processQuery method call.")
        {
        }

        protected NullProcessedQueryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}