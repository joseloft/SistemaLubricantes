
using Microsoft.VisualBasic;

namespace Entidades.Productos
{
    public class EntidadProducto
    {
        public void ReplaceNull()
        {
            marca = marca ?? "";
            medida = medida ?? "";
            moneda = moneda ?? "";
            litroXBalde = litroXBalde ?? 0;
        }
        public string cod_categoria { get; set; }
        public string cod_producto { get; set; }
        public string nombre { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string medida { get; set; }
        public int stock { get; set; }
        public decimal precio_compra { get; set; }
        public float porcentaje_venta { get; set; }
        public decimal precio_venta { get; set; }
        public string moneda { get; set; }        
        public string codigo_UM { get; set; }
        public string codigo_tipo { get; set; }
        public string codigo_marca { get; set; }
        public string tipo_moneda { get;set; }
        public decimal? litroXBalde { get; set; }        
        
    }
}
