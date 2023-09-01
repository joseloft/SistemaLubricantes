using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Usuarios
{
    public class EntidadUsuario
    {
        public void ReplaceNull()
        {
            dni = dni ?? "";
            correo = correo ?? "";
            celular = celular ?? "";
            estado = estado ?? false;
        }
        public string codigo_usuario { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string dni { get; set; }
        public string correo { get; set; }
        public string celular { get; set; }
        public bool? estado { get; set; }
    }
}
