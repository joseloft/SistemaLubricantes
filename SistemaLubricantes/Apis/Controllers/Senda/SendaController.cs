using Apis.Excepciones;
using Entidades.Home;
using Entidades.Senda;
using LogicaNegocio.Home.Implementacion;
using LogicaNegocio.Senda.Implementacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Apis.Controllers.Senda
{
    public class SendaController : Controller
    {
        #region "variables"
        private readonly IConfiguration _configuration;
        private SendaLogica _sendaLogica;

        public SendaController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _sendaLogica = new SendaLogica(_configuration);
        }
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
        public ActionResult PostEmitirComprobante(string nro_venta)
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
