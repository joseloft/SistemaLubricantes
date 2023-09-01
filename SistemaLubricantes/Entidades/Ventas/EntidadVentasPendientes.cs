using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Ventas
{
    public class EntidadVentasPendientes
    {
        public string codigo_venta { get; set; }
        public string tipo_documento { get; set; }
        public string documento { get; set; }
        public string cliente { get; set; }
        public string fecha_venta { get; set; }
        public string forma_pago { get; set; }
        public string moneda { get; set; }
        public decimal total { get; set; }
        public string estado { get; set; }
        public string fecha_vencimiento { get; set; }
        public decimal saldo { get; set; }
        public int dias_credito { get; set; }
    }
}
