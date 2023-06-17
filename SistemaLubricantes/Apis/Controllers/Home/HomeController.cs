using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using LogicaNegocio.Home.Implementacion;
using Apis.Excepciones;
using System.Data;
using System.Net;
using System.ComponentModel.DataAnnotations;
using Entidades.Home;
using System;

namespace Apis.Controllers.Home
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private TipoCambioLogica _tipoCambioLogica;

        public HomeController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _tipoCambioLogica = new TipoCambioLogica(_configuration);
        }

        /// <summary>
        /// Method to get TC current
        /// </summary>        
        /// <response code="200">Ok</response>
        /// <response code="204">No Content - Respuesta en blanco</response> 
        /// <response code="400">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response>
        [HttpGet("ListarTipoCambio")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetListarTipoCambio()
        {
            if (!new TipoCambioLogica(_configuration).ListarTipoCambio(out EntidadTipoCambio objTipoCambio))
            {
                var objErrorAnswer = new ErrorAnswer()
                {
                    error = new ErrorAnswerDetail()
                    {
                        idtransaccion = "",
                        titulo = "ERROR",
                        codigo = ConstantsError.ERROR_GENERICO_CODIGO,
                        mensaje = ConstantsError.ERROR_GENERICO_MENSAJE
                    }
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, objErrorAnswer);
            }
            return StatusCode((int)HttpStatusCode.OK, new JsonResult(objTipoCambio));

        }

        /// <summary>
        /// Generic method to create TC 
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpPost("GuardarTipoCambio")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult PostGuardarTipoCambio([FromBody] EntidadTipoCambio objTipoCambio)
        {
            string mensaje;
            if (!_tipoCambioLogica.GuardarTipoCambio(objTipoCambio, out mensaje))
            {
                return new JsonResult(new ErrorDetails()
                {
                    StatusCode = Convert.ToInt32(ConstantsError.ERROR_EN_SERVIDOR_CODIGO),
                    Message = ConstantsError.ERROR_EN_SERVIDOR_MENSAJE
                });
            }
            return StatusCode((int)HttpStatusCode.OK, new JsonResult(mensaje));

        }
    }
}
