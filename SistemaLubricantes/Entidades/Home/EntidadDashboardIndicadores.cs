using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Home
{
    public class EntidadDashboardIndicadores
    {
        public EntidadTotalVentas objTotalVentas { get; set; }
        public EntidadCantidadVentas objCantidadVentas { get; set; }
        public EntidadClientesNuevos objClientesNuevos { get; set; }
        public EntidadFacturacion objFacturacion { get; set; }
    }
    public class EntidadTotalVentas
    {
        public string reporte { get; set; }
        public decimal montoPago { get; set; }
    }
    public class EntidadCantidadVentas
    {
        public string reporte { get; set; }
        public int cantidad { get; set; }
    }
    public class EntidadClientesNuevos
    {
        public string reporte { get; set; }
        public int cantidad { get; set; }
    }
    public class EntidadFacturacion
    {
        public string reporte { get; set; }
        public decimal montoPago { get; set; }
    }
}
