using Apis.Excepciones;
using Entidades.Home;
using Entidades.Senda;
using LogicaNegocio.Home.Implementacion;
using LogicaNegocio.Senda.Implementacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Utilitarios;
using System.Net.Http;
using RestSharp;
using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;
using Azure;

namespace Apis.Controllers.Senda
{
    [Route("api/senda/sendaservicecenter")]
    [ApiController]
    public class SendaController : Controller
    {
        #region "variables"
        private readonly IConfiguration _configuration;
        private SendaLogica _sendaLogica;

        #region "constructor"
        public SendaController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _sendaLogica = new SendaLogica(_configuration);
        }
        #endregion
        #endregion

        #region "methods"
        #region "apis"

        /// <summary>
        /// Generic method to senda send
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpPost("EmitirComprobante")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult PostEmitirComprobante([Required] string nro_venta)
        {
            #region "get senda api url"
            var sendaAppsetting = ConfigurationReader.GetKeyValueAppsetting(FileJson.Samanga, SettingConexion.Senda_Section_Appsetting, SettingConexion.Senda_Key_Appsetting);
            if (string.IsNullOrEmpty(sendaAppsetting))
            {
                throw new UCustomException(ConstantsError.ERROR_DESCONOCIDO_MENSAJE, ConstantsError.ERROR_DESCONOCIDO_CODIGO);
            }
            var sUrlApiBase = ConfigurationReader.GetKeyValueAppsetting(sendaAppsetting, SettingEndPoint.Senda_Section_Uri, SettingEndPoint.Senda_Key_Emision);
            if (string.IsNullOrEmpty(sUrlApiBase))
            {
                throw new UCustomException(ConstantsError.ERROR_DESCONOCIDO_MENSAJE, ConstantsError.ERROR_DESCONOCIDO_CODIGO);
            }
            var stoken = ConfigurationReader.GetKeyValueAppsetting(sendaAppsetting, SettingEndPoint.Senda_Section_Uri, SettingEndPoint.Senda_Key_Token_Prueba);
            #endregion

            #region "set parameters"
            EntidadSenda objSenda;
            if (!_sendaLogica.EmitirComprobante(out objSenda, nro_venta))
            {
                return StatusCode((int)HttpStatusCode.NoContent, "");
            }
            var sUrlApiParams = JsonConvert.SerializeObject(objSenda);
            #endregion

            #region "send request"
            var sUrlApiRequest = sUrlApiBase;
            var client = new RestClient(sUrlApiRequest);
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);            
            request.AddHeader("Content-Type", "application/json");            
            request.AddParameter("application/json", sUrlApiParams, ParameterType.RequestBody);
            request.AddHeader("Authorization", "Bearer " + stoken);
            IRestResponse response = client.Execute(request);
            string sResponse = response.Content;

            #region "get response"
            JObject jsonParse = JObject.Parse(sResponse);
            if (!jsonParse.HasValues)
            {
                var jsonerrorArray = jsonParse;
                throw new UCustomException(ConstantsError.ERROR_SIN_CONTENIDO_MENSAJE + " => " + jsonerrorArray.Value<string>("message"), Convert.ToInt32(ConstantsError.ERROR_SIN_CONTENIDO_CODIGO));
            }
            var jsonerror = jsonParse["error"];
            if (jsonerror != null)
            {
                var jsonerrorArray = (JObject)jsonerror;
                throw new UCustomException(ConstantsError.ERROR_EXCEPCION_MENSAJE + " => " + jsonerrorArray.Value<string>("message"), Convert.ToInt32(ConstantsError.ERROR_EXCEPCION_CODIGO));
            }
            #endregion

            return StatusCode((int)HttpStatusCode.OK, sResponse);
            #endregion
        }

        #endregion

        #region "privados"

        /// <summary>
        /// Generic method to senda send
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpPut("ObtenerJsonComprobante")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult PutObtenerJson([Required] string nro_venta)
        {
            EntidadSenda objSenda;
            if (!_sendaLogica.EmitirComprobante(out objSenda, nro_venta))
            {
                return StatusCode((int)HttpStatusCode.NoContent, "");
            }
            return StatusCode((int)HttpStatusCode.OK, objSenda);
        }

        #endregion

        #endregion
    }
}
