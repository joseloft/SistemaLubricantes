using AccesoDatos.Caja.Implementacion;
using AccesoDatos.Clientes.Implementacion;
using AccesoDatos.Productos.Implementacion;
using AccesoDatos.Ventas.Implementacion;
using Entidades.Caja;
using Entidades.Clientes;
using Entidades.Productos;
using Entidades.Ventas;
using LogicaNegocio.Caja.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Caja.Implementacion
{
    public class CajaLogica : ICajaLogica
    {
        private CajaDatos _cajaDatos;
        private readonly IConfiguration _configuration;
        public CajaLogica(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _cajaDatos = new CajaDatos(_configuration);
        }
        public CajaLogica(string _DbConexion)
        {
            _cajaDatos = new CajaDatos(_DbConexion);
        }        
        public bool ListarTipoComprobante(out List<EntidadTipoComprobante> lstComprobante)
        {
            DataTable objDtt;
            var bRsl = _cajaDatos.ListarTipoComprobante(out objDtt);
            if (!bRsl)
            {
                lstComprobante = null;
                return bRsl;
            };
            lstComprobante = new List<EntidadTipoComprobante>();
            EntidadTipoComprobante objC = null;
            foreach (DataRow row in objDtt.Rows)
            {
                objC = new EntidadTipoComprobante()
                {
                    comprobante_id = row["tipo_comp"].ToString(),
                    comprobante = row["descripcion"].ToString()
                };
                lstComprobante.Add(objC);
            }
            return bRsl;
        }
        public bool ListarTipoPago(out List<EntidadTipoPago> lstTipoPago)
        {
            DataTable objDtt;
            var bRsl = _cajaDatos.ListarTipoPago(out objDtt);
            if (!bRsl)
            {
                lstTipoPago = null;
                return bRsl;
            };
            lstTipoPago = new List<EntidadTipoPago>();
            EntidadTipoPago objP = null;
            foreach (DataRow row in objDtt.Rows)
            {
                objP = new EntidadTipoPago()
                {
                    codigo_tipo_pago = row["codigo"].ToString(),
                    tipo_pago = row["descripcion"].ToString()
                };
                lstTipoPago.Add(objP);
            }
            return bRsl;
        }
        public bool GuardarComprobante(EntidadComprobante objComprobante, out string mensaje)
        {
            return _cajaDatos.GuardarComprobante(objComprobante, out mensaje);
        }
        public bool GuardarCredito(EntidadCredito objCredito, out string mensaje)
        {
            return _cajaDatos.GuardarCredito(objCredito, out mensaje);
        }
        public bool GuardarPago(EntidadPago objPago, out string mensaje)
        {
            return _cajaDatos.GuardarPago(objPago, out mensaje);
        }
    }
}
