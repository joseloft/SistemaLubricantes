using AccesoDatos.Ventas.Interface;
using Configuracion.Implementacion;
using Entidades.Productos;
using Entidades.Ventas;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Ventas.Implementacion
{
    public class VentasDatos : IVentasDatos
    {
        private readonly string context;
        public VentasDatos(IConfiguration _configuration)
        {
            context = new ConfiguracionData(_configuration).GetConnectionString("ConexionBdComercio");
        }
        public VentasDatos(string _DbConexion)
        {
            context = _DbConexion;
        }
        public bool ListaUltimasVentas(out DataSet _objDts, string placa, string cod_cliente)
        {
            placa = placa ?? "";
            cod_cliente = cod_cliente ?? "";
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_listar_ventas_placaCliente]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter Pplaca = new SqlParameter("@Pplaca", SqlDbType.Char, 10);
                    Pplaca.Value = placa;
                    objCmd.Parameters.Add(Pplaca);

                    SqlParameter Pcod_cliente = new SqlParameter("@Pcod_cliente", SqlDbType.Char, 8);
                    Pcod_cliente.Value = cod_cliente;
                    objCmd.Parameters.Add(Pcod_cliente);

                    objCnx.Open();
                    var objDta = new SqlDataAdapter();
                    objDta.SelectCommand = objCmd;
                    var objDts = new DataSet();
                    objDta.Fill(objDts);
                    _objDts = objDts;
                    bRsl = true;
                }
            }
            catch (System.Exception ex)
            {
                throw; //new System.Exception(ex.Message);
            }
            finally
            {
                try
                {
                    if (objDtr != null && !objDtr.IsClosed) objDtr.Close();
                    if (objCnx != null && objCnx.State == ConnectionState.Open)
                    {
                        objCnx.Close();
                    }
                }
                catch (System.Exception)
                {

                }
            }
            return bRsl;
        }
        public bool GuardarVenta(EntidadVenta objVentas, out string mensaje)
        {
            objVentas.ReplaceNull();
            SqlConnection objCnx = null;
            var bRsl = false;
            mensaje = "";
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_registrar_venta]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Pcod_venta", SqlDbType.Char, 8).Value = objVentas.codigo_venta;
                    objCmd.Parameters.Add("@Pcod_cliente", SqlDbType.Char, 8).Value = objVentas.codigo_cliente;
                    objCmd.Parameters.Add("@Pcod_usuario", SqlDbType.Char, 8).Value = objVentas.codigo_usuario;
                    objCmd.Parameters.Add("@Ptotal", SqlDbType.Decimal).Value = objVentas.total;
                    objCmd.Parameters.Add("@PsubTotal", SqlDbType.Decimal).Value = objVentas.sub_total;
                    objCmd.Parameters.Add("@Pigv", SqlDbType.Decimal).Value = objVentas.igv;
                    objCmd.Parameters.Add("@Pplaca", SqlDbType.Char, 8).Value = objVentas.placa;
                    objCmd.Parameters.Add("@Pcod_tc", SqlDbType.Int).Value = objVentas.codigo_tc;
                    objCmd.Parameters.Add("@Pcondicion", SqlDbType.Bit).Value = objVentas.condicion;

                    var dtw = new DataTable();
                    dtw.Columns.Add("cod_venta", typeof(string));
                    dtw.Columns.Add("cod_prod", typeof(string));
                    dtw.Columns.Add("cantidad", typeof(float));
                    dtw.Columns.Add("pre_venta", typeof(decimal));
                    dtw.Columns.Add("igv", typeof(decimal));
                    dtw.Columns.Add("subtotal", typeof(decimal));
                    dtw.Columns.Add("monto_real", typeof(decimal));

                    foreach (var item in objVentas.detalleVenta)
                    {
                        dtw.Rows.Add(item.cod_venta, item.cod_prod, item.cantidad, item.pre_venta,
                                     item.igv, item.subtotal, item.monto_real);
                    }

                    var PdetalleVenta = new SqlParameter("@PdetalleVenta", SqlDbType.Structured);
                    PdetalleVenta.Value = dtw;
                    PdetalleVenta.TypeName = "[dbo].[t_detalleVenta]";
                    objCmd.Parameters.Add(PdetalleVenta);

                    objCnx.Open();
                    var dtr = objCmd.ExecuteReader();
                    if (!dtr.HasRows)
                    {
                        mensaje = "";
                        return bRsl;
                    }
                    while (dtr.Read())
                    {
                        bRsl = true;
                        mensaje = dtr[0].ToString();
                    }

                }
            }
            catch (System.Exception ex)
            {
                throw; //new System.Exception(ex.Message);
            }
            finally
            {
                try
                {
                    if (objCnx != null && objCnx.State == ConnectionState.Open)
                    {
                        objCnx.Close();
                    }
                }
                catch (System.Exception)
                {

                }
            }

            return bRsl;
        }
        public bool ListarVentasP(out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_lista_ventas_pendientes]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCnx.Open();
                    objDtr = objCmd.ExecuteReader();
                    var _objDtt = new DataTable();
                    _objDtt.Load(objDtr);
                    objDtt = _objDtt;
                    bRsl = true;
                }
            }
            catch (System.Exception ex)
            {
                throw; //new System.Exception(ex.Message);
            }
            finally
            {
                try
                {
                    if (objDtr != null && !objDtr.IsClosed) objDtr.Close();
                    if (objCnx != null && objCnx.State == ConnectionState.Open)
                    {
                        objCnx.Close();
                    }
                }
                catch (System.Exception)
                {

                }
            }

            return bRsl;
        }
        public bool ListarDetalleVentasP(string codigo_venta, out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_lista_detalle_ventas_pendientes]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter Pcodigo_venta = new SqlParameter("@Pcodigo_venta", SqlDbType.Char, 8);
                    Pcodigo_venta.Value = codigo_venta;
                    objCmd.Parameters.Add(Pcodigo_venta);

                    objCnx.Open();
                    objDtr = objCmd.ExecuteReader();
                    var _objDtt = new DataTable();
                    _objDtt.Load(objDtr);
                    objDtt = _objDtt;
                    bRsl = true;
                }
            }
            catch (System.Exception ex)
            {
                throw; //new System.Exception(ex.Message);
            }
            finally
            {
                try
                {
                    if (objDtr != null && !objDtr.IsClosed) objDtr.Close();
                    if (objCnx != null && objCnx.State == ConnectionState.Open)
                    {
                        objCnx.Close();
                    }
                }
                catch (System.Exception)
                {

                }
            }

            return bRsl;
        }
        public bool AnularVenta(string codigo_venta, string codigo_usuario, out string mensaje)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            mensaje = "";
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_anular_venta]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter Pcodigo_venta = new SqlParameter("@Pcod_venta", SqlDbType.Char, 8);
                    Pcodigo_venta.Value = codigo_venta;
                    objCmd.Parameters.Add(Pcodigo_venta);

                    SqlParameter Pcodigo_usuario = new SqlParameter("@Pcod_usuario", SqlDbType.Char, 8);
                    Pcodigo_usuario.Value = codigo_usuario;
                    objCmd.Parameters.Add(Pcodigo_usuario);

                    objCnx.Open();
                    var dtr = objCmd.ExecuteReader();
                    if (!dtr.HasRows)
                    {
                        mensaje = "";
                        return bRsl;
                    }
                    while (dtr.Read())
                    {
                        bRsl = true;
                        mensaje = dtr[0].ToString();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw; //new System.Exception(ex.Message);
            }
            finally
            {
                try
                {
                    if (objDtr != null && !objDtr.IsClosed) objDtr.Close();
                    if (objCnx != null && objCnx.State == ConnectionState.Open)
                    {
                        objCnx.Close();
                    }
                }
                catch (System.Exception)
                {

                }
            }

            return bRsl;
        }
    }
}
