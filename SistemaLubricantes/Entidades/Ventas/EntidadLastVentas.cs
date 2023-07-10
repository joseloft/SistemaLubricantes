using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Ventas
{
    public class EntidadLastVentas
    {
        public string cod_venta { get; set; }
        public string nro_venta { get; set; }
        public string fecha_venta { get; set; }
        public decimal total { get; set; }
        public string estado { get; set; }
        public string placa { get; set; }
        public decimal tipo_cambio { get; set;}
        public List<EntidadLastVentaDetalle> detalleLastVenta { get; set; }
    }
    public class EntidadLastVentaDetalle
    {
        public string cod_venta { get; set; }
        public string cod_prod { get; set; }
        public string marca { get; set; }
        public string producto { get; set; }
        public int cantidad { get; set; }
        public decimal pre_venta { get; set; }
    }
}
