using Newtonsoft.Json;
using System.Collections.Generic;

namespace Apis.Controllers.Clientes.Modelos.Respuesta
{
    public class ClientesRespuesta
    {
        [JsonProperty("clientes")]
        public List<ClientesListaRespuesta> Clientes { get; set; }
    }
    public class ClientesListaRespuesta
    {
        [JsonProperty("cod_cliente")]
        public string cod_cliente { get; set; }

        [JsonProperty("nombres")]
        public string nombres { get; set; }

        [JsonProperty("apellidos")]
        public string apellidos { get; set; }

        [JsonProperty("correo")]
        public string correo { get; set; }

        [JsonProperty("direccion")]
        public string direccion { get; set; }

        [JsonProperty("distrito")]
        public string distrito { get; set; }

        [JsonProperty("telefono")]
        public string telefono { get; set; }

        [JsonProperty("celular")]
        public string celular { get; set; }

        [JsonProperty("dni")]
        public string dni { get; set; }
    }
}
