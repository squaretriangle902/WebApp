using System;

namespace WebApp.BLL.Core
{
    internal class BLLException : Exception
    {
        public BLLException() : base("BLL exception")
        {
        }

        public BLLException(string message) : base(message)
        {
        }

        public BLLException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
