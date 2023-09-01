using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Caja
{
    public class EntidadPago
    {
        public string codigo_venta { get; set; }
        public string codigo_usuario { get; set; }
        public string tipo_pago { get; set; }
        public decimal monto { get; set; }
    }
}
