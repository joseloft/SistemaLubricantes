using Entidades.Home;
using System.Data;

namespace AccesoDatos.Home.Interface
{
    interface IHomeDatos
    {
        bool ListarTipoCambio(out DataTable objDtt);
        bool GuardarTipoCambio(EntidadTipoCambio objTipoCambio, out string mensaje);
        bool DashboardPagos(out DataSet _objDts);
        bool DashboardIndicadores(out DataSet _objDts);
        bool DashboardNotificaciones(out DataSet _objDts);
    }
}
