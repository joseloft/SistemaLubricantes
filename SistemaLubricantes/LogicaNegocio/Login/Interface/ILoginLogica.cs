using Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Login.Interface
{
    interface ILoginLogica
    {
        bool Autentificacion(string usuario, string contrasenia, out EntidadUsuario objUsuario);
        bool CambiarClave(string codigo_usuario, string nueva_clave, out string mensaje);
    }
}
