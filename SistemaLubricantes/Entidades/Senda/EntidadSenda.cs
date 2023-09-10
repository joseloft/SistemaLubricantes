using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Senda
{
    public class EntidadSenda
    {
        public List<EntidadSendaDetalle> items { get; set; }
        public EntidadSendaCabecera cabecera { get; set; }        
    }
    public class EntidadSendaCabecera
    {
        public string ruc_emisor { get; set; }
        public string razonsocial_emisor { get; set; }
        public string direccion_emisor { get; set; }
        public string telefono_emisor { get; set; }
        public string email_emisor { get; set; }
        public string cod_domifiscal { get; set; }
        public string tipo_codi { get; set; }
        public string fecha { get; set; }
        public string fvenc { get; set; }
        public string tipodocu { get; set; }
        public string nro_serie_efact { get; set; }
        public string tipo_moneda { get; set; }
        public string numero { get; set; }
        public string tipodocurefe { get; set;}
        public string numerorefe { get;set; }
        public string motivo_07_08 { get;set; }
        public string descripcion_07_08 { get; set; }
        public string fecharefe { get; set; }
        public int tipodoi { get; set; }
        public string numerodoi { get; set; }
        public string desc_tipodocu { get; set; }
        public string razonsocial { get; set; }
        public string direccion { get; set; }
        public string cliente { get; set; }
        public string email_cliente { get; set; }
        public string email_cc { get; set; }
        public int codigo_cliente { get; set;}
        public string rec_tele { get; set; }
        public string rec_ubigeo { get; set;}
        public string rec_pais { get; set;}
        public string rec_depa { get; set; }
        public string rec_provi { get; set; }
        public string rec_distri { get; set; }
        public string rec_urb { get; set; }
        public string vendedor { get; set;}
        public string metodo_pago { get; set;}
        public string codigo_metodopago { get;set;}
        public string desc_metodopago { get; set;}
        public string totalpagado_efectivo { get; set; }
        public string vuelto { get; set; }
        public string file_nro { get; set; }
        public string centro_costo { get; set;}
        public string nro_pedido { get; set;}
        public string local { get; set;}
        public string caja { get; set;}
        public string cajero { get; set;}
        public string nro_transaccion { get; set;}
        public string orden_compra { get; set;}
        public string glosa { get; set;}
        public string glosa_refe { get; set;}
        public string glosa_pie_pagina { get; set;}
        public string mensaje { get;set;}
        public string numero_gr { get;set;}
        public string ant_numero { get; set;}
        public string docurela_numero { get; set; }
        public string ant_monto { get; set;}
        public string op_exportacion { get; set;}
        public decimal op_exonerada { get; set;}
        public decimal op_inafecta { get; set;}
        public string op_gravada { get; set;}
        public decimal tot_valorventa { get; set;}
        public string tot_precioventa { get;set;}
        public string isc { get; set;}
        public string igv { get; set; }
        public string porc_igv { get; set;}
        public string igv_gratuita { get; set; }
        public string importe_total { get; set; }
        public string total_pagar { get; set; }
        public string redondeo { get; set; }
        public string total_otros_tributos { get; set; }
        public decimal total_otros_cargos { get; set; }
        public string cargodesc_motivo { get; set; }
        public string cargodesc_base { get; set; }
        public string porc_dsctoglobal { get; set; }
        public decimal total_descuento { get; set; }
        public string descto_global { get; set; }
        public decimal total_gratuitas { get;set; }
        public string importe_letras { get; set; }
        public string total_icbper { get; set; }
        public string usuario { get; set; }
        public string tipocambio { get;set; }
        public string codigo_sucu { get; set; }
        public string detraccion_bs { get;set; }
        public string detraccion_nrocta { get; set; }
        public string detraccion_porc { get; set; }
        public string detraccion_monto { get; set; }
        public string detraccion_moneda { get; set; }
        public string detraccion_mediopago { get; set; }
        public int almacen_id { get; set; }
        public string icoterms { get; set;}
        public string glosa_detraccion { get; set; }
    }
    public class EntidadSendaDetalle
    {
        public string tipodocu { get; set; }
        public string codigo { get; set; }
        public string codigo_sunat { get; set; }
        public string codigo_gs1 { get; set; }
        public string descripcion { get; set; }
        public string cantidad { get;set; }
        public string unid { get; set; }
        public string tipoprecioventa { get; set; }
        public string tipo_afect_igv { get; set; }
        public string codigo_tributo { get; set; }
        public int is_anticipo { get; set; }
        public string valorunitbruto { get; set; }
        public string valorunit { get;set; }
        public string valorventabruto { get; set;}
        public string valorventa { get; set; }
        public string preciounitbruto { get;set; }
        public string preciounit { get; set; }
        public string precioventa { get; set; }
        public string precioventabruto { get; set; }
        public string igv { get; set; }
        public string porc_igv { get; set; }
        public string isc { get; set; }
        public string porc_isc { get; set; }
        public string dscto_unit { get; set; }
        public string porc_dscto_unit { get; set;}
        public string cod_cargodesc { get; set; }
        public string base_cargodesc { get;set; }
        public string otrostributos_porc { get; set; }
        public string otrostributos_monto { get; set; }
        public string otrostributos_base { get; set; }
        public string placavehiculo { get; set; }
        public string tot_impuesto { get; set; }
        public string tipo_operacion { get; set; }
        public string opt_tipodoi { get; set; }
        public string opt_numerodoi { get; set; }
        public string opt_pasaportepais { get; set; }
        public string opt_huesped { get; set; }
        public string opt_huespedpais { get; set; }
        public string opt_fingresopais { get; set; }
        public string opt_fcheckin { get; set; }
        public string opt_fcheckout { get; set; }
        public string opt_fconsumo { get; set; }
        public string opt_diaspermanencia { get;set; }

    }
}
