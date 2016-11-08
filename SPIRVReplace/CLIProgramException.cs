using System;
using System.Runtime.Serialization;

namespace SPIRVReplace
{
    [Serializable]
    internal class CLIProgramException : Exception
    {
        public CLIProgramException()
        {
        }

        public CLIProgramException(string message) : base(message)
        {
        }

        public CLIProgramException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CLIProgramException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}