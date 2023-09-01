using AccesoDatos.Clientes.Implementacion;
using AccesoDatos.Login.Implementacion;
using AccesoDatos.Productos.Implementacion;
using Entidades.Clientes;
using Entidades.Productos;
using Entidades.Usuarios;
using LogicaNegocio.Login.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Login.Implementacion
{
    public class LoginLogica :ILoginLogica
    {
        private LoginDatos _loginDatos;
        private readonly IConfiguration _configuration;
        public LoginLogica(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _loginDatos = new LoginDatos(_configuration);
        }
        public LoginLogica(string _DbConexion)
        {
            _loginDatos = new LoginDatos(_DbConexion);
        }
        public bool Autentificacion(string usuario, string contrasenia, out EntidadUsuario objUsuario)
        {
            DataTable objDtt = null;
            var bRsl = _loginDatos.Autentificacion(usuario, contrasenia, out objDtt);
            if (!bRsl)
            {
                objUsuario = null;
                return bRsl;
            }
            objUsuario = new EntidadUsuario();
            EntidadUsuario objU = null;
            foreach (DataRow dataRow in objDtt.Rows)
            {
                objU = new EntidadUsuario()
                {
                    codigo_usuario = dataRow["codigo_usuario"].ToString(),
                    login = dataRow["login"].ToString(),
                    password = dataRow["password"].ToString(),
                    dni = dataRow["dni"].ToString(),
                    correo = dataRow["correo"].ToString(),
                    celular = dataRow["celular"].ToString()
                };
            }

            objUsuario = objU;
            return bRsl;
        }
        public bool CambiarClave(string codigo_usuario, string nueva_clave, out string mensaje)
        {
            return _loginDatos.CambiarClave(codigo_usuario, nueva_clave, out mensaje);
        }
    }
}
