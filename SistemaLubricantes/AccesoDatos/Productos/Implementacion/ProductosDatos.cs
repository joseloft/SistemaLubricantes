using AccesoDatos.Productos.Interface;
using Configuracion.Implementacion;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace AccesoDatos.Productos.Implementacion
{
    public class ProductosDatos : IProductosDatos
    {
        private readonly string context;
        public ProductosDatos(IConfiguration _configuration)
        {
            context = new ConfiguracionData(_configuration).GetConnectionString("ConexionBdComercio");
        }
        public ProductosDatos(string _DbConexion)
        {
            context = _DbConexion;
        }
        public bool ListarProductos(out DataTable objDtt, int parametro, string producto)
        {
            producto = producto ?? "";
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[lista_productos]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@parametro", SqlDbType.Int).Value = parametro;
                    objCmd.Parameters.Add("@producto", SqlDbType.VarChar, 100).Value = producto;

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
    }
}
