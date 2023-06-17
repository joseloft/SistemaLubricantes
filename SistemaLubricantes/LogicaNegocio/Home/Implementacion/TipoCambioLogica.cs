using AccesoDatos.Home.Implementacion;
using Microsoft.Extensions.Configuration;
using System;
using Entidades.Home;
using LogicaNegocio.Home.Interface;
using System.Data;
using System.Collections.Generic;

namespace LogicaNegocio.Home.Implementacion
{
    public class TipoCambioLogica: ITipoCambioLogica
    {
        private TipoCambioDatos _tipoCambioDatos;
        private readonly IConfiguration _configuration;
        public TipoCambioLogica(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _tipoCambioDatos = new TipoCambioDatos(_configuration);
        }
        public TipoCambioLogica(string _DbConexion)
        {
            _tipoCambioDatos = new TipoCambioDatos(_DbConexion);
        }
        public bool ListarTipoCambio(out EntidadTipoCambio objTipoCambio)
        {
            DataTable objDtt = null;
            var bRsl = _tipoCambioDatos.ListarTipoCambio(out objDtt);
            if (!bRsl)
            {
                objTipoCambio = null;
                return bRsl;
            }
            objTipoCambio = new EntidadTipoCambio();
            EntidadTipoCambio objTC = null;
            foreach (DataRow dataRow in objDtt.Rows)
            {
                objTC = new EntidadTipoCambio()
                {
                    tipoCambioID = Convert.ToInt32(dataRow["tipoCambioID"]),
                    tipoCambio = Convert.ToDecimal(dataRow["tipoCambio"])
                };
            }

            objTipoCambio = objTC;
            return bRsl;
        }
        public bool GuardarTipoCambio(EntidadTipoCambio objTipoCambio, out string mensaje)
        {
            return _tipoCambioDatos.GuardarTipoCambio(objTipoCambio, out mensaje);
        }
    }
}
