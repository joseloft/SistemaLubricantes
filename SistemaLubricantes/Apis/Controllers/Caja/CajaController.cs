using Apis.Excepciones;
using Entidades.Caja;
using Entidades.Clientes;
using Entidades.Productos;
using Entidades.Ventas;
using LogicaNegocio.Caja.Implementacion;
using LogicaNegocio.Clientes.Implemetacion;
using LogicaNegocio.Productos.Implementacion;
using LogicaNegocio.Ventas.Implementacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Apis.Controllers.Caja
{
    [Route("api/caja")]
    [ApiController]
    public class CajaController : Controller
    {
        private readonly IConfiguration _configuration;
        private CajaLogica _cajaLogica;

        public CajaController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _cajaLogica = new CajaLogica(_configuration);
        }               

        /// <summary>
        /// Generic method to list comprobante
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("ListarTipoComprobante")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetListarTipoComprobante()
        {
            List<EntidadTipoComprobante> lstComprobante;
            if (!_cajaLogica.ListarTipoComprobante(out lstComprobante))
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(lstComprobante));

        }

        /// <summary>
        /// Generic method to list type pay
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("ListarTipoPago")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetListarTipoPago()
        {
            List<EntidadTipoPago> lstTipoPago;
            if (!_cajaLogica.ListarTipoPago(out lstTipoPago))
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(lstTipoPago));

        }

        /// <summary>
        /// Generic method to create tickets 
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpPost("GuardarComprobante")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult PostGuardarComprobante([Required][FromBody] EntidadComprobante objComprobante)
        {
            string mensaje;
            if (!_cajaLogica.GuardarComprobante(objComprobante, out mensaje))
            {
                return new JsonResult(new ErrorDetails()
                {
                    StatusCode = Convert.ToInt32(ConstantsError.ERROR_EN_SERVIDOR_CODIGO),
                    Message = ConstantsError.ERROR_EN_SERVIDOR_MENSAJE
                });
            }
            return StatusCode((int)HttpStatusCode.OK, new JsonResult(mensaje));

        }
        /// <summary>
        /// Generic method to create credits 
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpPut("GuardarCredito")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult PutGuardarCredito([Required][FromBody] EntidadCredito objCredito)
        {
            string mensaje;
            if (!_cajaLogica.GuardarCredito(objCredito, out mensaje))
            {
                return new JsonResult(new ErrorDetails()
                {
                    StatusCode = Convert.ToInt32(ConstantsError.ERROR_EN_SERVIDOR_CODIGO),
                    Message = ConstantsError.ERROR_EN_SERVIDOR_MENSAJE
                });
            }
            return StatusCode((int)HttpStatusCode.OK, new JsonResult(mensaje));

        }
        /// <summary>
        /// Generic method to create pays 
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpPut("GuardarPago")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult PutGuardarPago([Required][FromBody] EntidadPago objPago)
        {
            string mensaje;
            if (!_cajaLogica.GuardarPago(objPago, out mensaje))
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
