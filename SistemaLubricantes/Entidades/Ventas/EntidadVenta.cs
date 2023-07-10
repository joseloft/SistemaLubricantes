using System.Collections.Generic;

namespace Entidades.Ventas
{
    public class EntidadVenta
    {
        public void ReplaceNull()
        {
            codigo_venta = codigo_venta ?? "";
            codigo_usuario = codigo_usuario ?? "";
            placa = placa ?? "";
            codigo_tc = codigo_tc ?? "";
        }
        public string codigo_venta { get; set; }
        public decimal sub_total { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public string codigo_cliente { get;set; }
        public string codigo_usuario { get; set; }
        public string placa { get; set; }
        public string codigo_tc { get; set; }
        public List<EntidadVentaDetalle> detalleVenta { get; set; }
    }
    public class EntidadVentaDetalle
    {
        public void ReplaceNull()
        {
            cod_venta = string.Empty;
        }
        public string cod_venta { get; set; }
        public string cod_prod { get; set; }
        public int cantidad { get; set; }
        public decimal pre_venta { get; set; }
        public decimal igv { get; set; }
        public decimal subtotal { get; set; }
        public decimal monto_real { get; set; }
    }
}
