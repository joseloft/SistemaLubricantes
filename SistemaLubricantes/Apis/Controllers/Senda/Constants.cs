using System;

namespace Apis.Controllers.Senda
{
    public enum HttpMethod
    {
        GET,
        POST,
        PUT
    }
    public struct SettingEndPoint
    {
        public const string Senda_Section_Uri = "UriSenda";
        public const string Senda_Key_Token_Prueba = "token_prueba";
        public const string Senda_Key_Token_Produccion = "token_produccion";
        public const string Senda_Key_Emision = "Url01";
        public const string Senda_Key_Anulacion = "Url02";
        public const string Senda_Key_Consultacion = "Url03";
    }
    public static class ConstantsError
    {
        public const Int32 ERROR_SIN_CONTENIDO_CODIGO = 1;
        public const string ERROR_SIN_CONTENIDO_MENSAJE = "END POINT WITHOUT TOKEN";

        public const Int32 ERROR_MALA_PETICION_CODIGO = 2;
        public const string ERROR_MALA_PETICION_MENSAJE = "REQUEST COULD NOT BE UNDERSTOOD";

        public const Int32 ERROR_GENERICO_CODIGO = 3;
        public const string ERROR_GENERICO_MENSAJE = "GENERIC ERROR HAS OCCURRED";

        public const Int32 ERROR_EXCEPCION_CODIGO = 4;
        public const string ERROR_EXCEPCION_MENSAJE = "ERRORS THAT OCCUR DURING EXECUTION";

        public const Int32 ERROR_SIN_CONEXION_INTERNET_CODIGO = 5;
        public const string ERROR_SIN_CONEXION_INTERNET_MENSAJE = "WITHOUT INTERNET CONNECTION";

        public const Int32 ERROR_TIEMPO_FUERA_CODIGO = 6;
        public const string ERROR_TIEMPO_FUERA_MENSAJE = "TIME OUT";

        public const Int32 ERROR_NO_HUBO_RESPUESTA_CODIGO = 7;
        public const string ERROR_NO_HUBO_RESPUESTA_MENSAJE = "NO RESPONSE RETURNED";

        public const Int32 ERROR_DESCONOCIDO_CODIGO = 8;
        public const string ERROR_DESCONOCIDO_MENSAJE = "UNKNOWN ERROR";

        public const Int32 ERROR_CUENTA_INVALIDA_CODIGO = 9;
        public const string ERROR_CUENTA_INVALIDA_MENSAJE = "INVALID ACCOUNT";

        public const Int32 ERROR_CONVERSION_TIPO_DATO_CODIGO = 10;
        public const string ERROR_CONVERSION_TIPO_DATO_MENSAJE = "DATA TYPE CONVERSION ERROR";

    }
}
