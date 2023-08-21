using System;

namespace WebApp.DAL.SQL
{
    internal class DalException : Exception
    {
        public DalException() : base("BLL exception")
        {
        }

        public DalException(string message) : base(message)
        {
        }

        public DalException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
