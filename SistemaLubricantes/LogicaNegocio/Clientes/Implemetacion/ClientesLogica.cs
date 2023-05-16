using AccesoDatos.Clientes.Implementacion;
using LogicaNegocio.Clientes.Interface;
using Microsoft.Extensions.Configuration;
using Entidades.Clientes;
using System.Collections.Generic;
using System.Data;

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
                    ruc = row["ruc"].ToString()
                };
                lstClientes.Add(objCli);
            }
            return bRsl;
        }
        public bool GuardarClientes(EntidadCliente entidadCliente)
        {
            return _clientesDatos.GuardarClientes(entidadCliente);
        }
    }
}
