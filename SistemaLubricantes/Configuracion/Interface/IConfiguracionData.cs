using Microsoft.Extensions.Configuration;

namespace Configuracion.Interface
{
    public interface IConfiguracionData
    {
        string GetConnectionString(string connectionName);

    }
}
