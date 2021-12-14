using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Assertions
{

    [Serializable]
    public class AssertionException : Exception
    {
        public AssertionException() { }
        public AssertionException(string message) : base(message) { }
        public AssertionException(string message, Exception inner) : base(message, inner) { }
        protected AssertionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
