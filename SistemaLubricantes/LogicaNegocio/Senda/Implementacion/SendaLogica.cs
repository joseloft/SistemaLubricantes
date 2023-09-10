using AccesoDatos.Home.Implementacion;
using AccesoDatos.Senda.Implementacion;
using Entidades.Home;
using Entidades.Senda;
using LogicaNegocio.Senda.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Senda.Implementacion
{
    public class SendaLogica : ISendaLogica
    {
        private SendaDatos _sendaDatos;
        private readonly IConfiguration _configuration;
        public SendaLogica(IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _sendaDatos = new SendaDatos(_configuration);
        }
        public SendaLogica(string _DbConexion)
        {
            _sendaDatos = new SendaDatos(_DbConexion);
        }
        public bool EmitirComprobante(out EntidadSenda objSenda, string nro_venta)
        {
            var bRsl = false;
            objSenda = null;
            DataSet objDts;
            if (!_sendaDatos.EmitirComprobante(out objDts, nro_venta))
            {
                objSenda = null;
                return bRsl;
            }

            objSenda = new EntidadSenda();                       
            
            objSenda.items = new List<EntidadSendaDetalle>();
            DataTable dttDetalle = objDts.Tables[1];
            foreach (DataRow objDtr in dttDetalle.Rows)
            {
                objSenda.items.Add(new EntidadSendaDetalle()
                {
                    tipodocu = objDtr["tipodocu"].ToString(),
                    codigo = objDtr["codigo"].ToString(),
                    codigo_sunat = objDtr["codigo_sunat"].ToString(),
                    codigo_gs1 = objDtr["codigo_gs1"].ToString(),
                    descripcion = objDtr["descripcion"].ToString(),
                    cantidad = objDtr["cantidad"].ToString(),
                    unid = objDtr["unid"].ToString(),
                    tipoprecioventa = objDtr["tipoprecioventa"].ToString(),
                    tipo_afect_igv = objDtr["tipo_afect_igv"].ToString(),
                    codigo_tributo = objDtr["codigo_tributo"].ToString(),
                    is_anticipo = int.Parse(objDtr["is_anticipo"].ToString()),
                    valorunitbruto = objDtr["valorunitbruto"].ToString(),
                    valorunit = objDtr["valorunit"].ToString(),
                    valorventabruto = objDtr["valorventabruto"].ToString(),
                    valorventa = objDtr["valorventa"].ToString(),
                    preciounitbruto = objDtr["preciounitbruto"].ToString(),
                    preciounit = objDtr["preciounit"].ToString(),
                    precioventa = objDtr["precioventa"].ToString(),
                    precioventabruto = objDtr["precioventabruto"].ToString(),
                    igv = objDtr["igv"].ToString(),
                    porc_igv = objDtr["porc_igv"].ToString(),
                    isc = objDtr["isc"].ToString(),
                    porc_isc = objDtr["porc_isc"].ToString(),
                    dscto_unit = objDtr["dscto_unit"].ToString(),
                    porc_dscto_unit = objDtr["porc_dscto_unit"].ToString(),
                    cod_cargodesc = objDtr["cod_cargodesc"].ToString(),
                    base_cargodesc = objDtr["base_cargodesc"].ToString(),
                    otrostributos_porc = objDtr["otrostributos_porc"].ToString(),
                    otrostributos_monto = objDtr["otrostributos_monto"].ToString(),
                    otrostributos_base = objDtr["otrostributos_base"].ToString(),
                    placavehiculo = objDtr["placavehiculo"].ToString(),
                    tot_impuesto = objDtr["tot_impuesto"].ToString(),
                    tipo_operacion = objDtr["tipo_operacion"].ToString(),
                    opt_tipodoi = objDtr["opt_tipodoi"].ToString(),
                    opt_numerodoi = objDtr["opt_numerodoi"].ToString(),
                    opt_pasaportepais = objDtr["opt_pasaportepais"].ToString(),
                    opt_huesped = objDtr["opt_huesped"].ToString(),
                    opt_huespedpais = objDtr["opt_huespedpais"].ToString(),
                    opt_fingresopais = objDtr["opt_fingresopais"].ToString(),
                    opt_fcheckin = objDtr["opt_fcheckin"].ToString(),
                    opt_fcheckout = objDtr["opt_fcheckout"].ToString(),
                    opt_fconsumo = objDtr["opt_fconsumo"].ToString(),
                    opt_diaspermanencia = objDtr["opt_diaspermanencia"].ToString()
                });
            }

            DataRow dttCabecera = objDts.Tables[0].Rows[0];
            objSenda.cabecera = new EntidadSendaCabecera()
            {
                ruc_emisor = dttCabecera["ruc_emisor"].ToString(),
                razonsocial_emisor = dttCabecera["razonsocial_emisor"].ToString(),
                direccion_emisor = dttCabecera["direccion_emisor"].ToString(),
                telefono_emisor = dttCabecera["telefono_emisor"].ToString(),
                email_emisor = dttCabecera["email_emisor"].ToString(),
                cod_domifiscal = dttCabecera["cod_domifiscal"].ToString(),
                tipo_codi = dttCabecera["tipo_codi"].ToString(),
                fecha = dttCabecera["Fecha"].ToString(),
                fvenc = dttCabecera["Fvenc"].ToString(),
                tipodocu = dttCabecera["tipodocu"].ToString(),
                nro_serie_efact = dttCabecera["nro_serie_efact"].ToString(),
                tipo_moneda = dttCabecera["tipo_moneda"].ToString(),
                numero = dttCabecera["numero"].ToString(),
                tipodocurefe = dttCabecera["tipodocurefe"].ToString(),
                numerorefe = dttCabecera["numerorefe"].ToString(),
                motivo_07_08 = dttCabecera["motivo_07_08"].ToString(),
                descripcion_07_08 = dttCabecera["descripcion_07_08"].ToString(),
                fecharefe = dttCabecera["fecharefe"].ToString(),
                tipodoi = int.Parse(dttCabecera["tipodoi"].ToString()),
                numerodoi = dttCabecera["numerodoi"].ToString(),
                desc_tipodocu = dttCabecera["desc_tipodocu"].ToString(),
                razonsocial = dttCabecera["razonsocial"].ToString(),
                direccion = dttCabecera["direccion"].ToString(),
                cliente = dttCabecera["cliente"].ToString(),
                email_cliente = dttCabecera["email_cliente"].ToString(),
                email_cc = dttCabecera["email_cc"].ToString(),
                codigo_cliente = int.Parse(dttCabecera["codigo_cliente"].ToString()),
                rec_tele = dttCabecera["rec_tele"].ToString(),
                rec_ubigeo = dttCabecera["rec_ubigeo"].ToString(),
                rec_pais = dttCabecera["rec_pais"].ToString(),
                rec_depa = dttCabecera["rec_depa"].ToString(),
                rec_provi = dttCabecera["rec_provi"].ToString(),
                rec_distri = dttCabecera["rec_distri"].ToString(),
                rec_urb = dttCabecera["rec_urb"].ToString(),
                vendedor = dttCabecera["vendedor"].ToString(),
                metodo_pago = dttCabecera["metodo_pago"].ToString(),
                codigo_metodopago = dttCabecera["codigo_metodopago"].ToString(),
                desc_metodopago = dttCabecera["desc_metodopago"].ToString(),
                totalpagado_efectivo = dttCabecera["totalpagado_efectivo"].ToString(),
                vuelto = dttCabecera["vuelto"].ToString(),
                file_nro = dttCabecera["file_nro"].ToString(),
                centro_costo = dttCabecera["centro_costo"].ToString(),
                nro_pedido = dttCabecera["nro_pedido"].ToString(),
                local = dttCabecera["local"].ToString(),
                caja = dttCabecera["caja"].ToString(),
                cajero = dttCabecera["cajero"].ToString(),
                nro_transaccion = dttCabecera["nro_transaccion"].ToString(),
                orden_compra = dttCabecera["orden_compra"].ToString(),
                glosa = dttCabecera["glosa"].ToString(),
                glosa_refe = dttCabecera["glosa_refe"].ToString(),
                glosa_pie_pagina = dttCabecera["glosa_pie_pagina"].ToString(),
                mensaje = dttCabecera["mensaje"].ToString(),
                numero_gr = dttCabecera["numero_gr"].ToString(),
                ant_numero = dttCabecera["ant_numero"].ToString(),
                docurela_numero = dttCabecera["docurela_numero"].ToString(),
                ant_monto = dttCabecera["ant_monto"].ToString(),
                op_exportacion = dttCabecera["op_exportacion"].ToString(),
                op_exonerada = decimal.Parse(dttCabecera["op_exonerada"].ToString()),
                op_inafecta = decimal.Parse(dttCabecera["op_inafecta"].ToString()),
                op_gravada = dttCabecera["op_gravada"].ToString(),
                tot_valorventa = decimal.Parse(dttCabecera["tot_valorventa"].ToString()),
                tot_precioventa = dttCabecera["tot_precioventa"].ToString(),
                isc = dttCabecera["isc"].ToString(),
                igv = dttCabecera["igv"].ToString(),
                porc_igv = dttCabecera["porc_igv"].ToString(),
                igv_gratuita = dttCabecera["igv_gratuita"].ToString(),
                importe_total = dttCabecera["importe_total"].ToString(),
                total_pagar = dttCabecera["total_pagar"].ToString(),
                redondeo = dttCabecera["redondeo"].ToString(),
                total_otros_tributos = dttCabecera["total_otros_tributos"].ToString(),
                total_otros_cargos = decimal.Parse(dttCabecera["total_otros_cargos"].ToString()),
                cargodesc_motivo = dttCabecera["cargodesc_motivo"].ToString(),
                cargodesc_base = dttCabecera["cargodesc_base"].ToString(),
                porc_dsctoglobal = dttCabecera["porc_dsctoglobal"].ToString(),
                total_descuento = decimal.Parse(dttCabecera["total_descuento"].ToString()),
                descto_global = dttCabecera["descto_global"].ToString(),
                total_gratuitas = decimal.Parse(dttCabecera["total_gratuitas"].ToString()),
                importe_letras = dttCabecera["importe_letras"].ToString(),
                total_icbper = dttCabecera["total_icbper"].ToString(),
                usuario = dttCabecera["usuario"].ToString(),
                tipocambio = dttCabecera["tipocambio"].ToString(),
                codigo_sucu = dttCabecera["codigo_sucu"].ToString(),
                detraccion_bs = dttCabecera["detraccion_bs"].ToString(),
                detraccion_nrocta = dttCabecera["detraccion_nrocta"].ToString(),
                detraccion_porc = dttCabecera["detraccion_porc"].ToString(),
                detraccion_monto = dttCabecera["detraccion_monto"].ToString(),
                detraccion_moneda = dttCabecera["detraccion_moneda"].ToString(),
                detraccion_mediopago = dttCabecera["detraccion_mediopago"].ToString(),
                almacen_id = int.Parse(dttCabecera["almacen_id"].ToString()),
                icoterms = dttCabecera["icoterms"].ToString(),
                glosa_detraccion = dttCabecera["glosa_detraccion"].ToString()
            };

            bRsl = true;
            return bRsl;

        }
    }
}
