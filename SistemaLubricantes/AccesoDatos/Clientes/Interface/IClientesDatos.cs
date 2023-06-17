using System.Data;
using Entidades.Clientes;

namespace AccesoDatos.Clientes.Interface
{
    interface IClientesDatos
    {
        bool ListarClientes(out DataTable objDtt);
        bool GuardarClientes(EntidadCliente objCliente, out string mensaje);
        bool BuscarClientes(string cod_cliente, string documento, string placa, out DataTable objDtt);
    }
}
