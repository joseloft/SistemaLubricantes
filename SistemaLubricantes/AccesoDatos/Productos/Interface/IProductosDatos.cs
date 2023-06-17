using Entidades.Clientes;
using Entidades.Productos;
using System.Data;

namespace AccesoDatos.Productos.Interface
{
    interface IProductosDatos
    {
        bool ListarProductos(out DataTable objDtt);
        bool GuardarProductos(EntidadProducto objProducto, out string mensaje);
        bool ListarMoneda(out DataTable objDtt);
        bool ListarCategoria(out DataTable objDtt);
        bool ListarMarca(out DataTable objDtt);
    }
}
