
namespace Entidades.Clientes
{
    public class EntidadCliente
    {
        public void ReplaceNull()
        {
            id = id ?? 0;
            distrito = distrito ?? "";
            cod_cliente = cod_cliente ?? "";
            telefono = telefono ?? "";            
            apellidos = apellidos ?? "";
            direccion = direccion ?? "";
            celular = celular ?? "";
            tipoCliente = tipoCliente ?? "";
            nroDocumento = nroDocumento ?? "";
            retenedor = retenedor ?? "";

        }
        public int? id { get; set; }
        public string cod_cliente { get; set; }
        public string distrito { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public string celular { get; set; }
        public string tipoCliente { get; set; }
        public string nroDocumento { get; set; }
        public string retenedor { get; set; }
    }
}
