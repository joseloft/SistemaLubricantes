using AccesoDatos.Productos.Interface;
using Configuracion.Implementacion;
using Entidades.Clientes;
using Entidades.Productos;
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
        public bool ListarProductos(out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_listar_productos]", objCnx))
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
        public bool GuardarProductos(EntidadProducto objProducto, out string mensaje)
        {
            objProducto.ReplaceNull();
            SqlConnection objCnx = null;
            var bRsl = false;
            mensaje = "";
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_registrar_producto]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Pcod_prod", SqlDbType.Char, 6).Value = objProducto.cod_producto;
                    objCmd.Parameters.Add("@Pnombre", SqlDbType.VarChar, 100).Value = objProducto.nombre;
                    objCmd.Parameters.Add("@PtipoMoneda", SqlDbType.Char, 2).Value = objProducto.tipo_moneda;
                    objCmd.Parameters.Add("@Pcodigo_cat", SqlDbType.Char, 6).Value = objProducto.cod_categoria;
                    objCmd.Parameters.Add("@Pcodigo_tipo", SqlDbType.Char, 8).Value = objProducto.codigo_tipo;
                    objCmd.Parameters.Add("@Pmodelo", SqlDbType.VarChar, 100).Value = objProducto.modelo;
                    objCmd.Parameters.Add("@Pcodigo_marca", SqlDbType.Char, 6).Value = objProducto.codigo_marca;
                    objCmd.Parameters.Add("@Pstock", SqlDbType.Int).Value = objProducto.stock;
                    objCmd.Parameters.Add("@Pprec_venta", SqlDbType.Decimal).Value = objProducto.precio_venta;
                    objCmd.Parameters.Add("@Pprec_compra", SqlDbType.Decimal).Value = objProducto.precio_compra;
                    objCmd.Parameters.Add("@Pcodigo_UM", SqlDbType.Char, 6).Value = objProducto.codigo_UM;
                    objCmd.Parameters.Add("@Pcodigo_UMC", SqlDbType.Char, 8).Value = objProducto.codigo_UMC;

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
        public bool ObtenerCodigoProducto(out string codigoProducto)
        {
            SqlConnection objCnx = null;
            var bRsl = false;
            codigoProducto = "";
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_correlativo_producto]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;                    

                    objCnx.Open();
                    var dtr = objCmd.ExecuteReader();
                    if (!dtr.HasRows)
                    {
                        codigoProducto = "";
                        return bRsl;
                    }
                    while (dtr.Read())
                    {
                        bRsl = true;
                        codigoProducto = dtr[0].ToString();
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
        public bool ListarMoneda(out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_moneda]", objCnx))
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
        public bool ListarCategoria(out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_lista_Categorias]", objCnx))
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
        public bool ListarMarca(string codigoCategoria, out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_marca_x_categoria]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter PcodigoCategoria = new SqlParameter("@codigo_Cat", SqlDbType.Char, 6);
                    PcodigoCategoria.Value = codigoCategoria;
                    objCmd.Parameters.Add(PcodigoCategoria);

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
        public bool ListarTipo(string codigoCategoria, out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_tipo_x_categoria]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter PcodigoCategoria = new SqlParameter("@codigo_Cat", SqlDbType.Char, 6);
                    PcodigoCategoria.Value = codigoCategoria;
                    objCmd.Parameters.Add(PcodigoCategoria);

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
        public bool GuardarMarca(string categoriaID, string nombre, out string mensaje)
        {
            SqlConnection objCnx = null;
            var bRsl = false;
            mensaje = "";
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_agregar_marca]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter PcategoriaID = new SqlParameter("@Pcategoria", SqlDbType.Char, 6);
                    PcategoriaID.Value = categoriaID;
                    objCmd.Parameters.Add(PcategoriaID);

                    SqlParameter Pnombre = new SqlParameter("@Pnombre", SqlDbType.VarChar, 100);
                    Pnombre.Value = nombre;
                    objCmd.Parameters.Add(Pnombre);

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
        public bool GuardarTipo(string categoriaID, string descripcion, out string mensaje)
        {
            SqlConnection objCnx = null;
            var bRsl = false;
            mensaje = "";
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_agregar_tipo]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter PcategoriaID = new SqlParameter("@Pcategoria", SqlDbType.Char, 6);
                    PcategoriaID.Value = categoriaID;
                    objCmd.Parameters.Add(PcategoriaID);

                    SqlParameter Pdescripcion = new SqlParameter("@Pdescripcion", SqlDbType.VarChar, 100);
                    Pdescripcion.Value = descripcion;
                    objCmd.Parameters.Add(Pdescripcion);

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
        public bool ListarBalde(int parametro, out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_Unidad_Medida_x_tipo]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter Pparametro = new SqlParameter("@parametro", SqlDbType.Int);
                    Pparametro.Value = parametro;
                    objCmd.Parameters.Add(Pparametro);

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
        public bool ListarEnvase(int parametro, out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_Unidad_Medida_x_tipo]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter Pparametro = new SqlParameter("@parametro", SqlDbType.Int);
                    Pparametro.Value = parametro;
                    objCmd.Parameters.Add(Pparametro);

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
        public bool ListarPaquete(int parametro, out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_Unidad_Medida_x_tipo]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter Pparametro = new SqlParameter("@parametro", SqlDbType.Int);
                    Pparametro.Value = parametro;
                    objCmd.Parameters.Add(Pparametro);

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
        public bool ListarCilindro(int parametro, out DataTable objDtt)
        {
            SqlConnection objCnx = null;
            SqlDataReader objDtr = null;
            var bRsl = false;
            try
            {
                objCnx = new SqlConnection(this.context);
                using (var objCmd = new SqlCommand("[dbo].[sp_Unidad_Medida_x_tipo]", objCnx))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter Pparametro = new SqlParameter("@parametro", SqlDbType.Int);
                    Pparametro.Value = parametro;
                    objCmd.Parameters.Add(Pparametro);

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
