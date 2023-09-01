using Entidades.Productos;
using Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Ventas.Interface
{
    interface IVentasLogica
    {
        bool ListaUltimasVentas(out List<EntidadLastVentas> lstLastVentas, string placa, string cod_cliente);
        bool GuardarVenta(EntidadVenta objVentas, out string mensaje);
        bool ListarVentasP(out List<EntidadVentasPendientes> lstVentasP);
        bool ListarDetalleVentasP(string codigo_venta, out List<EntidadDetalleVentasPendientes> lstDetalleVentasP);
        bool AnularVenta(string codigo_venta, string codigo_usuario, out string mensaje);
    }
}
