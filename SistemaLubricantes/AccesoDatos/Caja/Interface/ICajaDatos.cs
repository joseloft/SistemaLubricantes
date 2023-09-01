using Entidades.Caja;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Caja.Interface
{
    interface ICajaDatos
    {       
        bool ListarTipoComprobante(out DataTable objDtt);
        bool ListarTipoPago(out DataTable objDtt);
        bool GuardarComprobante(EntidadComprobante objComprobante, out string mensaje);
        bool GuardarCredito(EntidadCredito objCredito, out string mensaje);
        bool GuardarPago(EntidadPago objPago, out string mensaje);
    }
}
