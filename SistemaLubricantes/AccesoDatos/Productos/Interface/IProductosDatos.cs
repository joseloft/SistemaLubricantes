using System.Data;

namespace AccesoDatos.Productos.Interface
{
    interface IProductosDatos
    {
        bool ListarProductos(out DataTable objDtt, int parametro, string producto);
    }
}
