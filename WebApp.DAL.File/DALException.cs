﻿using System;

namespace WebApp.DAL.File
{
    internal class DalException : Exception
    {
        public DalException() : base("DAL exception")
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
