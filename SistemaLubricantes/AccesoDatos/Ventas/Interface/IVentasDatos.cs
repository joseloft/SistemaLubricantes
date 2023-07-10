using Entidades.Ventas;
using System.Data;

namespace AccesoDatos.Ventas.Interface
{
    interface IVentasDatos
    {
        bool ListaUltimasVentas(out DataSet _objDts, string placa, string cod_cliente);
        bool GuardarVenta(EntidadVenta objVentas, out string mensaje);
        bool ListarVentasP(out DataTable objDtt);
        bool ListarDetalleVentasP(string codigo_venta, out DataTable objDtt);
    }
}
