using AccesoDatos.Caja.Interface;
using Configuracion.Implementacion;
using Entidades.Caja;
using Entidades.Ventas;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Caja.Implementacion
{
    public class CajaDatos : ICajaDatos
    {
        private readonly string context;
        public CajaDatos(IConfiguration _configuration)
        {
            context = new ConfiguracionData(_configuration).GetConnectionString("ConexionBdComercio");
        }
        public CajaDatos(string _DbConexion)
        {
            context = _DbConexion;
        }        
        public bool ListarTipoComprobante(out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_lista_comprobantes]", objCnx))
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
        public bool ListarTipoPago(out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_lista_tipo_pago]", objCnx))
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
        public bool GuardarComprobante(EntidadComprobante objComprobante, out string mensaje)
        {
            SqlConnection objCnx = null;
            var bRsl = false;
            mensaje = "";
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_registrar_comprobante]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Pcodigo_venta", SqlDbType.Char, 8).Value = objComprobante.codigo_venta;
                    objCmd.Parameters.Add("@Ptipo_comprobante", SqlDbType.Char, 2).Value = objComprobante.tipo_comprobante;
                    objCmd.Parameters.Add("@Ptipo_documento", SqlDbType.Char, 2).Value = objComprobante.tipo_documento;
                    objCmd.Parameters.Add("@Pcodigo_usuario", SqlDbType.Char, 8).Value = objComprobante.codigo_usuario;
                    objCmd.Parameters.Add("@Ptipo_pago", SqlDbType.Char, 1).Value = objComprobante.tipo_pago;

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
    }
}
