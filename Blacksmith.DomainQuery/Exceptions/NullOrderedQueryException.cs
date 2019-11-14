using System;
using System.Runtime.Serialization;

namespace Blacksmith.DomainQuery.Exceptions
{
    [Serializable]
    public class NullOrderedQueryException : InvalidOperationException
    {
        public NullOrderedQueryException(object key)
            : base($"Null query returned from concrete class after set {key} key order.")
        {
        }

        protected NullOrderedQueryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}