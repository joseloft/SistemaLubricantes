using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Senda.Interface
{
    interface ISendaDatos
    {
        bool EmitirComprobante(out DataSet _objDts, string nro_venta);
    }
}
