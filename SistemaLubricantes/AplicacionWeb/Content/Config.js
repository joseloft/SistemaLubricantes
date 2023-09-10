function getPath() {
    let uriApiComercio = '';

    //DESARROLLO
    //->LOCAL CON PUERTO 80 POR DEFECTO
    //uriApiComercio = "http://localhost";
    //->LOCAL CON PUERTO DEFINIDO
    uriApiComercio = "https://localhost:44380/";
    //DEV
    //uriApiComercio = "";
    //PRODUCCION
    //uriApiComercio = "";

    return uriApiComercio;
};

const HOST_SENDA = "api/senda/sendaservicecenter";

const ENDPOINT_EMISION = "/EmitirComprobante";
const ENDPOINT_ANULACION = "/AnularComprobante";
const ENDPOINT_CONSULTACION = "/ConsultarComprobante";