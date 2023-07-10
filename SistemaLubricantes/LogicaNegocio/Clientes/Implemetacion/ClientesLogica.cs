using AccesoDatos.Clientes.Implementacion;
using LogicaNegocio.Clientes.Interface;
using Microsoft.Extensions.Configuration;
using Entidades.Clientes;
using System.Collections.Generic;
using System.Data;
using AccesoDatos.Home.Implementacion;
using Entidades.Home;
using System;

namespace LogicaNegocio.Clientes.Implemetacion
{
    public class ClientesLogica : IClientesLogica
    {
        private ClientesDatos _clientesDatos;
        private readonly IConfiguration _configuration;
        public ClientesLogica(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _clientesDatos = new ClientesDatos(_configuration);
        }
        public ClientesLogica(string _DbConexion)
        {
            _clientesDatos = new ClientesDatos(_DbConexion);
        }
        public bool ListarClientes(out List<EntidadCliente> lstClientes)
        {
            DataTable objDtt;
            var bRsl = _clientesDatos.ListarClientes(out objDtt);
            if (!bRsl)
            {
                lstClientes = null;
                return bRsl;
            };
            lstClientes = new List<EntidadCliente>();
            EntidadCliente objCli = null;
            foreach (DataRow row in objDtt.Rows)
            {
                objCli = new EntidadCliente()
                {
                    cod_cliente = row["cod_cliente"].ToString(),
                    nombres = row["nombres"].ToString(),
                    apellidos = row["apellidos"].ToString(),
                    direccion = row["direccion"].ToString(),
                    distrito = row["distrito"].ToString(),
                    celular = row["celular"].ToString(),
                    correo = row["correo"].ToString(),
                    dni = row["dni"].ToString(),
                    ruc = row["ruc"].ToString()
                };
                lstClientes.Add(objCli);
            }
            return bRsl;
        }
        public bool GuardarClientes(EntidadCliente objCliente, out string mensaje)
        {
            return _clientesDatos.GuardarClientes(objCliente, out mensaje);
        }
        public bool BuscarClientes(string cod_cliente, string documento, string placa, out EntidadFiltroCliente objFiltro)
        {
            DataTable objDtt = null;
            var bRsl = _clientesDatos.BuscarClientes(cod_cliente, documento, placa, out objDtt);
            if (!bRsl)
            {
                objFiltro = null;
                return bRsl;
            }
            objFiltro = new EntidadFiltroCliente();
            EntidadFiltroCliente objTC = null;
            foreach (DataRow dataRow in objDtt.Rows)
            {
                objTC = new EntidadFiltroCliente()
                {
                    cod_cliente = dataRow["cod_cliente"].ToString(),
                    nombre = dataRow["nombre"].ToString(),
                    documento = dataRow["documento"].ToString(),
                    placa = dataRow["placa"].ToString()
                };
            }

            objFiltro = objTC;
            return bRsl;
        }
    }
}
