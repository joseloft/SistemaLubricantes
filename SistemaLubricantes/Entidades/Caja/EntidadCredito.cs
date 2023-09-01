using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Caja
{
    public class EntidadCredito
    {
        public string codigo_venta { get; set; }
        public string codigo_usuario { get; set; }
        public int dias { get; set; }
        public decimal abono { get; set;}
    }
}
