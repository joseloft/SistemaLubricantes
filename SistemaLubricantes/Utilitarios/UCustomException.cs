using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{
    public class UCustomException : Exception
    {
        public int ErrorCode = 0;
        public string ValorAdicional;

        public UCustomException() : base()
        {
        }
        public UCustomException(string message, int code) : base(message)
        {
            this.ErrorCode = code;
        }

        public UCustomException(string message, int code, Exception inner) : base(message, inner)
        {
            this.ErrorCode = code;
        }
    }
}
