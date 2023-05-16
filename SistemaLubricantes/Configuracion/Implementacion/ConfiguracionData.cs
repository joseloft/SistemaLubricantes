using Configuracion.Interface;
using Microsoft.Extensions.Configuration;

namespace Configuracion.Implementacion
{
    public class ConfiguracionData: IConfiguracionData
    {
        private readonly IConfiguration _configuration;
        public ConfiguracionData(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string GetConnectionString(string connectionName)
        {
            return this._configuration.GetConnectionString(connectionName);
        }
    }
}
