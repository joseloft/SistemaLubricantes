using AccesoDatos.Login.Interface;
using Configuracion.Implementacion;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Login.Implementacion
{
    public class LoginDatos : ILoginDatos
    {
        private readonly string context;
        public LoginDatos(IConfiguration _configuration)
        {
            context = new ConfiguracionData(_configuration).GetConnectionString("ConexionBdComercio");
        }
        public LoginDatos(string _DbConexion)
        {
            context = _DbConexion;
        }
        public bool Autentificacion(string usuario, string contrasenia, out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_autentificacionForLogin]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter Pusuario = new SqlParameter("@Pusuario", SqlDbType.Char, 8);
                    Pusuario.Value = usuario;
                    objCmd.Parameters.Add(Pusuario);

                    SqlParameter Pcontrasenia = new SqlParameter("@Pcontraseña", SqlDbType.VarChar, 50);
                    Pcontrasenia.Value = contrasenia;
                    objCmd.Parameters.Add(Pcontrasenia);

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
        public bool CambiarClave(string codigo_usuario, string nueva_clave, out string mensaje)
        {
            SqlConnection objCnx = null;
            var bRsl = false;
            mensaje = "";
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_actualizar_contraseña]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter Pcodigo_usuario = new SqlParameter("@Pcodigo_usuario", SqlDbType.Char, 8);
                    Pcodigo_usuario.Value = codigo_usuario;
                    objCmd.Parameters.Add(Pcodigo_usuario);

                    SqlParameter Pnueva_clave = new SqlParameter("@Pnueva_contraseña", SqlDbType.VarChar, 100);
                    Pnueva_clave.Value = nueva_clave;
                    objCmd.Parameters.Add(Pnueva_clave);

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
