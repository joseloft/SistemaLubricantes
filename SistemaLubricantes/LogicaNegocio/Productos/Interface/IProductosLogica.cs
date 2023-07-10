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
        bool ObtenerCodigoProducto(out string codigoProducto);
        bool ListarMoneda(out List<EntidadMoneda> lstMoneda);
        bool ListarCategoria(out List<EntidadCategoria> lstCategoria);
        bool ListarMarca(string codigoCategoria, out List<EntidadMarca> lstMarca);
        bool ListarTipo(string codigoCategoria, out List<EntidadTipo> lstTipo);
        bool GuardarMarca(string categoriaID, string nombre, out string mensaje);
        bool GuardarTipo(string categoriaID, string descripcion, out string mensaje);
        bool ListarBalde(int parametro, out List<EntidadBalde> lstBalde);
        bool ListarEnvase(int parametro, out List<EntidadEnvase> lstEnvase);
        bool ListarPaquete(int parametro, out List<EntidadPaquete> lstPaquete);
        bool ListarCilindro(int parametro, out List<EntidadCilindro> lstCilindro);
    }
}
