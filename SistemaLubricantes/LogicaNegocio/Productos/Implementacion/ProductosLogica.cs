using LogicaNegocio.Productos.Interface;
using AccesoDatos.Productos.Implementacion;
using Microsoft.Extensions.Configuration;
using System.Data;
using Entidades.Productos;
using System.Collections.Generic;
using System;

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
        public bool ListarProductos(out List<EntidadProducto> lstProductos, int parametro, string producto)
        {
            DataTable objDtt;
            var bRsl = _productosDatos.ListarProductos(out objDtt, parametro, producto);
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
                    cod_producto = row["Cod_Prod"].ToString(),
                    nombre = row["Nombre"].ToString(),
                    marca = row["marca"].ToString(),
                    stock = (float)Convert.ToDecimal(row["Stock"]),
                    precio_venta = Convert.ToDecimal(row["Prec_Venta"]),
                    moneda = row["moneda"].ToString()
                };
                lstProductos.Add(objPro);
            }
            return bRsl;
        }
    }
}
