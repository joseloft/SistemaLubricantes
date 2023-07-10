using Entidades.Clientes;
using Entidades.Productos;
using System.Data;

namespace AccesoDatos.Productos.Interface
{
    interface IProductosDatos
    {
        bool ListarProductos(out DataTable objDtt);
        bool GuardarProductos(EntidadProducto objProducto, out string mensaje);
        bool ObtenerCodigoProducto(out string codigoProducto);
        bool ListarMoneda(out DataTable objDtt);
        bool ListarCategoria(out DataTable objDtt);
        bool ListarMarca(string codigoCategoria, out DataTable objDtt);
        bool ListarTipo(string codigoCategoria, out DataTable objDtt);
        bool GuardarMarca(string categoriaID, string nombre, out string mensaje);
        bool GuardarTipo(string categoriaID, string descripcion, out string mensaje);
        bool ListarBalde(int parametro, out DataTable objDtt);
        bool ListarEnvase(int parametro, out DataTable objDtt);
        bool ListarPaquete(int parametro, out DataTable objDtt);
        bool ListarCilindro(int parametro, out DataTable objDtt);
    }
}
