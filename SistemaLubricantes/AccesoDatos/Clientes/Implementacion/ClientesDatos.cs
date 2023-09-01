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
                using (var objCmd = new SqlCommand("[dbo].[sp_listar_clientes]", objCnx))
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
        public bool GuardarClientes(EntidadCliente objCliente, out string mensaje)
        {
            objCliente.ReplaceNull();
            SqlConnection objCnx = null;
            var bRsl = false;
            mensaje = "";
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_registrar_cliente]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter Pnombres = new SqlParameter("@Pnombres", SqlDbType.VarChar, 100);
                    Pnombres.Value = objCliente.nombres;
                    objCmd.Parameters.Add(Pnombres);

                    SqlParameter Papellidos = new SqlParameter("@Papellidos", SqlDbType.VarChar, 100);
                    Papellidos.Value = objCliente.apellidos;
                    objCmd.Parameters.Add(Papellidos);

                    SqlParameter Pdistrito = new SqlParameter("@Pdistrito", SqlDbType.VarChar, 100);
                    Pdistrito.Value = objCliente.distrito;
                    objCmd.Parameters.Add(Pdistrito);

                    SqlParameter Pdireccion = new SqlParameter("@Pdireccion", SqlDbType.VarChar, 100);
                    Pdireccion.Value = objCliente.direccion;
                    objCmd.Parameters.Add(Pdireccion);                    

                    SqlParameter Pcelular = new SqlParameter("@Pcelular", SqlDbType.Char, 9);
                    Pcelular.Value = objCliente.celular;
                    objCmd.Parameters.Add(Pcelular);

                    SqlParameter Pcorreo = new SqlParameter("@Pcorreo", SqlDbType.VarChar, 100);
                    Pcorreo.Value = objCliente.correo;
                    objCmd.Parameters.Add(Pcorreo);

                    SqlParameter Ptelefono = new SqlParameter("@Ptelefono", SqlDbType.Char, 9);
                    Ptelefono.Value = objCliente.telefono;
                    objCmd.Parameters.Add(Ptelefono);

                    SqlParameter PtipoCliente = new SqlParameter("@Ptipo", SqlDbType.Char, 1);
                    PtipoCliente.Value = objCliente.tipoCliente;
                    objCmd.Parameters.Add(PtipoCliente);

                    SqlParameter PnroDocumento = new SqlParameter("@Pdocumento", SqlDbType.Char, 11);
                    PnroDocumento.Value = objCliente.nroDocumento;
                    objCmd.Parameters.Add(PnroDocumento);                                       

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
        public bool BuscarClientes(string cod_cliente, string documento, string placa, out DataTable objDtt)
        {
            cod_cliente = cod_cliente ?? "";
            documento = documento ?? "";
            placa = placa ?? "";
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_buscar_clientes]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Pcod_cliente", SqlDbType.Char, 8).Value = cod_cliente;
                    objCmd.Parameters.Add("@Pdocumento", SqlDbType.VarChar, 11).Value = documento;
                    objCmd.Parameters.Add("@Pplaca", SqlDbType.VarChar, 10).Value = placa;

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
