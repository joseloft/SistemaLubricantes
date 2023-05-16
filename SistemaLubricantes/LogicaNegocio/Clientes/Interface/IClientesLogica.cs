using System;
using System.Collections.Generic;
using Entidades.Clientes;

namespace LogicaNegocio.Clientes.Interface
{
    interface IClientesLogica
    {
        bool ListarClientes(out List<EntidadCliente> lstClientes);
        bool GuardarClientes(EntidadCliente entidadCliente);
    }
}
