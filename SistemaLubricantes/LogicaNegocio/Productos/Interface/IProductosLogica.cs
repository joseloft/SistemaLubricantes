using System.Collections.Generic;
using System.Data;
using Entidades.Productos;

namespace LogicaNegocio.Productos.Interface
{
    interface IProductosLogica
    {
        bool ListarProductos(out List<EntidadProducto> lstProductos, int parametro, string producto);
    }
}
