﻿using AccesoDatos.Home.Implementacion;
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

            DataRow dttCabecera = objDts.Tables[0].Rows[0];
            objSenda = new EntidadSenda();
            objSenda.cabecera = new EntidadSendaCabecera()
            {
                ruc_emisor = dttCabecera["ruc_emisor"].ToString(),
                razonsocial_emisor = dttCabecera["razonsocial_emisor"].ToString(),
                dirección_emisor = dttCabecera["dirección_emisor"].ToString(),
                telefono_emisor = dttCabecera["telefono_emisor"].ToString(),
                email_emisor = dttCabecera["email_emisor"].ToString(),
                cod_domifiscal = dttCabecera["cod_domifiscal"].ToString(),
                tipo_codi = dttCabecera["tipo_codi"].ToString(),
                Fecha = dttCabecera["Fecha"].ToString(),
                Fvenc = dttCabecera["Fvenc"].ToString(),
                tipodocu = dttCabecera["tipodocu"].ToString(),
                nro_serie_efact = dttCabecera["nro_serie_efact"].ToString(),
                tipo_moneda = dttCabecera["tipo_moneda"].ToString(),
                numero = dttCabecera["numero"].ToString(),
                tipodocurefe = dttCabecera["tipodocurefe"].ToString(),
                numerorefe = dttCabecera["numerorefe"].ToString(),
                motivo_07_08 = dttCabecera["motivo_07_08"].ToString(),
                descripcion_07_08 = dttCabecera["descripcion_07_08"].ToString(),
                fecharefe = dttCabecera["fecharefe"].ToString(),
                tipodoi = dttCabecera["tipodoi"].ToString(),
                numerodoi = dttCabecera["numerodoi"].ToString(),
                desc_tipodocu = dttCabecera["desc_tipodocu"].ToString(),
                razonsocial = dttCabecera["razonsocial"].ToString(),
                direccion = dttCabecera["direccion"].ToString(),
                cliente = dttCabecera["cliente"].ToString(),
                email_cliente = dttCabecera["email_cliente"].ToString(),
                email_cc = dttCabecera["email_cc"].ToString(),
                codigo_cliente = dttCabecera["codigo_cliente"].ToString(),
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
                totalpagado_efectivo = decimal.Parse(dttCabecera["totalpagado_efectivo"].ToString()),
                vuelto = decimal.Parse(dttCabecera["vuelto"].ToString()),
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
                op_exportacion = decimal.Parse(dttCabecera["op_exportacion"].ToString()),
                op_exonerada = decimal.Parse(dttCabecera["op_exonerada"].ToString()),
                op_inafecta = decimal.Parse(dttCabecera["op_inafecta"].ToString()),
                op_gravada = decimal.Parse(dttCabecera["op_gravada"].ToString()),
                tot_valorventa = decimal.Parse(dttCabecera["tot_valorventa"].ToString()),
                tot_precioventa = decimal.Parse(dttCabecera["tot_precioventa"].ToString()),
                isc = decimal.Parse(dttCabecera["isc"].ToString()),
                igv = decimal.Parse(dttCabecera["igv"].ToString()),
                porc_igv = decimal.Parse(dttCabecera["porc_igv"].ToString()),
                igv_gratuita = decimal.Parse(dttCabecera["igv_gratuita"].ToString()),
                importe_total = decimal.Parse(dttCabecera["importe_total"].ToString()),
                total_pagar = decimal.Parse(dttCabecera["total_pagar"].ToString()),
                redondeo = decimal.Parse(dttCabecera["redondeo"].ToString()),
                total_otros_tributos = decimal.Parse(dttCabecera["total_otros_tributos"].ToString()),
                total_otros_cargos = decimal.Parse(dttCabecera["total_otros_cargos"].ToString()),
                cargodesc_motivo = dttCabecera["cargodesc_motivo"].ToString(),
                cargodesc_base = decimal.Parse(dttCabecera["cargodesc_base"].ToString()),
                porc_dsctoglobal = decimal.Parse(dttCabecera["porc_dsctoglobal"].ToString()),
                total_descuento = decimal.Parse(dttCabecera["total_descuento"].ToString()),
                descto_global = decimal.Parse(dttCabecera["descto_global"].ToString()),
                total_gratuitas = decimal.Parse(dttCabecera["total_gratuitas"].ToString()),
                importe_letras = dttCabecera["importe_letras"].ToString(),
                total_icbper = decimal.Parse(dttCabecera["total_icbper"].ToString()),
                usuario = dttCabecera["usuario"].ToString(),
                tipocambio = decimal.Parse(dttCabecera["tipocambio"].ToString()),
                codigo_sucu = dttCabecera["codigo_sucu"].ToString(),
                detraccion_bs = dttCabecera["detraccion_bs"].ToString(),
                detraccion_nrocta = dttCabecera["detraccion_nrocta"].ToString(),
                detraccion_porc = dttCabecera["detraccion_porc"].ToString(),
                detraccion_monto = decimal.Parse(dttCabecera["detraccion_monto"].ToString()),
                detraccion_moneda = dttCabecera["detraccion_moneda"].ToString(),
                detraccion_mediopago = dttCabecera["detraccion_mediopago"].ToString(),
                almacen_id = int.Parse(dttCabecera["almacen_id"].ToString()),
                icoterms = dttCabecera["icoterms"].ToString(),
                glosa_detraccion = dttCabecera["glosa_detraccion"].ToString()
            };
            
            objSenda.items = new List<EntidadSendaDetalle>();
            //EntidadSendaDetalle items;
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
                    cantidad = decimal.Parse(objDtr["cantidad"].ToString()),
                    unid = objDtr["unid"].ToString(),
                    tipoprecioventa = objDtr["tipoprecioventa"].ToString(),
                    tipo_afect_igv = objDtr["tipo_afect_igv"].ToString(),
                    codigo_tributo = objDtr["codigo_tributo"].ToString(),
                    is_anticipo = int.Parse(objDtr["is_anticipo"].ToString()),
                    valorunitbruto = decimal.Parse(objDtr["valorunitbruto"].ToString()),
                    valorunit = decimal.Parse(objDtr["valorunit"].ToString()),
                    valorventabruto = decimal.Parse(objDtr["valorventabruto"].ToString()),
                    valorventa = decimal.Parse(objDtr["valorventa"].ToString()),
                    preciounitbruto = decimal.Parse(objDtr["preciounitbruto"].ToString()),
                    preciounit = decimal.Parse(objDtr["preciounit"].ToString()),
                    precioventa = decimal.Parse(objDtr["precioventa"].ToString()),
                    precioventabruto = decimal.Parse(objDtr["precioventabruto"].ToString()),
                    igv = decimal.Parse(objDtr["igv"].ToString()),
                    porc_igv = decimal.Parse(objDtr["porc_igv"].ToString()),
                    isc = decimal.Parse(objDtr["isc"].ToString()),
                    porc_isc = decimal.Parse(objDtr["porc_isc"].ToString()),
                    dscto_unit = decimal.Parse(objDtr["dscto_unit"].ToString()),
                    porc_dscto_unit = decimal.Parse(objDtr["porc_dscto_unit"].ToString()),
                    cod_cargodesc = objDtr["cod_cargodesc"].ToString(),
                    base_cargodesc = objDtr["base_cargodesc"].ToString(),
                    otrostributos_porc = decimal.Parse(objDtr["otrostributos_porc"].ToString()),
                    otrostributos_monto = decimal.Parse(objDtr["otrostributos_monto"].ToString()),
                    otrostributos_base = decimal.Parse(objDtr["otrostributos_base"].ToString()),
                    placavehiculo = objDtr["placavehiculo"].ToString(),
                    tot_impuesto = decimal.Parse(objDtr["tot_impuesto"].ToString()),
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

            bRsl = true;
            return bRsl;

        }
    }
}