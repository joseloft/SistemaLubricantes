using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Home
{
    public class EntidadDashboardNotificaciones
    {
        public List<EntidadProductosStock> lstProductosPocoStock { get; set; }
        public List<EntidadVentasComprobante> lstVentasSinComprobante { get; set; }
        public List<EntidadCreditosVencidos> lstCreditosPorVencer { get; set; }
        public List<EntidadaClientesCorreo> lstClientesSinCorreo { get; set; }
    }
    public class EntidadProductosStock
    {
        public int item { get;set; }
        public string codigo_producto { get; set; }
        public string marca { get; set; }
        public string producto { get; set; }
        public float stock { get; set; }
        public string categoria { get; set; }
        public decimal precio_venta { get; set; }
    }
    public class EntidadVentasComprobante
    {
        public int item { get; set; }
        public string codigo_venta { get; set; }
        public string cliente { get; set; }
        public string fecha_venta { get; set; }
        public decimal total { get; set; }
        public string estado { get; set; }
        public string placa { get; set; }
    }
    public class EntidadCreditosVencidos
    {
        public int item { get; set; }
        public string codigo_venta { get; set; }
        public string cliente { get; set; }
        public decimal monto_venta { get; set; }
        public decimal monto_credito { get; set; }
        public string fecha_vencimiento { get; set; }
        public int dias_credito { get; set; }
    }
    public class EntidadaClientesCorreo
    {
        public int item { get; set; }
        public string codigo_cliente { get; set; }
        public string cliente { get; set; }
        public string fecha_registro { get; set; }
        public string tipo_cliente { get;set; }
        public string documento { get;set; }
    }
}
