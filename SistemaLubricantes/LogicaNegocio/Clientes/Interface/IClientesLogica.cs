using System;
using System.Collections.Generic;
using System.Data;
using Entidades.Clientes;

namespace LogicaNegocio.Clientes.Interface
{
    interface IClientesLogica
    {
        bool ListarClientes(out List<EntidadCliente> lstClientes);
        bool GuardarClientes(EntidadCliente objCliente, out string mensaje);
        bool BuscarClientes(string cod_cliente, string documento, string placa, out EntidadFiltroCliente objFiltro);
    }
}
