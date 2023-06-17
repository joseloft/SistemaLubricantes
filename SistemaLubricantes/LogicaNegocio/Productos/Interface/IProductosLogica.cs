using System.Collections.Generic;
using System.Data;
using Entidades.Clientes;
using Entidades.Productos;

namespace LogicaNegocio.Productos.Interface
{
    interface IProductosLogica
    {
        bool ListarProductos(out List<EntidadProducto> lstProductos);
        bool GuardarProductos(EntidadProducto objProducto, out string mensaje);
        bool ListarMoneda(out List<EntidadMoneda> lstMoneda);
        bool ListarCategoria(out List<EntidadCategoria> lstCategoria);
        bool ListarMarca(out List<EntidadMarca> lstMarca);
    }
}
