using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Ventas
{
    public class EntidadDetalleVentasPendientes
    {
        public int nro_detalle { get; set; }
        public string producto { get; set; }
        public string marca { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal total { get; set; }
    }
}
