using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using LogicaNegocio.Productos.Implementacion;
using Entidades.Productos;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Apis.Controllers.Productos.Modelos.Respuesta;
using Apis.Excepciones;
using System.Net;
using Entidades.Clientes;
using LogicaNegocio.Clientes.Implemetacion;
using System;

namespace Apis.Controllers.Productos
{
    [Route("api/productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private ProductosLogica _productosLogica;

        public ProductosController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _productosLogica = new ProductosLogica(_configuration);
        }

        /// <summary>
        /// Generic method to list products 
        /// </summary>
        /// <param name="parametro"></param>
        /// <param name="producto"></param>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("ListarProductos")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult GetListarProductos()
        {
            List<EntidadProducto> lstProductos;
            if (!new ProductosLogica(_configuration).ListarProductos(out lstProductos))
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
            
            return StatusCode((int)HttpStatusCode.OK, new JsonResult(lstProductos));
        }

        /// <summary>
        /// Generic method to create products 
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpPost("GuardarProductos")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult PostGuardarProductos([Required][FromBody] EntidadProducto objProducto)
        {
            string mensaje;
            if (!_productosLogica.GuardarProductos(objProducto, out mensaje))
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
        /// Generic method to get code product
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("ObtenerCodigoProducto")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetObtenerCodigoProducto()
        {
            string codigoProducto;
            if (!_productosLogica.ObtenerCodigoProducto(out codigoProducto))
            {
                return new JsonResult(new ErrorDetails()
                {
                    StatusCode = Convert.ToInt32(ConstantsError.ERROR_EN_SERVIDOR_CODIGO),
                    Message = ConstantsError.ERROR_EN_SERVIDOR_MENSAJE
                });
            }
            return StatusCode((int)HttpStatusCode.OK, new JsonResult(codigoProducto));

        }

        /// <summary>
        /// Generic method to list currency
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("ListarMoneda")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetListarMoneda()
        {
            List<EntidadMoneda> lstMoneda;
            if (!_productosLogica.ListarMoneda(out lstMoneda))
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(lstMoneda));

        }

        /// <summary>
        /// Generic method to list categories
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("ListarCategoria")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetListarCategoria()
        {
            List<EntidadCategoria> lstCategoria;
            if (!_productosLogica.ListarCategoria(out lstCategoria))
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(lstCategoria));

        }

        /// <summary>
        /// Generic method to list brands
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("ListarMarca")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetListarMarca(string codigoCategoria)
        {
            List<EntidadMarca> lstMarca;
            if (!_productosLogica.ListarMarca(codigoCategoria, out lstMarca))
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(lstMarca));

        }

        /// <summary>
        /// Generic method to list tipo
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("ListarTipo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetListarTipo(string codigoCategoria)
        {
            List<EntidadTipo> lstTipo;
            if (!_productosLogica.ListarTipo(codigoCategoria, out lstTipo))
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(lstTipo));

        }

        /// <summary>
        /// Generic method to create brands 
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpPut("GuardarMarca")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult PutGuardarMarca([Required] string categoriaID, [Required] string nombre)
        {
            string mensaje;
            if (!_productosLogica.GuardarMarca(categoriaID, nombre, out mensaje))
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
        /// Generic method to create type 
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpPut("GuardarTipo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult PutGuardarTipo([Required] string categoriaID, [Required] string descripcion)
        {
            string mensaje;
            if (!_productosLogica.GuardarTipo(categoriaID, descripcion, out mensaje))
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
        /// Generic method to list balde
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("ListarBalde")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetListarBalde(int parametro)
        {
            List<EntidadBalde> lstBalde;
            if (!_productosLogica.ListarBalde(parametro, out lstBalde))
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(lstBalde));

        }
        /// <summary>
        /// Generic method to list balde
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("ListarEnvase")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetListarEnvase(int parametro)
        {
            List<EntidadEnvase> lstEnvase;
            if (!_productosLogica.ListarEnvase(parametro, out lstEnvase))
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(lstEnvase));

        }
        /// <summary>
        /// Generic method to list balde
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("ListarPaquete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetListarPaquete(int parametro)
        {
            List<EntidadPaquete> lstPaquete;
            if (!_productosLogica.ListarPaquete(parametro, out lstPaquete))
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(lstPaquete));

        }
        /// <summary>
        /// Generic method to list balde
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpGet("ListarCilindro")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult GetListarCilindro(int parametro)
        {
            List<EntidadCilindro> lstCilindro;
            if (!_productosLogica.ListarCilindro(parametro, out lstCilindro))
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(lstCilindro));

        }
    }
}
