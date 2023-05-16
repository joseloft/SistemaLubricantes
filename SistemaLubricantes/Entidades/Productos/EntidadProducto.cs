
namespace Entidades.Productos
{
    public class EntidadProducto
    {
        public string cod_producto { get; set; }
        public string nombre { get; set; }
        public string marca { get; set; }
        public string medida { get; set; }
        public float stock { get; set; }
        public decimal precio_compra { get; set; }
        public int porcentaje_venta { get; set; }
        public decimal precio_venta { get; set; }
        public decimal litroXBalde { get; set; }
        public string moneda { get; set; }
        public string modelo { get; set; }
    }
}
