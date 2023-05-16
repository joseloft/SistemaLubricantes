using AccesoDatos.Clientes.Interface;
using Configuracion.Implementacion;
using Entidades.Clientes;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace AccesoDatos.Clientes.Implementacion
{
    public class ClientesDatos : IClientesDatos
    {
        private readonly string context;
        public ClientesDatos(IConfiguration _configuration)
        {
            context = new ConfiguracionData(_configuration).GetConnectionString("ConexionBdComercio");
        }
        public ClientesDatos(string _DbConexion)
        {
            context = _DbConexion;
        }
        public bool ListarClientes(out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[lista_clientes]", objCnx))
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
        public bool GuardarClientes(EntidadCliente entidadCliente)
        {
            SqlConnection objCnx = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("sp_registro_cliente_web", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter Pnombres = new SqlParameter("@Pnombres", SqlDbType.VarChar, 100);
                    Pnombres.Value = entidadCliente.nombres;
                    objCmd.Parameters.Add(Pnombres);

                    SqlParameter Papellidos = new SqlParameter("@Papellidos", SqlDbType.VarChar, 100);
                    Papellidos.Value = entidadCliente.apellidos;
                    objCmd.Parameters.Add(Papellidos);

                    SqlParameter Pdireccion = new SqlParameter("@Pdireccion", SqlDbType.VarChar, 100);
                    Pdireccion.Value = entidadCliente.direccion;
                    objCmd.Parameters.Add(Pdireccion);

                    SqlParameter Pdistrito = new SqlParameter("@Pdistrito", SqlDbType.VarChar, 100);
                    Pdistrito.Value = entidadCliente.distrito;
                    objCmd.Parameters.Add(Pdistrito);

                    SqlParameter Pcelular = new SqlParameter("@Pcelular", SqlDbType.Char, 9);
                    Pcelular.Value = entidadCliente.celular;
                    objCmd.Parameters.Add(Pcelular);

                    SqlParameter Pcorreo = new SqlParameter("@Pcorreo", SqlDbType.VarChar, 100);
                    Pcorreo.Value = entidadCliente.correo;
                    objCmd.Parameters.Add(Pcorreo);

                    SqlParameter Ptelefono = new SqlParameter("@Ptelefono", SqlDbType.Char, 9);
                    Ptelefono.Value = entidadCliente.telefono;
                    objCmd.Parameters.Add(Ptelefono);

                    SqlParameter Pruc = new SqlParameter("@Pruc", SqlDbType.VarChar, 11);
                    Pruc.Value = entidadCliente.ruc;
                    objCmd.Parameters.Add(Pruc);

                    SqlParameter Pdni = new SqlParameter("@Pdni", SqlDbType.Int);
                    Pdni.Value = entidadCliente.dni;
                    objCmd.Parameters.Add(Pdni);

                    objCnx.Open();
                    var dtr = objCmd.ExecuteReader();
                    if (!dtr.HasRows)
                    {
                        return bRsl;
                    }
                    while (dtr.Read())
                    {
                        bRsl = true;
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
