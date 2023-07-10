using Apis.Excepciones;
using Entidades.Productos;
using LogicaNegocio.Productos.Implementacion;
using LogicaNegocio.Ventas.Implementacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System;
using Entidades.Ventas;
using System.Collections.Generic;
using LogicaNegocio.Caja.Implementacion;

namespace Apis.Controllers.Ventas
{
    [Route("api/ventas")]
    [ApiController]
    public class VentasController : Controller
    {
        private readonly IConfiguration _configuration;
        private VentasLogica _ventasLogica;

        public VentasController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _ventasLogica = new VentasLogica(_configuration);
        }

        /// <summary>
        /// Method to get last sales list
        /// </summary>
        /// <param name="placa"></param>
        /// <param name="cod_cliente"></param>
        /// <response code="200">Ok</response>
        /// <response code="204">No Content - Respuesta en blanco</response> 
        /// <response code="400">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response>
        [HttpGet("ListaUltimasVentas")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetListaUltimasVentas(string placa, string cod_cliente)
        {
            List<EntidadLastVentas> lstLastVentas;
            if (!_ventasLogica.ListaUltimasVentas(out lstLastVentas, placa, cod_cliente))
            {
                var objErrorAnswer = new ErrorAnswer()
                {
                    error = new ErrorAnswerDetail()
                    {
                        idtransaccion = "",
                        titulo = "ERROR",
                        codigo = ConstantsError.ERROR_EN_SERVIDOR_CODIGO,
                        mensaje = ConstantsError.ERROR_EN_SERVIDOR_MENSAJE
                    }
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, objErrorAnswer);
            }

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(lstLastVentas));

        }

        /// <summary>
        /// Generic method to create sales 
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpPost("GuardarVenta")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult PostGuardarVentas([Required][FromBody] EntidadVenta objVenta)
        {
            string mensaje;
            if (!_ventasLogica.GuardarVenta(objVenta, out mensaje))
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
        /// Generic method to list sales pending 
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("ListarVentasPendientes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetListarVentasPendientes()
        {
            List<EntidadVentasPendientes> lstVentasP;
            if (!new VentasLogica(_configuration).ListarVentasP(out lstVentasP))
            {
                var objErrorAnswer = new ErrorAnswer()
                {
                    error = new ErrorAnswerDetail()
                    {
                        idtransaccion = "",
                        titulo = "ERROR",
                        codigo = ConstantsError.ERROR_EN_SERVIDOR_CODIGO,
                        mensaje = ConstantsError.ERROR_EN_SERVIDOR_MENSAJE
                    }
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, objErrorAnswer);
            }

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(lstVentasP));
        }

        /// <summary>
        /// Generic method to list sales pending 
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("ListarDetalleVentaPendientes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetListarDetalleVentaPendientes(string codigo_venta)
        {
            List<EntidadDetalleVentasPendientes> lstDetalleVentasP;
            if (!new VentasLogica(_configuration).ListarDetalleVentasP(codigo_venta, out lstDetalleVentasP))
            {
                var objErrorAnswer = new ErrorAnswer()
                {
                    error = new ErrorAnswerDetail()
                    {
                        idtransaccion = "",
                        titulo = "ERROR",
                        codigo = ConstantsError.ERROR_EN_SERVIDOR_CODIGO,
                        mensaje = ConstantsError.ERROR_EN_SERVIDOR_MENSAJE
                    }
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, objErrorAnswer);
            }

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(lstDetalleVentasP));
        }
    }
}
