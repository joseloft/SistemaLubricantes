using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Excepciones
{
    public class SuccessAnswer
    {
        public SuccessAnswerDetail success { get; set; }
    }

    public class SuccessAnswerDetail
    {
        public string titulo { get; set; }
        public string codigo { get; set; }
        public string mensaje { get; set; }
    }
    public class ErrorAnswer
    {
        /// <summary>
        /// Objeto contenedor de error.
        /// </summary>
        public ErrorAnswerDetail error { get; set; }
    }
    public class ErrorAnswerDetail
    {
        /// <summary>
        /// Identificador de transacción.
        /// </summary>
        public string idtransaccion { get; set; }
        /// <summary>
        /// Título de error.
        /// </summary>
        public string titulo { get; set; }
        /// <summary>
        /// Codigo de error.
        /// </summary>
        public string codigo { get; set; }
        /// <summary>
        /// Mensaje de error.
        /// </summary>
        public string mensaje { get; set; }
    }
}
