using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Excepciones
{
    public static class ConstantsError
    {
        public const string ERROR_SIN_CONTENIDO_CODIGO = "00001";
        public const string ERROR_SIN_CONTENIDO_MENSAJE = "END POINT WITHOUT TOKEN";

        public const string ERROR_MALA_PETICION_CODIGO = "00002";
        public const string ERROR_MALA_PETICION_MENSAJE = "REQUEST COULD NOT BE UNDERSTOOD";

        public const string ERROR_GENERICO_CODIGO = "00003";
        public const string ERROR_GENERICO_MENSAJE = "GENERIC ERROR HAS OCCURRED";

        public const string ERROR_EXCEPCION_CODIGO = "00004";
        public const string ERROR_EXCEPCION_MENSAJE = "ERRORS THAT OCCUR DURING EXECUTION";

        public const string ERROR_SIN_CONEXION_INTERNET_CODIGO = "00005";
        public const string ERROR_SIN_CONEXION_INTERNET_MENSAJE = "WITHOUT INTERNET CONNECTION";

        public const string ERROR_TIEMPO_FUERA_CODIGO = "00006";
        public const string ERROR_TIEMPO_FUERA_MENSAJE = "TIME OUT";

        public const string ERROR_NO_HUBO_RESPUESTA_CODIGO = "00007";
        public const string ERROR_NO_HUBO_RESPUESTA_MENSAJE = "NO RESPONSE RETURNED";

        public const string ERROR_DESCONOCIDO_CODIGO = "00008";
        public const string ERROR_DESCONOCIDO_MENSAJE = "UNKNOWN ERROR";

        public const string ERROR_EN_SERVIDOR_CODIGO = "00009";
        public const string ERROR_EN_SERVIDOR_MENSAJE = "INTERNAL SERVER ERROR";

        public const string ERROR_ACCESO_DENEGADO_CODIGO = "00010";
        public const string ERROR_ACCESO_DENEGADO_MENSAJE = "UNAUTHORIZED";

        public const string ERROR_REQUEST_SUCCESSED_CODIGO = "00011";
        public const string ERROR_REQUEST_SUCCESSED_MENSAJE = "OK";
    }

    public struct ContextItem
    {
        public const string ItemValue = "_parameters_";
    }
}
