var dataEfectivo = [];
var dataTarjeta = [];
var dataYape = [];
var dataPlin = [];
var dataCredito = [];
var dataContado = [];
var dataTotalVentas = [];
var dataCantidadVentas = [];
var dataClientesNuevos = [];
var dataFacturacion = [];
var dataProductosStock = [];
var dataVentasComprobante = [];
var dataCreditosVencer = [];
var dataClientesCorreo = [];
$(document).ready(function () {

    $.ajax({
        type: "PUT",
        url: "https://localhost:44380/api/home/DashboardPagos",
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            
            if (data.value != null) {
                dataEfectivo = data.value.objEfectivo;
                $(efectivo).text(dataEfectivo.montoPago);

                dataTarjeta = data.value.objTarjeta;
                $(tarjeta).text(dataTarjeta.montoPago);

                dataYape = data.value.objYape;
                $(yape).text(dataYape.montoPago);

                dataPlin = data.value.objPlin;
                $(plin).text(dataPlin.montoPago);

                dataCredito = data.value.objCredito;
                $(credito).text(dataCredito.montoPago);

                dataContado = data.value.objContado;
                $(contado).text(dataContado.montoPago);
            }
        }
    });

    $.ajax({
        type: "PUT",
        url: "https://localhost:44380/api/home/DashboardIndicadores",
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {

            if (data.value != null) {
                dataTotalVentas = data.value.objTotalVentas;
                $(totalVentas).text(dataTotalVentas.montoPago);

                dataCantidadVentas = data.value.objCantidadVentas;
                $(cantidadVentas).text(dataCantidadVentas.cantidad);

                dataClientesNuevos = data.value.objClientesNuevos;
                $(clientesNuevos).text(dataClientesNuevos.cantidad);

                dataFacturacion = data.value.objFacturacion;
                $(facturacion).text(dataFacturacion.montoPago);

            }
        }
    });

    poblarNotificaciones();
});

function poblarNotificaciones() {
    $("table#tblProductos").DataTable().destroy();
    $("table#tblVentas").DataTable().destroy();
    $("table#tblCreditos").DataTable().destroy();
    $("table#tblClientes").DataTable().destroy();

    $.ajax({
        type: "PUT",
        url: "https://localhost:44380/api/home/DashboardNotificaciones",
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {

            if (data.value != null) {
                dataProductosStock = data.value.lstProductosPocoStock;
                $("table#tblProductos>tbody").empty();
                var pStock = '';
                $.each(dataProductosStock, function (i, e) {
                    pStock += '<tr>';
                    pStock += '<td class="item">' + e.item + '</td >';
                    pStock += '<td class="codigo">' + e.codigo_producto + '</td >';
                    pStock += '<td class="marca">' + e.marca + '</td >';
                    pStock += '<td class="producto">' + e.producto + '</td >';
                    pStock += '<td class="stock">' + e.stock + '</td >';
                    pStock += '<td class="modelo">' + e.categoria + '</td >';
                    pStock += '<td class="precio">' + e.precio_venta + '</td >';
                    pStock += '</tr>';
                })
                $("table#tblProductos>tbody").append(pStock);

                dataVentasComprobante = data.value.lstVentasSinComprobante;
                $("table#tblVentas>tbody").empty();
                var vRecibo = '';
                $.each(dataVentasComprobante, function (i, e) {
                    vRecibo += '<tr>';
                    vRecibo += '<td class="item">' + e.item + '</td >';
                    vRecibo += '<td class="codigo">' + e.codigo_venta + '</td >';
                    vRecibo += '<td class="cliente">' + e.cliente + '</td >';
                    vRecibo += '<td class="fecha">' + e.fecha_venta + '</td >';
                    vRecibo += '<td class="total">' + e.total + '</td >';
                    vRecibo += '<td class="estado">' + e.estado + '</td >';
                    vRecibo += '<td class="placa">' + e.placa + '</td >';
                    vRecibo += '</tr>';
                })
                $("table#tblVentas>tbody").append(vRecibo);

                dataCreditosVencer = data.value.lstCreditosPorVencer;
                $("table#tblCreditos>tbody").empty();
                var cVence = '';
                $.each(dataCreditosVencer, function (i, e) {
                    cVence += '<tr>';
                    cVence += '<td class="item">' + e.item + '</td >';
                    cVence += '<td class="codigo">' + e.codigo_venta + '</td >';
                    cVence += '<td class="cliente">' + e.cliente + '</td >';
                    cVence += '<td class="venta">' + e.monto_venta + '</td >';
                    cVence += '<td class="credito">' + e.monto_credito + '</td >';
                    cVence += '<td class="vencimiento">' + e.fecha_vencimiento + '</td >';
                    cVence += '<td class="dias">' + e.dias_credito + '</td >';
                    cVence += '</tr>';
                })
                $("table#tblCreditos>tbody").append(cVence);

                dataClientesCorreo = data.value.lstClientesSinCorreo;
                $("table#tblClientes>tbody").empty();
                var cCorreo = '';
                $.each(dataClientesCorreo, function (i, e) {
                    cCorreo += '<tr>';
                    cCorreo += '<td class="item">' + e.item + '</td >';
                    cCorreo += '<td class="codigo">' + e.codigo_cliente + '</td >';
                    cCorreo += '<td class="cliente">' + e.cliente + '</td >';
                    cCorreo += '<td class="fecha">' + e.fecha_registro + '</td >';
                    cCorreo += '<td class="tipo">' + e.tipo_cliente + '</td >';
                    cCorreo += '<td class="documento">' + e.documento + '</td >';
                    cCorreo += '</tr>';
                })
                $("table#tblClientes>tbody").append(cCorreo);

            }
        }
    });
}