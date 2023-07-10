using AccesoDatos.Caja.Implementacion;
using AccesoDatos.Productos.Implementacion;
using AccesoDatos.Ventas.Implementacion;
using Entidades.Productos;
using Entidades.Ventas;
using LogicaNegocio.Ventas.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios;

namespace LogicaNegocio.Ventas.Implementacion
{
    public class VentasLogica: IVentasLogica
    {
        private VentasDatos _ventasDatos;
        private readonly IConfiguration _configuration;
        public VentasLogica(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _ventasDatos = new VentasDatos(_configuration);
        }
        public VentasLogica(string _DbConexion)
        {
            _ventasDatos = new VentasDatos(_DbConexion);
        }
        public bool ListaUltimasVentas(out List<EntidadLastVentas> lstLastVentas, string placa, string cod_cliente)
        {
            var bRsl = false;
            lstLastVentas = null;
            DataSet objDts;
            if (!_ventasDatos.ListaUltimasVentas(out objDts, placa, cod_cliente))
            {
                return bRsl;
            }
            if (objDts.Tables[0].Rows.Count == 0)
            {
                return bRsl;
            }
            DataTable objLast = objDts.Tables[0];
            DataTable objDetail = objDts.Tables[1];
            List<EntidadLastVentas> lstVentas = new List<EntidadLastVentas>();
            EntidadLastVentas objVentas = null;
            foreach (DataRow row in objLast.Rows)
            {
                objVentas = new EntidadLastVentas()
                {                    
                    cod_venta = row["cod_venta"].ToString(),
                    nro_venta = row["nro_venta"].ToString(),
                    fecha_venta = row["fecha_venta"].ToString(),
                    total = Convert.ToDecimal(row["total"]),
                    estado = row["estado"].ToString(),
                    placa = row["placa"].ToString(),
                    tipo_cambio = Convert.ToDecimal(row["tipo_cambio"])
                };
                var lstLastdetail = (UConvert.ToList<EntidadLastVentaDetalle>(objDetail)).Where(o => o.cod_venta == objVentas.cod_venta).ToList();
                objVentas.detalleLastVenta = new List<EntidadLastVentaDetalle>();
                lstLastdetail.ForEach(item =>
                {
                    objVentas.detalleLastVenta.Add(new EntidadLastVentaDetalle()
                    {
                        cod_venta = item.cod_venta,
                        cod_prod = item.cod_prod,
                        marca = item.marca,
                        producto = item.producto,
                        cantidad = item.cantidad,
                        pre_venta = item.pre_venta
                    });
                });
                lstVentas.Add(objVentas);
            }
            lstLastVentas = lstVentas;
            bRsl = true;
            return bRsl;

        }
        public bool GuardarVenta(EntidadVenta objVentas, out string mensaje)
        {
            return _ventasDatos.GuardarVenta(objVentas, out mensaje);
        }
        public bool ListarVentasP(out List<EntidadVentasPendientes> lstVentasP)
        {
            DataTable objDtt;
            var bRsl = _ventasDatos.ListarVentasP(out objDtt);
            if (!bRsl)
            {
                lstVentasP = null;
                return bRsl;
            };
            lstVentasP = new List<EntidadVentasPendientes>();
            EntidadVentasPendientes objVentP = null;
            foreach (DataRow row in objDtt.Rows)
            {
                objVentP = new EntidadVentasPendientes()
                {
                    codigo_venta = row["cod_venta"].ToString(),
                    tipo_documento = row["tipo_docu"].ToString(),
                    documento = row["documento"].ToString(),
                    cliente = row["cliente"].ToString(),
                    fecha_venta = row["fecha_venta"].ToString(),
                    forma_pago = row["forma_pago"].ToString(),
                    moneda = row["moneda"].ToString(),
                    total = Convert.ToDecimal(row["total"].ToString()),
                    estado = row["estado"].ToString()
                };
                lstVentasP.Add(objVentP);
            }
            return bRsl;
        }
        public bool ListarDetalleVentasP(string codigo_venta, out List<EntidadDetalleVentasPendientes> lstDetalleVentasP)
        {
            DataTable objDtt;
            var bRsl = _ventasDatos.ListarDetalleVentasP(codigo_venta, out objDtt);
            if (!bRsl)
            {
                lstDetalleVentasP = null;
                return bRsl;
            };
            lstDetalleVentasP = new List<EntidadDetalleVentasPendientes>();
            EntidadDetalleVentasPendientes objDetVentP = null;
            foreach (DataRow row in objDtt.Rows)
            {
                objDetVentP = new EntidadDetalleVentasPendientes()
                {
                    nro_detalle = Convert.ToInt32(row["nro_detalle"]),
                    producto = row["producto"].ToString(),
                    marca = row["marca"].ToString(),
                    cantidad = Convert.ToInt32(row["cantidad"]),
                    precio = Convert.ToDecimal(row["precio"]),
                    total = Convert.ToDecimal(row["total"])
                };
                lstDetalleVentasP.Add(objDetVentP);
            }
            return bRsl;
        }
    }
}
