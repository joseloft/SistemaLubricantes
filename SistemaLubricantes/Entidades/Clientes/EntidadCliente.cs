
namespace Entidades.Clientes
{
    public class EntidadCliente
    {
        public void ReplaceNull()
        {
            id = id ?? 0;
            razon_social = razon_social ?? "";
            distrito = distrito ?? "";
            cod_cliente = cod_cliente ?? "";
            telefono = telefono ?? "";            
            apellidos = apellidos ?? "";
            direccion = direccion ?? "";
            celular = celular ?? "";
            tipo = tipo ?? "";
            dni = dni ?? "";
            ruc = ruc ?? "";
            retenedor = retenedor ?? "";

        }
        public int? id { get; set; }
        public string cod_cliente { get; set; }
        public string razon_social { get; set; }
        public string distrito { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public string celular { get; set; }
        public string tipo { get; set; }
        public string dni { get; set; }
        public string ruc { get; set; }
        public string retenedor { get; set; }
    }
}
