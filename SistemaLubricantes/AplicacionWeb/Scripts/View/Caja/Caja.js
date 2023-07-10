var dataVentas = [];
var dataVentasP = [];
var dataComprobante = [];
var dataTipoPago = [];
$(document).ready(function () {

    listarVentasPendientes();
    listarTipoComprobante();
    listarTipoPago();

    $('#selectComprobante').prop('disabled', 'disabled');
    $('#selectPago').prop('disabled', 'disabled');
    document.getElementById('lblcredito').style.display = 'none';
    document.getElementById('diasCredito').style.display = 'none';
    document.getElementById('diasNoCredito').style.display = 'block';
    $(document).on("change", "#selectNroVenta", function () {
        mostrarVenta(this.value);
        $('#selectComprobante').removeAttr('disabled');
        $('#selectPago').removeAttr('disabled');
    });
    $(document).on("change", "#selectPago", function () {
        if (this.value == '02') {
            document.getElementById('lblcredito').style.display = 'block';
            document.getElementById('diasCredito').style.display = 'block';
            document.getElementById('diasNoCredito').style.display = 'none';
        } else {
            document.getElementById('lblcredito').style.display = 'none';
            document.getElementById('diasCredito').style.display = 'none';
            document.getElementById('diasNoCredito').style.display = 'block';
        }
    });
    $(document).on("click", "#btnEmitir", function () {
        guardarComprobante();
    });
});

function listarVentasPendientes() {
    loadCombo([], 'selectNroVenta', true);

    $.ajax({
        type: "GET",
        url: "https://localhost:44380/api/ventas/ListarVentasPendientes",
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            dataVentas = data.value;
            if (dataVentas.length > 0) {
                dataVentasP = dataVentas.map(
                    obj => {
                        return {
                            "id": obj.codigo_venta,
                            "name": obj.codigo_venta + '-' + obj.cliente
                        }
                    }
                );

                $("table#tblLista").DataTable().destroy();
                $("table#tblLista>tbody").empty();

                var tDetalle = '';
                $.each(dataVentas, function (i, e) {
                    tDetalle += '<tr>';
                    tDetalle += '<td class="nro_venta" >' + e.codigo_venta + '</td >';
                    tDetalle += '<td class="documento">' + e.documento + '</td >';
                    tDetalle += '<td class="cliente">' + e.cliente + '</td >';
                    tDetalle += '<td class="fecha_venta">' + e.fecha_venta + '</td >';
                    tDetalle += '<td class="forma_pago">' + e.forma_pago + '</td >';
                    tDetalle += '<td class="moneda">' + e.moneda + '</td >';
                    tDetalle += '<td class="total">' + e.total + '</td >';
                    tDetalle += '<td class="condicion">' + e.estado + '</td >';
                    tDetalle += '<td style="text-align:center">';
                    tDetalle += '<button type="button" style="font-size:10px" class="btn btn-sm btn-success" onclick="verVenta(this)" title="Ver Venta"><i class="fas fa-paper-plane fa-sm"></i></button>';
                    tDetalle += '<button type="button" style="font-size:10px" class="btn btn-sm btn-danger" onclick="anularVenta(this)" title="Anular Venta"><i class="fas fa-ban fa-sm"></i></button>';
                    tDetalle += '</td>';
                    tDetalle += '</tr>';
                })

                $("table#tblLista>tbody").append(tDetalle);

            } else {
                $("table#tblLista").DataTable().destroy();
                $("table#tblLista>tbody").empty();
                dataVentasP = dataVentas;
            }

            loadCombo(dataVentasP, 'selectNroVenta', true, 'Seleccione la venta');
            $('selectNroVenta').selectpicker('refresh');
        }
    });    
            
}
function listarTipoComprobante() {
    loadCombo([], 'selectComprobante', false);
    $.ajax({
        type: "GET",
        url: "https://localhost:44380/api/caja/ListarTipoComprobante",
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            let comprobantes = data.value;
            if (comprobantes.length > 0) {
                dataComprobante = comprobantes.map(
                    obj => {
                        return {
                            "id": obj.comprobante_id,
                            "name": obj.comprobante
                        }
                    }
                );
            }
            loadCombo(dataComprobante, 'selectComprobante', false);
            $('selectComprobante').selectpicker('refresh');
        }
    });
}
function listarTipoPago(){
    loadCombo([], 'selectPago', false);
    $.ajax({
        type: "GET",
        url: "https://localhost:44380/api/caja/ListarTipoPago",
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            let pagos = data.value;
            if (pagos.length > 0) {
                dataTipoPago = pagos.map(
                    obj => {
                        return {
                            "id": obj.codigo_tipo_pago,
                            "name": obj.tipo_pago
                        }
                    }
                );
            }
            loadCombo(dataTipoPago, 'selectPago', false);
            $('selectPago').selectpicker('refresh');
        }
    });
}
function loadCombo(data, control, firtElement, textFirstElement) {
    var content = "";
    if (firtElement == true) {
        content += "<option value=''>" + textFirstElement + "</option>";
    }
    for (var i = 0; i < data.length; i++) {
        content += "<option value='" + data[i].id + "'>";
        content += data[i].name;
        content += "</option>";
    }
    $('#' + control).empty().append(content);
}

function mostrarVenta(codigo_venta) {
    let dataVentasS = dataVentas.filter(item => item.codigo_venta == codigo_venta);

    $(txtCliente).val(dataVentasS[0].cliente);
    $(txtTipoDocumento).val(dataVentasS[0].tipo_documento);
    $(txtDocumento).val(dataVentasS[0].documento);
    $(txtTotal).val('S/. ' + dataVentasS[0].total);

    listarDetalleVenta(codigo_venta);
}
function listarDetalleVenta(codigo_venta) {
    $("table#tblDetalle").DataTable().destroy();

    $.ajax({
        type: "GET",
        url: "https://localhost:44380/api/ventas/ListarDetalleVentaPendientes?codigo_venta=" + codigo_venta,
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            dataProductos = data.value;
            $("table#tblDetalle>tbody").empty();
            var content = '';
            $.each(dataProductos, function (i, e) {
                content += '<tr>';
                content += '<td class="item">' + e.nro_detalle + '</td >';
                content += '<td class="producto">' + e.producto + '</td >';
                content += '<td class="marca">' + e.marca + '</td >';
                content += '<td class="cantidad">' + e.cantidad + '</td >';
                content += '<td class="precio">' + e.precio + '</td >';
                content += '<td class="total">' + e.total + '</td >';
                content += '</tr>';
            })
            $("table#tblDetalle>tbody").append(content);
            
        }
    });
}
function guardarComprobante() {
    if (!obligatorioComprobante()) {
        return
    }

    let codigo_venta = $("#selectNroVenta").val();
    let tipo_comprobante = $("#selectComprobante").val();
    let tipo_documento = $("#txtTipoDocumento").val();
    let codigo_usuario = 'U0000001';
    let tipo_pago = $("#selectPago").val();

    var objComprobante = {
        "codigo_venta": codigo_venta,
        "tipo_comprobante": tipo_comprobante,
        "tipo_documento": tipo_documento,
        "codigo_usuario": codigo_usuario,
        "tipo_pago": tipo_pago
    }

    $.ajax({
        type: 'POST',
        url: "https://localhost:44380/api/caja/GuardarComprobante",
        async: false,
        headers: {
            'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0'
        },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(objComprobante),
        success: function (data) {
            let bResult = false;
            if (data.hasOwnProperty('statusCode')) {
                switch (data.statusCode) {
                    case null: bResult = true; break;
                    case 9:
                        swal("Oops", data.message, "error"); break;
                    default: swal("Oops", "Ocurrió un problema!", "error"); break;
                }
            }
            if (!bResult) return false;
            swal("Buen trabajo!", data.value, "success");
        },
        error: function (datoError) {
            swal("Oops!", "No se pudo guardar el cambio.", "error");
        },
        complete: function () {
            limpiarComprobante();
        }
    })
}
function obligatorioComprobante() {
    if ($("#selectNroVenta").val() == '00') {
        swal('Completar Datos!', 'Seleccione el numero de venta', "warning");
        return false;
    }
    if ($("#selectComprobante").val() == '00') {
        swal('Completar Datos', 'Seleccione el tipo de comprobante', "warning");
        return false;
    }
    if ($("#selectPago").val() == '00') {
        swal('Completar Datos', 'Seleccione el tipo de pago', "warning");
        return false;
    }
    if ($(txtTotal).val() == '') {
        swal('Completar Datos', 'No se puedo obtener el monto total', "warning");
        return false;
    }
    let table = $('#tblDetalle').DataTable();
    if (!table.data().count()) {
        swal('Completar Datos', 'No existe detalle de la venta. Verificar!', "warning");
        return false;
    }
    return true;
}
function limpiarComprobante() {
    $("#selectNroVenta").val('').selectpicker('refresh');
    listarVentasPendientes();
    $("#selectNroVenta").selectpicker('refresh');
    listarTipoComprobante();
    listarTipoPago();
    $(txtCliente).val('');
    $(txtTipoDocumento).val('');
    $(txtDocumento).val('');
    $(txtTotal).val('');
    $(txtCredito).val('');
    document.getElementById('lblcredito').style.display = 'none';
    document.getElementById('diasCredito').style.display = 'none';
    document.getElementById('diasNoCredito').style.display = 'block';
    $("table#tblDetalle").DataTable().destroy();
    $("table#tblDetalle>tbody").empty();
}