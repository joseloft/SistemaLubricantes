using Newtonsoft.Json;
using System.Collections.Generic;

namespace Apis.Controllers.Productos.Modelos.Respuesta
{
    public class ProductosRespuesta
    {
        [JsonProperty("productos")]
        public List<ProductosListaRespuesta> Productos { get; set; }
    }
    public class ProductosListaRespuesta
    {
        [JsonProperty("cod_producto")]
        public string cod_producto { get; set; }

        [JsonProperty("nombre")]
        public string nombre { get; set; }

        [JsonProperty("marca")]
        public string marca { get; set; }

        [JsonProperty("stock")]
        public float stock { get; set; }

        [JsonProperty("precio_venta")]
        public decimal precio_venta { get; set; }

        [JsonProperty("moneda")]
        public string moneda { get; set; }
    }
}
