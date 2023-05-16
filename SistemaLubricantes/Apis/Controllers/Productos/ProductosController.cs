using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using LogicaNegocio.Productos.Implementacion;
using Entidades.Productos;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Apis.Controllers.Productos.Modelos.Respuesta;
using Apis.Excepciones;
using System.Net;

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
        public ActionResult GetListarProductos([Required(ErrorMessage = "This field is required")] int parametro, string producto)
        {
            producto = producto ?? string.Empty;

            if (parametro <= 0)
            {
                var objErrorAnswer = new ErrorAnswer()
                {
                    error = new ErrorAnswerDetail()
                    {
                        idtransaccion = "",
                        titulo = "ERROR",
                        codigo = ConstantsError.ERROR_MALA_PETICION_CODIGO,
                        mensaje = ConstantsError.ERROR_MALA_PETICION_MENSAJE
                    }
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, objErrorAnswer);
            }

            List<EntidadProducto> lstProductos;
            if (!new ProductosLogica(_configuration).ListarProductos(out lstProductos, parametro, producto))
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

            var response = new ProductosRespuesta
            {
                Productos = new List<ProductosListaRespuesta>()
            };
            lstProductos.ForEach(item =>
            {
                response.Productos.Add(new ProductosListaRespuesta()
                {
                    cod_producto = item.cod_producto,
                    nombre = item.nombre,
                    marca = item.marca,
                    stock = item.stock,
                    precio_venta = item.precio_venta,
                    moneda = item.moneda
                });
            });
            return StatusCode((int)HttpStatusCode.OK, response);
        }
    }
}
