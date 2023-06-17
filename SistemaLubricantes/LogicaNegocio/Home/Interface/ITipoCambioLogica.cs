using System;
using System.Collections.Generic;
using System.Data;
using Entidades.Home;

namespace LogicaNegocio.Home.Interface
{
    interface ITipoCambioLogica
    {
        bool ListarTipoCambio(out EntidadTipoCambio objTipoCambio);
        bool GuardarTipoCambio(EntidadTipoCambio objTipoCambio, out string mensaje);
    }
}
