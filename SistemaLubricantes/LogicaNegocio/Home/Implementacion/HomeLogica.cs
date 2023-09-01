using AccesoDatos.Home.Implementacion;
using Microsoft.Extensions.Configuration;
using System;
using Entidades.Home;
using LogicaNegocio.Home.Interface;
using System.Data;
using System.Collections.Generic;
using AccesoDatos.Ventas.Implementacion;
using Entidades.Ventas;
using Utilitarios;

namespace LogicaNegocio.Home.Implementacion
{
    public class HomeLogica: IHomeLogica
    {
        private HomeDatos _homeDatos;
        private readonly IConfiguration _configuration;
        public HomeLogica(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _homeDatos = new HomeDatos(_configuration);
        }
        public HomeLogica(string _DbConexion)
        {
            _homeDatos = new HomeDatos(_DbConexion);
        }
        public bool ListarTipoCambio(out EntidadTipoCambio objTipoCambio)
        {
            DataTable objDtt = null;
            var bRsl = _homeDatos.ListarTipoCambio(out objDtt);
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
            return _homeDatos.GuardarTipoCambio(objTipoCambio, out mensaje);
        }
        public bool DashboardPagos(out EntidadDashboardPago objDashboardPago)
        {
            var bRsl = false;
            objDashboardPago = null;
            DataSet objDts;
            if (!_homeDatos.DashboardPagos(out objDts))
            {
                return bRsl;
            }
            DataRow dttEfectivo = objDts.Tables[0].Rows[0];
            DataRow dttTarjeta = objDts.Tables[1].Rows[0];
            DataRow dttYape = objDts.Tables[2].Rows[0];
            DataRow dttPlin = objDts.Tables[3].Rows[0];
            DataRow dttCredito = objDts.Tables[4].Rows[0];
            DataRow dttContado = objDts.Tables[5].Rows[0];

            objDashboardPago = new EntidadDashboardPago();
            objDashboardPago.objEfectivo = new EntidadEfectivo()
            {
                codigo = dttEfectivo["codigo"].ToString(),
                tipoPago = dttEfectivo["tipoPago"].ToString(),
                montoPago = decimal.Parse(dttEfectivo["montoPago"].ToString())
            };
            objDashboardPago.objTarjeta = new EntidadTarjeta()
            {
                codigo = dttTarjeta["codigo"].ToString(),
                tipoPago = dttTarjeta["tipoPago"].ToString(),
                montoPago = decimal.Parse(dttTarjeta["montoPago"].ToString())
            };
            objDashboardPago.objYape = new EntidadYape()
            {
                codigo = dttYape["codigo"].ToString(),
                tipoPago = dttYape["tipoPago"].ToString(),
                montoPago = decimal.Parse(dttYape["montoPago"].ToString())
            };
            objDashboardPago.objPlin = new EntidadPlin()
            {
                codigo = dttPlin["codigo"].ToString(),
                tipoPago = dttPlin["tipoPago"].ToString(),
                montoPago = decimal.Parse(dttPlin["montoPago"].ToString())
            };
            objDashboardPago.objCredito = new EntidadCredito()
            {
                codigo = dttCredito["codigo"].ToString(),
                tipoPago = dttCredito["tipoPago"].ToString(),
                montoPago = decimal.Parse(dttCredito["montoPago"].ToString())
            };
            objDashboardPago.objContado = new EntidadContado()
            {
                codigo = dttContado["codigo"].ToString(),
                tipoPago = dttContado["tipoPago"].ToString(),
                montoPago = decimal.Parse(dttContado["montoPago"].ToString())
            };
            
            bRsl = true;
            return bRsl;

        }
        public bool DashboardIndicadores(out EntidadDashboardIndicadores objDashboardIndicadores)
        {
            var bRsl = false;
            objDashboardIndicadores = null;
            DataSet objDts;
            if (!_homeDatos.DashboardIndicadores(out objDts))
            {
                return bRsl;
            }
            DataRow dttTotalVentas = objDts.Tables[0].Rows[0];
            DataRow dttCantidadVentas = objDts.Tables[1].Rows[0];
            DataRow dttClientesNuevos = objDts.Tables[2].Rows[0];
            DataRow dttFacturacion = objDts.Tables[3].Rows[0];

            objDashboardIndicadores = new EntidadDashboardIndicadores();
            objDashboardIndicadores.objTotalVentas = new EntidadTotalVentas()
            {
                reporte = dttTotalVentas["reporte"].ToString(),
                montoPago = decimal.Parse(dttTotalVentas["montoPago"].ToString())
            };
            objDashboardIndicadores.objCantidadVentas = new EntidadCantidadVentas()
            {
                reporte = dttCantidadVentas["reporte"].ToString(),
                cantidad = int.Parse(dttCantidadVentas["cantidad"].ToString())
            };
            objDashboardIndicadores.objClientesNuevos = new EntidadClientesNuevos()
            {
                reporte = dttClientesNuevos["reporte"].ToString(),
                cantidad = int.Parse(dttClientesNuevos["cantidad"].ToString())
            };
            objDashboardIndicadores.objFacturacion = new EntidadFacturacion()
            {
                reporte = dttFacturacion["reporte"].ToString(),
                montoPago = decimal.Parse(dttFacturacion["montoPago"].ToString())
            };

            bRsl = true;
            return bRsl;

        }
        public bool DashboardNotificaciones(out EntidadDashboardNotificaciones objDashboardNotificaciones)
        {
            var bRsl = false;
            objDashboardNotificaciones = null;
            DataSet objDts;
            if (!_homeDatos.DashboardNotificaciones(out objDts))
            {
                return bRsl;
            }
            
            objDashboardNotificaciones = new EntidadDashboardNotificaciones();

            DataTable dttProductosStock = objDts.Tables[0];
            objDashboardNotificaciones.lstProductosPocoStock = new List<EntidadProductosStock>();
            foreach (DataRow objDtp in dttProductosStock.Rows)
            {
                objDashboardNotificaciones.lstProductosPocoStock.Add(new EntidadProductosStock()
                {
                    item = int.Parse(objDtp["item"].ToString()),
                    codigo_producto = objDtp["codigo_producto"].ToString(),
                    marca = objDtp["marca"].ToString(),
                    producto = objDtp["producto"].ToString(),
                    stock = float.Parse(objDtp["stock"].ToString()),
                    categoria = objDtp["categoria"].ToString(),
                    precio_venta = decimal.Parse(objDtp["precio_venta"].ToString())
                });
            }

            objDashboardNotificaciones.lstVentasSinComprobante = new List<EntidadVentasComprobante>();
            EntidadVentasComprobante detVentas;
            DataTable dttVentasComprobante = objDts.Tables[1];            
            foreach (DataRow objDtv in dttVentasComprobante.Rows)
            {
                detVentas = new EntidadVentasComprobante();
                detVentas.item = int.Parse(objDtv["item"].ToString());
                detVentas.codigo_venta = objDtv["codigo_venta"].ToString();
                detVentas.cliente = objDtv["cliente"].ToString();
                detVentas.fecha_venta = objDtv["fecha_venta"].ToString();
                detVentas.total = decimal.Parse(objDtv["total"].ToString());
                detVentas.estado = objDtv["estado"].ToString();
                detVentas.placa = objDtv["placa"].ToString();
                objDashboardNotificaciones.lstVentasSinComprobante.Add(detVentas);
            }

            DataTable dttCreditosVencer = objDts.Tables[2];
            objDashboardNotificaciones.lstCreditosPorVencer = new List<EntidadCreditosVencidos>();
            foreach (DataRow objDtr in dttCreditosVencer.Rows)
            {
                objDashboardNotificaciones.lstCreditosPorVencer.Add(new EntidadCreditosVencidos()
                {
                    item = int.Parse(objDtr["item"].ToString()),
                    codigo_venta = objDtr["codigo_venta"].ToString(),
                    cliente = objDtr["cliente"].ToString(),
                    monto_venta = decimal.Parse(objDtr["monto_venta"].ToString()),
                    monto_credito = decimal.Parse(objDtr["monto_credito"].ToString()),
                    fecha_vencimiento = objDtr["fecha_vencimiento"].ToString(),
                    dias_credito = int.Parse(objDtr["dias_credito"].ToString())
                });
            }

            objDashboardNotificaciones.lstClientesSinCorreo = new List<EntidadaClientesCorreo>();
            EntidadaClientesCorreo detClientes;
            DataTable dttClientesCorreo = objDts.Tables[3];            
            foreach (DataRow objDtc in dttClientesCorreo.Rows)
            {
                detClientes = new EntidadaClientesCorreo();
                detClientes.item = int.Parse(objDtc["item"].ToString());
                detClientes.codigo_cliente = objDtc["codigo_cliente"].ToString();
                detClientes.cliente = objDtc["cliente"].ToString();
                detClientes.fecha_registro = objDtc["fecha_registro"].ToString();
                detClientes.tipo_cliente = objDtc["tipo_cliente"].ToString();
                detClientes.documento = objDtc["documento"].ToString();
                objDashboardNotificaciones.lstClientesSinCorreo.Add(detClientes);
            }

            bRsl = true;
            return bRsl;

        }
    }
}
