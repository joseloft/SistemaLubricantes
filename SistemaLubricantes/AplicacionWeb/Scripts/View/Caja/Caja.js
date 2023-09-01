var dataVentas = [];
var dataVentasP = [];
var dataComprobante = [];
var dataTipoPago = [];
var _saldo = 0;
$(document).ready(function () {
    $.LoadingOverlay("show");
    
    listarVentasPendientes();
    listarTipoComprobante();
    listarTipoPago();

    $('#selectComprobante').prop('disabled', 'disabled');
    $('#selectPago').prop('disabled', 'disabled');
    document.getElementById('divCredito').style.display = 'none';
    document.getElementById('divDiasCredito').style.display = 'none';
    document.getElementById('divSaldo').style.display = 'none';
    document.getElementById('divFV').style.display = 'none';
    document.getElementById('divPago').style.display = 'none';
    document.getElementById('divNoCredito').style.display = 'block';
    document.getElementById('divNuevoSaldo').style.display = 'none';

    $.LoadingOverlay("hide");

    $(document).on("change", "#selectNroVenta", function () {
        mostrarVenta(this.value);
        $('#selectComprobante').removeAttr('disabled');
        $('#selectPago').removeAttr('disabled');
    });
    $(document).on("change", "#selectPago", function () {
        if (this.value != '05') {
            document.getElementById('divCredito').style.display = 'none';
            document.getElementById('divDiasCredito').style.display = 'none';
            document.getElementById('divSaldo').style.display = 'none';
            document.getElementById('divFV').style.display = 'none';
            document.getElementById('divPago').style.display = 'none';
            document.getElementById('divNoCredito').style.display = 'block';
            document.getElementById('divNuevoSaldo').style.display = 'none';
        } else {
            document.getElementById('divPago').style.display = 'block';
            document.getElementById('divNoCredito').style.display = 'none';
        }
    });
    $('#btnPagar').click(function () {
        if ($(txtDiasCredito).val() == 0) {
            swal('No hay fecha de vencimiento!', 'Ingrese los dias del credito', "warning");
            return;
        }
        limpiarPago();
        $("#modalPago").modal("show");
    });
    $(document).on("keyup change", "#txtMonto", function () {
        document.getElementById('divNuevoSaldo').style.display = 'block';
        let total = $(txtTotal).val().slice(4);
        let monto = $(txtMonto).val();
        let saldo = 0;
        if (total == _saldo) {
            saldo = total - monto;
        } else {
            saldo = total - monto - (total - _saldo);
        }
        if (saldo < 0) {
            $(txtSaldo).text(saldo);
            swal("Oh NO!", 'El monto de pago total supera al monto de venta', "warning");
            return;
        }
        if (saldo == 0.00) {
            $('#btnAgregarPago').prop('disabled', true);
        }
        $(txtSaldo).text(saldo.toFixed(2));
    });
    $('#btnGuardarPago').click(function () {        
        guardarPago();
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
                var color = '';
                var colorEstado = '';
                $.each(dataVentas, function (i, e) {
                    if (e.forma_pago == 'CREDITO') {
                        tDetalle += '<tr class="table-warning">';
                        color = 'badge-warning';
                    } else if (e.forma_pago == 'CONTADO') {
                        tDetalle += '<tr class="table-primary">';
                        color = 'badge-primary';
                    }

                    if (e.estado == 'PENDIENTE') {
                        colorEstado = 'badge-warning';
                    } else if (e.estado == 'AMORTIZADO') {
                        colorEstado = 'badge-primary';
                    }

                    tDetalle += '<td class="nro_venta" >' + e.codigo_venta + '</td >';
                    tDetalle += '<td class="tipoDocumento" hidden>' + e.tipo_documento + '</td >';
                    tDetalle += '<td class="documento">' + e.documento + '</td >';
                    tDetalle += '<td class="cliente">' + e.cliente + '</td >';
                    tDetalle += '<td class="fecha_venta">' + e.fecha_venta + '</td >';
                    tDetalle += '<td class="forma_pago">' + e.forma_pago + '</td >';
                    tDetalle += '<td class="moneda">' + e.moneda + '</td >';
                    tDetalle += '<td class="total">' + e.total + '</td >';
                    tDetalle += '<td class="condicion" style="font-size:12px"><div class="badge ' + colorEstado + ' badge-pill">' + e.estado + '</div></td>';
                    tDetalle += '<td style="text-align:center">';
                    tDetalle += '<button type="button" style="font-size:10px" class="btn btn-sm btn-success" onclick="verVenta(this)" title="Ver Venta"><i class="fas fa-paper-plane fa-sm"></i></button>';
                    tDetalle += '<button type="button" style="font-size:10px" class="btn btn-sm btn-danger" onclick="anularVenta(this)" title="Anular Venta"><i class="fas fa-ban fa-sm"></i></button>';
                    tDetalle += '</td>';
                    tDetalle += '<td class="diasCredito" hidden>' + e.dias_credito + '</td >';
                    tDetalle += '<td class="saldo" hidden>' + e.saldo + '</td >';
                    tDetalle += '<td class="fechaVencimiento" hidden>' + e.fecha_vencimiento + '</td >';
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

    $(txtDiasCredito).val(dataVentasS[0].dias_credito);
    $(txtSaldoVenta).text(dataVentasS[0].saldo.toFixed(2));
    _saldo = dataVentasS[0].saldo;
    $(txtFV).text(dataVentasS[0].fecha_vencimiento);

    if (dataVentasS[0].forma_pago == 'CREDITO') {
        $('#selectPago').val('05').change();
        $('#btnEmitir').css('visibility', 'hidden');
    } else {
        $('#selectPago').val('01').change();
        $('#btnEmitir').css('visibility', 'visible');
    }

    if (dataVentasS[0].estado == 'PENDIENTE' && dataVentasS[0].forma_pago == 'CREDITO') {
        document.getElementById('divCredito').style.display = 'block';
        document.getElementById('divDiasCredito').style.display = 'block';
        document.getElementById('divSaldo').style.display = 'none';
        document.getElementById('divFV').style.display = 'none';
        document.getElementById('divPago').style.display = 'block';
        document.getElementById('divNoCredito').style.display = 'none';
    } else if (dataVentasS[0].estado == 'AMORTIZADO' && dataVentasS[0].forma_pago == 'CREDITO') {
        document.getElementById('divCredito').style.display = 'none';
        document.getElementById('divDiasCredito').style.display = 'none';
        document.getElementById('divSaldo').style.display = 'block';
        document.getElementById('divFV').style.display = 'block';
        document.getElementById('divPago').style.display = 'block';
        document.getElementById('divNoCredito').style.display = 'none';
    } else {
        document.getElementById('divCredito').style.display = 'none';
        document.getElementById('divDiasCredito').style.display = 'none';
        document.getElementById('divSaldo').style.display = 'none';
        document.getElementById('divFV').style.display = 'none';
        document.getElementById('divPago').style.display = 'none';
        document.getElementById('divNoCredito').style.display = 'block';
    }

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
function guardarPago() {
    var estado_pago = 0;

    if ($(txtSaldo).text() < 0) {
        swal("No se puede registrar el pago!", "El saldo es menor a 0.", "error");
        return
    }

    if ($("#selectPago").val() == '05' && dataVentas[0].total == dataVentas[0].saldo) {
        let codigo_venta = $("#selectNroVenta").val();
        let codigo_usuario = 'U0000001';
        let dias = $("#txtDiasCredito").val();
        let abono = $("#txtMonto").val();

        var objCredito = {
            "codigo_venta": codigo_venta,
            "codigo_usuario": codigo_usuario,
            "dias": dias,
            "abono": abono
        }

        $.ajax({
            type: 'PUT',
            url: "https://localhost:44380/api/caja/GuardarCredito",
            async: false,
            headers: {
                'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0'
            },
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(objCredito),
            success: function (data) {

            }
        })
    }

    let codigo_venta = $("#selectNroVenta").val();
    let codigo_usuario = 'U0000001';
    let tipo_pago = $("#selectFormaPago").val();
    let monto = $("#txtMonto").val();

    var objPago = {
        "codigo_venta": codigo_venta,
        "codigo_usuario": codigo_usuario,
        "tipo_pago": tipo_pago,
        "monto": monto
    }

    $.ajax({
        type: 'PUT',
        url: "https://localhost:44380/api/caja/GuardarPago",
        async: false,
        headers: {
            'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0'
        },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(objPago),
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

            let mensaje = data.value.split(',');
            estado_pago = mensaje[0];
            let resultado = mensaje[1];

            swal("Buen trabajo!", resultado, "success");
        },
        error: function (datoError) {
            swal("Oops!", "No se pudo guardar el cambio.", "error");
        },
        complete: function () {
            $("#modalPago").modal("hide");
            if (estado_pago == 1) {
                document.getElementById('divCredito').style.display = 'none';
                document.getElementById('divDiasCredito').style.display = 'none';
                document.getElementById('divSaldo').style.display = 'none';
                document.getElementById('divFV').style.display = 'none';
                document.getElementById('divPago').style.display = 'none';
                document.getElementById('divNoCredito').style.display = 'block';
                $('#btnEmitir').css('visibility', 'visible');
            } else {
                limpiarComprobante();
            }            
        }
    })
}
function limpiarPago() {
    loadCombo([], 'selectFormaPago', false);    
    loadCombo(dataTipoPago, 'selectFormaPago', false);
    $('selectFormaPago').selectpicker('refresh');
        
    $("#txtMonto").val('');
    document.getElementById('divNuevoSaldo').style.display = 'none';
    $("#txtSaldo").val('');
}
function guardarComprobante() {
    if (!obligatorioComprobante()) {
        return
    }

    if ($("#selectPago").val() != '05') {
        let codigo_venta = $("#selectNroVenta").val();
        let codigo_usuario = 'U0000001';
        let tipo_pago = $("#selectPago").val();
        let monto = $(txtTotal).val().slice(4);

        var objPago = {
            "codigo_venta": codigo_venta,
            "codigo_usuario": codigo_usuario,
            "tipo_pago": tipo_pago,
            "monto": monto
        }

        $.ajax({
            type: 'PUT',
            url: "https://localhost:44380/api/caja/GuardarPago",
            async: false,
            headers: {
                'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0'
            },
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(objPago),
            success: function (data) {
                
            }
        })
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
    $(txtDiasCredito).val('');
    document.getElementById('divCredito').style.display = 'none';
    document.getElementById('divDiasCredito').style.display = 'none';
    document.getElementById('divSaldo').style.display = 'none';
    document.getElementById('divFV').style.display = 'none';
    document.getElementById('divPago').style.display = 'none';
    document.getElementById('divNoCredito').style.display = 'block';
    document.getElementById('divNuevoSaldo').style.display = 'none';
    $("table#tblDetalle").DataTable().destroy();
    $("table#tblDetalle>tbody").empty();
}
function verVenta(xthis) {
    let nroVenta = $(xthis).closest("tr").find('.nro_venta').text();
    let cliente = $(xthis).closest("tr").find('.cliente').text();
    let tipoDocumento = $(xthis).closest("tr").find('.tipoDocumento').text();
    let documento = $(xthis).closest("tr").find('.documento').text();
    let total = $(xthis).closest("tr").find('.total').text();
    let diasCredito = $(xthis).closest("tr").find('.diasCredito').text();
    let saldo = $(xthis).closest("tr").find('.saldo').text();    
    let fechaVencimiento = $(xthis).closest("tr").find('.fechaVencimiento').text();

    $("#selectNroVenta").val(nroVenta).selectpicker('refresh').change();
    $(txtCliente).val(cliente);
    $(txtTipoDocumento).val(tipoDocumento);
    $(txtDocumento).val(documento);
    $(txtTotal).val('S/. ' + total);
    $(txtDiasCredito).val(diasCredito);
    $(txtSaldoVenta).text(saldo);
    _saldo = saldo;
    $(txtFV).text(fechaVencimiento);

    document.getElementById("selectComprobante").focus();
    listarDetalleVenta(nroVenta);

}
function anularVenta(xthis) {
    let codigo_venta = $(xthis).closest("tr").find('.nro_venta').text();
    let codigo_usuario = 'U0000001';

    Swal.fire({
        title: 'Esta seguro de anular la venta: ' + codigo_venta + ' ?',
        icon: 'info',
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonText: 'Sí',
        cancelButtonText: 'No',
        confirmButtonColor: '#4CAA42',
        cancelButtonColor: '#d33'
    }).then((result) => {
        if (result.dismiss === Swal.DismissReason.cancel) {
            swal("Buenisimo!", 'Anulacion de venta cancelada', "warning");
        } else {
            confirmarAnulacionVenta(codigo_venta, codigo_usuario);
        }
    });    
}
function confirmarAnulacionVenta(codigo_venta, codigo_usuario) {

    $.ajax({
        type: 'DELETE',
        url: "https://localhost:44380/api/ventas/AnularVenta?codigo_venta=" + codigo_venta + "&codigo_usuario=" + codigo_usuario,
        async: false,
        headers: {
            'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0'
        },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
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
            swal("Oops!", "No se pudo anular la venta.", "error");
        },
        complete: function () {
            $("#collapseVentas").collapse('toggle');
            listarVentasPendientes();
        }
    })
}