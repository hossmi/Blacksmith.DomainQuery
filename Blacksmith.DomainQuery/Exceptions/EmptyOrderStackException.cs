using System;
using System.Runtime.Serialization;

namespace Blacksmith.DomainQuery.Exceptions
{
    [Serializable]
    public class EmptyOrderStackException : InvalidOperationException
    {
        public EmptyOrderStackException(): base("There are no order conditions for to remove.")
        {
        }

        protected EmptyOrderStackException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}