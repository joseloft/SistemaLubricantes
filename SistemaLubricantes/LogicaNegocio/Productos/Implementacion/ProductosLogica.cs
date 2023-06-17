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
        public bool ListarMarca(out List<EntidadMarca> lstMarca)
        {
            DataTable objDtt;
            var bRsl = _productosDatos.ListarMarca(out objDtt);
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
    }
}
