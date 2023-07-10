using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Caja
{
    public class EntidadComprobante
    {
        public string codigo_venta { get; set; }
        public string tipo_comprobante { get; set; }
        public string tipo_documento { get; set; }
        public string codigo_usuario { get; set; }
        public string tipo_pago { get; set; }
    }
}
