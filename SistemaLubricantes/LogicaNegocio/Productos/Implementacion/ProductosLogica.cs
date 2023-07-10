using LogicaNegocio.Productos.Interface;
using AccesoDatos.Productos.Implementacion;
using Microsoft.Extensions.Configuration;
using System.Data;
using Entidades.Productos;
using System.Collections.Generic;
using System;
using AccesoDatos.Clientes.Implementacion;
using Entidades.Clientes;

namespace LogicaNegocio.Productos.Implementacion
{
    public class ProductosLogica : IProductosLogica
    {
        private ProductosDatos _productosDatos;
        private readonly IConfiguration _configuration;
        public ProductosLogica(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _productosDatos = new ProductosDatos(_configuration);
        }
        public ProductosLogica(string _DbConexion)
        {
            _productosDatos = new ProductosDatos(_DbConexion);
        }
        public bool ListarProductos(out List<EntidadProducto> lstProductos)
        {
            DataTable objDtt;
            var bRsl = _productosDatos.ListarProductos(out objDtt);
            if (!bRsl)
            {
                lstProductos = null;
                return bRsl;
            };
            lstProductos = new List<EntidadProducto>();
            EntidadProducto objPro = null;
            foreach (DataRow row in objDtt.Rows)
            {
                objPro = new EntidadProducto()
                {
                    cod_categoria = row["codigo_cat"].ToString(),
                    cod_producto = row["Cod_Prod"].ToString(),
                    nombre = row["Nombre"].ToString(),
                    marca = row["marca"].ToString(),
                    stock = Convert.ToInt32(row["Stock"]),
                    precio_venta = Convert.ToDecimal(row["Prec_Venta"]),
                    moneda = row["moneda"].ToString()
                };
                lstProductos.Add(objPro);
            }
            return bRsl;
        }
        public bool GuardarProductos(EntidadProducto objProducto, out string mensaje)
        {
            return _productosDatos.GuardarProductos(objProducto, out mensaje);
        }
        public bool ObtenerCodigoProducto(out string codigoProducto)
        {
            return _productosDatos.ObtenerCodigoProducto(out codigoProducto);
        }
        public bool ListarMoneda(out List<EntidadMoneda> lstMoneda)
        {
            DataTable objDtt;
            var bRsl = _productosDatos.ListarMoneda(out objDtt);
            if (!bRsl)
            {
                lstMoneda = null;
                return bRsl;
            };
            lstMoneda = new List<EntidadMoneda>();
            EntidadMoneda objM = null;
            foreach (DataRow row in objDtt.Rows)
            {
                objM = new EntidadMoneda()
                {
                    tipo_moneda = row["tipo_moneda"].ToString(),
                    moneda = row["descripcion"].ToString()
                };
                lstMoneda.Add(objM);
            }
            return bRsl;
        }
        public bool ListarCategoria(out List<EntidadCategoria> lstCategoria)
        {
            DataTable objDtt;
            var bRsl = _productosDatos.ListarCategoria(out objDtt);
            if (!bRsl)
            {
                lstCategoria = null;
                return bRsl;
            };
            lstCategoria = new List<EntidadCategoria>();
            EntidadCategoria objC = null;
            foreach (DataRow row in objDtt.Rows)
            {
                objC = new EntidadCategoria()
                {
                    codigo_categoria = row["Codigo_Cat"].ToString(),
                    categoria = row["Nombre"].ToString()
                };
                lstCategoria.Add(objC);
            }
            return bRsl;
        }
        public bool ListarMarca(string codigoCategoria, out List<EntidadMarca> lstMarca)
        {
            DataTable objDtt;
            var bRsl = _productosDatos.ListarMarca(codigoCategoria, out objDtt);
            if (!bRsl)
            {
                lstMarca = null;
                return bRsl;
            };
            lstMarca = new List<EntidadMarca>();
            EntidadMarca objM = null;
            foreach (DataRow row in objDtt.Rows)
            {
                objM = new EntidadMarca()
                {
                    codigo_marca = row["Codigo_Mar"].ToString(),
                    marca = row["Nombre"].ToString()
                };
                lstMarca.Add(objM);
            }
            return bRsl;
        }
        public bool ListarTipo(string codigoCategoria, out List<EntidadTipo> lstTipo)
        {
            DataTable objDtt;
            var bRsl = _productosDatos.ListarTipo(codigoCategoria, out objDtt);
            if (!bRsl)
            {
                lstTipo = null;
                return bRsl;
            };
            lstTipo = new List<EntidadTipo>();
            EntidadTipo objT = null;
            foreach (DataRow row in objDtt.Rows)
            {
                objT = new EntidadTipo()
                {
                    codigo_tipo = row["codigo_tipo"].ToString(),
                    tipo = row["descripcion"].ToString()
                };
                lstTipo.Add(objT);
            }
            return bRsl;
        }
        public bool GuardarMarca(string categoriaID, string nombre, out string mensaje)
        {
            return _productosDatos.GuardarMarca(categoriaID, nombre, out mensaje);
        }
        public bool GuardarTipo(string categoriaID, string descripcion, out string mensaje)
        {
            return _productosDatos.GuardarTipo(categoriaID, descripcion, out mensaje);
        }
        public bool ListarBalde(int parametro, out List<EntidadBalde> lstBalde)
        {
            DataTable objDtt;
            var bRsl = _productosDatos.ListarBalde(parametro, out objDtt);
            if (!bRsl)
            {
                lstBalde = null;
                return bRsl;
            };
            lstBalde = new List<EntidadBalde>();
            EntidadBalde objB = null;
            foreach (DataRow row in objDtt.Rows)
            {
                objB = new EntidadBalde()
                {
                    codigo_balde = row["codigo_UM"].ToString(),
                    balde = row["descripcion"].ToString()
                };
                lstBalde.Add(objB);
            }
            return bRsl;
        }
        public bool ListarEnvase(int parametro, out List<EntidadEnvase> lstEnvase)
        {
            DataTable objDtt;
            var bRsl = _productosDatos.ListarEnvase(parametro, out objDtt);
            if (!bRsl)
            {
                lstEnvase = null;
                return bRsl;
            };
            lstEnvase = new List<EntidadEnvase>();
            EntidadEnvase objE = null;
            foreach (DataRow row in objDtt.Rows)
            {
                objE = new EntidadEnvase()
                {
                    codigo_envase = row["codigo_UM"].ToString(),
                    envase = row["descripcion"].ToString()
                };
                lstEnvase.Add(objE);
            }
            return bRsl;
        }
        public bool ListarPaquete(int parametro, out List<EntidadPaquete> lstPaquete)
        {
            DataTable objDtt;
            var bRsl = _productosDatos.ListarPaquete(parametro, out objDtt);
            if (!bRsl)
            {
                lstPaquete = null;
                return bRsl;
            };
            lstPaquete = new List<EntidadPaquete>();
            EntidadPaquete objP = null;
            foreach (DataRow row in objDtt.Rows)
            {
                objP = new EntidadPaquete()
                {
                    codigo_paquete = row["codigo_UM"].ToString(),
                    paquete = row["descripcion"].ToString()
                };
                lstPaquete.Add(objP);
            }
            return bRsl;
        }
        public bool ListarCilindro(int parametro, out List<EntidadCilindro> lstCilindro)
        {
            DataTable objDtt;
            var bRsl = _productosDatos.ListarCilindro(parametro, out objDtt);
            if (!bRsl)
            {
                lstCilindro = null;
                return bRsl;
            };
            lstCilindro = new List<EntidadCilindro>();
            EntidadCilindro objC = null;
            foreach (DataRow row in objDtt.Rows)
            {
                objC = new EntidadCilindro()
                {
                    codigo_cilindro = row["codigo_UM"].ToString(),
                    cilindro = row["descripcion"].ToString()
                };
                lstCilindro.Add(objC);
            }
            return bRsl;
        }
    }
}
