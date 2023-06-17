using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using LogicaNegocio.Clientes.Implemetacion;
using Entidades.Clientes;
using System.Collections.Generic;
using Apis.Excepciones;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System;

namespace Apis.Controllers.Clientes
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : Controller
    {
        private readonly IConfiguration _configuration;
        private ClientesLogica _clientesLogica;

        public ClientesController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _clientesLogica = new ClientesLogica(_configuration);
        }

        /// <summary>
        /// Generic method to list clients 
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("ListarClientes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetListarClientes()
        {
            List<EntidadCliente> lstClientes;
            if (!new ClientesLogica(_configuration).ListarClientes(out lstClientes))
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

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(lstClientes));
        }

        /// <summary>
        /// Generic method to create clients 
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpPost("GuardarClientes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult PostGuardarClientes([Required][FromBody] EntidadCliente objCliente)
        {
            string mensaje;
            if (!_clientesLogica.GuardarClientes(objCliente, out mensaje))
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
        /// Generic method to filter clients
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("BuscarClientes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetBuscarClientes(string cod_cliente, string documento, string placa)
        {
            if (!_clientesLogica.BuscarClientes(cod_cliente, documento, placa, out EntidadFiltroCliente objFiltro))
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(objFiltro));

        }
    }
}
