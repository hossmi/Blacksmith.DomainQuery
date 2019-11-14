using System;
using System.Runtime.Serialization;

namespace Blacksmith.DomainQuery.Exceptions
{
    [Serializable]
    public class AlreadyAddedOrderKeyException : InvalidOperationException
    {
        public AlreadyAddedOrderKeyException(object key) : base($"The order key {key} has already been added.")
        {
        }

        protected AlreadyAddedOrderKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}