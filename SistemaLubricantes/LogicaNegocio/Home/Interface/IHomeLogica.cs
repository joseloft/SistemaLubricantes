using System;
using System.Collections.Generic;
using System.Data;
using Entidades.Home;

namespace LogicaNegocio.Home.Interface
{
    interface IHomeLogica
    {
        bool ListarTipoCambio(out EntidadTipoCambio objTipoCambio);
        bool GuardarTipoCambio(EntidadTipoCambio objTipoCambio, out string mensaje);
        bool DashboardPagos(out EntidadDashboardPago objDashboardPago);
        bool DashboardIndicadores(out EntidadDashboardIndicadores objDashboardIndicadores);
        bool DashboardNotificaciones(out EntidadDashboardNotificaciones objDashboardNotificaciones);
    }
}
