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