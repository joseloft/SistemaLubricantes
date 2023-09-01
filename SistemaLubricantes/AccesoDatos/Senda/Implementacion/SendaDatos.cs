using AccesoDatos.Senda.Interface;
using Configuracion.Implementacion;
using Entidades.Ventas;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Senda.Implementacion
{
    public class SendaDatos : ISendaDatos
    {
        private readonly string context;
        public SendaDatos(IConfiguration _configuration)
        {
            context = new ConfiguracionData(_configuration).GetConnectionString("ConexionBdComercio");
        }
        public SendaDatos(string _DbConexion)
        {
            context = _DbConexion;
        }

        public bool EmitirComprobante(out DataSet _objDts, string nro_venta)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[enviar_comprobante_electronico]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter Pnro_venta = new SqlParameter("@Pnro_venta", SqlDbType.Char, 8);
                    Pnro_venta.Value = nro_venta;
                    objCmd.Parameters.Add(Pnro_venta);

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
    }
}
