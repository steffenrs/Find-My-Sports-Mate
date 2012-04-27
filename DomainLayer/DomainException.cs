using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    [Serializable()]
    public class DomainException : System.Exception
    {
        public DomainException() : base() { }
        public DomainException(string message) : base(message) { }
        public DomainException(string message, System.Exception inner) : base(message, inner) { }
        protected DomainException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
