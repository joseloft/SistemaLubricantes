using Apis.Excepciones;
using Entidades.Clientes;
using Entidades.Usuarios;
using LogicaNegocio.Caja.Implementacion;
using LogicaNegocio.Clientes.Implemetacion;
using LogicaNegocio.Login.Implementacion;
using LogicaNegocio.Productos.Implementacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Numerics;

namespace Apis.Controllers.Login
{
    [Route("api/security")]
    [ApiController]
    public class LoginController : Controller
    {
        #region "variables"
        private readonly IConfiguration _configuration;
        private LoginLogica _loginLogica;
        #endregion

        #region "constructor"
        public LoginController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _loginLogica = new LoginLogica(_configuration);
        }
        #endregion

        #region "methods"
        #region "apis"
        /// <summary>
        /// Generic method to verify user authentication
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="contrasenia"></param>
        /// <response code="200">Ok</response>
        /// <response code="401">Requires authentication - El acceso requiere autenticación</response> 
        [HttpPost("AuthenticateUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401, Type = typeof(ErrorAnswer))]
        [AllowAnonymous]
        public ActionResult Authenticate([Required] string usuario, [Required] string contrasenia)
        {
            if (!_loginLogica.Autentificacion(usuario, contrasenia, out EntidadUsuario objUsuario))
            {
                return new JsonResult(new ErrorDetails()
                {
                    StatusCode = Convert.ToInt32(ConstantsError.ERROR_ACCESO_DENEGADO_CODIGO),
                    Message = ConstantsError.ERROR_ACCESO_DENEGADO_MENSAJE
                });
            }

            return StatusCode((int)HttpStatusCode.OK, new JsonResult(objUsuario));
        }

        /// <summary>
        /// Generic method to update passwoerd 
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="204">Not Conten - Respuesta en blanco</response>
        /// <response code="400">Bad Request - Solicitud Errada o contenido incorrecto</response> 
        /// <response code="404">Not Found - No se encontro información</response>   
        /// <response code="500">Server Error - Errores no controlados</response> 
        [HttpPut("CambiarClave")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult PutCambiarClave([Required] string codigo_usuario, [Required] string nueva_clave)
        {
            string mensaje;
            if (!_loginLogica.CambiarClave(codigo_usuario, nueva_clave, out mensaje))
            {
                return new JsonResult(new ErrorDetails()
                {
                    StatusCode = Convert.ToInt32(ConstantsError.ERROR_EN_SERVIDOR_CODIGO),
                    Message = ConstantsError.ERROR_EN_SERVIDOR_MENSAJE
                });
            }
            return StatusCode((int)HttpStatusCode.OK, new JsonResult(mensaje));

        }
        [HttpPost("Login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(404, Type = typeof(ErrorAnswer))]
        [ProducesResponseType(500, Type = typeof(ErrorAnswer))]
        public ActionResult Login([Required] string Login, [Required] string Password)
        {
            ViewBag.User = Login;
            var ObjUsu = Authenticate(Login, Password);
            if (ObjUsu == null)
            {
                ViewBag.Error = "El Usuario no existe";
                return View("Index");
            }
            else
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");
            }
            
        }
        #endregion
        #endregion

        #region "private methods"


        #endregion
    }
}
