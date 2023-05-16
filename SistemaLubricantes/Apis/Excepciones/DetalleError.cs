using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Excepciones
{
    internal class ErrorDetails
    {
        public ErrorDetails()
        {

        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
