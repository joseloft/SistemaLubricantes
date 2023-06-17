using Entidades.Home;
using System.Data;

namespace AccesoDatos.Home.Interface
{
    interface ITipoCambioDatos
    {
        bool ListarTipoCambio(out DataTable objDtt);
        bool GuardarTipoCambio(EntidadTipoCambio objTipoCambio, out string mensaje);
    }
}
