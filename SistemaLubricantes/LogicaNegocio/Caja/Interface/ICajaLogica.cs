using Entidades.Caja;
using Entidades.Clientes;
using Entidades.Productos;
using Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Caja.Interface
{
    interface ICajaLogica
    {        
        bool ListarTipoComprobante(out List<EntidadTipoComprobante> lstComprobante);
        bool ListarTipoPago(out List<EntidadTipoPago> lstTipoPago);
        bool GuardarComprobante(EntidadComprobante objComprobante, out string mensaje);
        bool GuardarCredito(EntidadCredito objCredito, out string mensaje);
        bool GuardarPago(EntidadPago objPago, out string mensaje);
    }
}
