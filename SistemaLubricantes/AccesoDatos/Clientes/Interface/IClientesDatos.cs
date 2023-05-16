using System.Data;
using Entidades.Clientes;

namespace AccesoDatos.Clientes.Interface
{
    interface IClientesDatos
    {
        bool ListarClientes(out DataTable objDtt);
        bool GuardarClientes(EntidadCliente entidadCliente);
    }
}
