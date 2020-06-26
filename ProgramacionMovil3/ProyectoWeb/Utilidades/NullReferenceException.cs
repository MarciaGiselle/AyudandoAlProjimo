using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWeb.Utilidades
{
    public class NullReferenceException : Exception
    {
        public NullReferenceException(string message)
            : base(message)
        {
        }
    }
}