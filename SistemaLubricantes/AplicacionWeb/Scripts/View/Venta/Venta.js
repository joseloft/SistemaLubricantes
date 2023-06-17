var dataProductos = [];
var dataClientes = [];
var dataBusqueda = [];
var tipoCambio = localStorage.getItem("tipoCambio");
$(document).ready(function () {    

    $("#dCredito").css("display", "none");

    listarProductos();
    $.LoadingOverlay("hide");
    listarClientes();
    $('#credito').click(function () {
        $("#dCredito").css("display", "block");
        $('#contado').prop('checked', false);
    });
    $('#contado').click(function () {
        $("#dCredito").css("display", "none");
        $('#credito').prop('checked', false);
    });
    $(document).on("click", "#btnBuscar", function () {
        buscarCliente();
    });
    $(document).on("change", "#iptDocumento", function () {
        loadCombo([], 'selectCliente', true);
        $("#iptPlaca").val('');
    });
    $(document).on("change", "#selectCliente", function () {
        $("#iptDocumento").val('');
        $("#iptPlaca").val('');
    });
    $(document).on("change", "#iptPlaca", function () {
        $("#iptDocumento").val('');
        loadCombo([], 'selectCliente', true);
    });
    $('#btnAgregar').click(function () {
        limpiarClientes();
        $("#modalCliente").modal("show");
    });
    $('#btnGuardarCliente').click(function () {
        guardarCliente();
    });
    $('#chkAceite').click(function () {
        if ($('#chkAceite').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkAceite").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkMuelle').click(function () {
        if ($('#chkMuelle').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkMuelle").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkFiltro').click(function () {
        if ($('#chkFiltro').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkFiltro").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkFaja').click(function () {
        if ($('#chkFaja').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkFaja").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkAmortiguador').click(function () {
        if ($('#chkAmortiguador').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkAmortiguador").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkBateria').click(function () {
        if ($('#chkBateria').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkBateria").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkRodaje').click(function () {
        if ($('#chkRodaje').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkRodaje").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkGeneral').click(function () {
        if ($('#chkGeneral').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkGeneral").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkAceite').click(function () {
        if ($('#chkAceite').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkAceite").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkManguera').click(function () {
        if ($('#chkManguera').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkManguera").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkFoco').click(function () {
        if ($('#chkFoco').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkFoco").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkPerno').click(function () {
        if ($('#chkPerno').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkPerno").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkCruceta').click(function () {
        if ($('#chkCruceta').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkCruceta").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkRefrigerante').click(function () {
        if ($('#chkRefrigerante').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkRefrigerante").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkLiquidoFreno').click(function () {
        if ($('#chkLiquidoFreno').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkLiquidoFreno").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkSoporte').click(function () {
        if ($('#chkSoporte').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkSoporte").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkAgua').click(function () {
        if ($('#chkAgua').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkAgua").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkJebe').click(function () {
        if ($('#chkJebe').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkJebe").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#chkOtro').click(function () {
        if ($('#chkOtro').is(":checked")) {
            let data = dataProductos.filter(item => item.cod_categoria == $("#chkOtro").val());
            filtradoProductos(data);
        } else {
            filtradoProductos(dataProductos);
        }
        $.LoadingOverlay("hide");
    });
    $('#btnProducto').click(function () {
        $("#modalProducto").modal("show");
    });
    $(document).on("click", ".row-remove", function () {
        $(this).parents('tr').detach();
        calcularTotal();
    });
    $(document).on("keyup", "input.cantidad", function (e) {
        let cantidad = (parseFloat($(this).closest("tr").find('input.cantidad').val()));
        let precio = $(this).closest("tr").find('input.precio').val();

        let total = cantidad * precio;
        $(this).closest("tr").find('td.total').text(total.toFixed(2));

        calcularTotal();
    });
    $(document).on("keyup", "input.precio", function (e) {
        let precio = $(this).closest("tr").find('input.precio').val();
        //let precio = $(this).closest("tr").find('td.precio>input').val();
        let cantidad = (parseFloat($(this).closest("tr").find('input.cantidad').val()));
        
        let total = cantidad * precio;
        $(this).closest("tr").find('td.total').text(total.toFixed(2));

        calcularTotal();
    });    
    /*
    $('#selectDescuento').change(function () {
        $(".descuento").val(0);
        calcularTotal();
    });
    */
});
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
function listarClientes() {
    loadCombo([], 'selectCliente', true);
    $.ajax({
        type: "GET",
        url: "https://localhost:44380/api/clientes/ListarClientes",
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            let clientes = data.value;
            if (clientes.length > 0) {
                dataClientes = clientes.map(
                    obj => {
                        return {
                            "id": obj.cod_cliente,
                            "name": obj.nombres + ' ' + obj.apellidos
                        }
                    }
                );
            }
            loadCombo(dataClientes, 'selectCliente', true, 'Seleccione un cliente');
            $('selectCliente').selectpicker('refresh');
        }
    });
}
function listarProductos() {    
    //$("table#tblProducto").DataTable().destroy();

    $.ajax({
        type: "GET",
        url: "https://localhost:44380/api/productos/ListarProductos",
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            dataProductos = data.value;
            //$("table#tblProducto tbody").empty();
            var content = '';
            $.each(dataProductos, function (i, e) {
                content += '<tr>';
                content += '<td style="text-align:center;min-width:5px!">';
                content += '<button type="button" style="font-size:5px" class="btn btn-sm btn-primary" onclick="agregarProducto(this)"><i class="fas fa-plus fa-sm"></i></button>';
                content += '</td>';
                content += '<td class="categoria" style="min-width:10px;" hidden>' + e.cod_categoria + '</td >';
                content += '<td class="codigo" style="min-width:15px;text-align:center!">' + e.cod_producto + '</td >';
                content += '<td class="nombre" style="min-width:220px!">' + e.nombre + '</td >';
                content += '<td class="marca" style="min-width:60px;text-align:center!">' + e.marca + '</td >';
                content += '<td class="stock" style="min-width:15px;text-align:center!">' + e.stock + '</td >';
                content += '<td class="precio" style="min-width:15px;text-align:center!">' + e.precio_venta + '</td >';
                content += '<td class="moneda" style="min-width:15px;text-align:center!">' + e.moneda + '</td >';
                content += '</tr>';
            })

            $("table#tblProducto tbody").append(content);
            table = $('table#tblProducto').DataTable({
                //columns: adjust,
                "scrollY": "200px",
                "scrollX": true,
                responsive: true,                
                //scrollCollapse: false,
                //orderCellsTop: false,
                //fixedHeader: true,
                searching: true,
                "searchable": true,
                ordering: false,
                info: false,                
                "bPaginate": true,
                "paging": true,
                "language": {
                    "url": "https://cdn.datatables.net/plug-ins/1.11.3/i18n/es_es.json"
                }
            });

        }
    });
    $.LoadingOverlay("show");
}
function buscarCliente() {
    let cod_cliente = $("#selectCliente").val();
    let documento = $("#iptDocumento").val();
    let placa = $("#iptPlaca").val();

    $.ajax({
        type: "GET",
        url: "https://localhost:44380/api/clientes/BuscarClientes?cod_cliente=" + cod_cliente + "&documento=" + documento + "&placa=" + placa,
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            dataBusqueda = data.value;
        },
        error: function (datoEr) {
            swal("Opps!", "No se encontro ningun cliente.", "warning");
        },
        complete: function () {
            $(iptDocumento).val(dataBusqueda.documento);
            $(iptPlaca).val(dataBusqueda.placa);

            let data = dataClientes.filter(item => item.id == dataBusqueda.cod_cliente).map(
                obj => {
                    return {
                        "id": obj.id,
                        "name": obj.name
                    }
                });
            loadCombo(data, 'selectCliente', false);
            $('selectCliente').selectpicker('refresh');
            
        }
    });
}
function guardarCliente() {
    if (!obligatorioClientes()) {
        return
    }

    let nombres = $("#txtnombres").val();
    let apellidos = $("#txtapellidos").val();
    let celular = $("#txtcelular").val();
    let distrito = $("#txtdistrito").val();
    let telefono = $("#txttelefono").val();
    let direccion = $("#txtdireccion").val();
    let correo = $("#txtcorreo").val();
    let dni = $("#txtdni").val();
    let ruc = $("#txtruc").val();

    var objCliente = {
        "distrito": distrito,
        "telefono": telefono,
        "correo": correo,
        "nombres": nombres,
        "apellidos": apellidos,
        "direccion": direccion,
        "celular": celular,
        "dni": dni,
        "ruc": ruc
    }

    $.ajax({
        type: 'POST',
        url: "https://localhost:44380/api/clientes/GuardarClientes",
        async: false,
        headers: {
            'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0'
        },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(objCliente),
        success: function (data) {            
            let bResult = false;
            if (data.hasOwnProperty('statusCode')) {
                switch (data.statusCode) {
                    case null: bResult = true; break;
                    case 9:
                        swal("Opps", data.message, "error"); break;
                    default: swal("Opps", "Ocurrió un problema!", "error"); break;
                }
            }
            if (!bResult) return false;
            swal("Buen trabajo!", data.value, "success");
        },
        error: function (datoError) {
            swal("Opps!", "No se pudo guardar el cambio.", "error");
        },
        complete: function () {
            listarClientes();
            $("#modalCliente").modal("hide");
        }
    })

}
function obligatorioClientes() {
    if ($(txtnombres).val() == '') {
        swal('Datos incompletos', 'Falta ingresar el nombre o razon social', "warning");
        return false;
    }
    if ($(txtcorreo).val() == '') {
        swal('Datos incompletos', 'Falta ingresar el correo electronico', "warning");
        return false;
    }
    if ($(txtdni).val() == '') {
        swal('Datos incompletos', 'Falta ingresar el numero de dni', "warning");
        return false;
    }
    if ($(txtruc).val() == '') {
        swal('Datos incompletos', 'Falta ingresar el numero de ruc', "warning");
        return false;
    }

    return true;
}
function limpiarClientes() {
    $("#txtnombres").val('');
    $("#txtapellidos").val('');
    $("#txtcelular").val('');
    $("#txtdistrito").val('');
    $("#txttelefono").val('');
    $("#txtdireccion").val('');
    $("#txtcorreo").val('');
    $("#txtdni").val('');
    $("#txtruc").val('');
}
function filtradoProductos(data) {    

    $("table#tblProducto").DataTable().destroy();
    $("table#tblProducto tbody").empty();
    var tblFiltro = '';
    $.each(data, function (i, e) {
        tblFiltro += '<tr>';
        tblFiltro += '<td style="text-align:center;min-width:5px!">';
        tblFiltro += '<button type="button" style="font-size:5px" class="btn btn-sm btn-primary" onclick="agregarProducto(this)"><i class="fas fa-plus fa-sm"></i></button>';
        tblFiltro += '</td>';
        tblFiltro += '<td class="categoria" style="min-width:10px;" hidden>' + e.cod_categoria + '</td >';
        tblFiltro += '<td class="codigo" style="min-width:15px;text-align:center!">' + e.cod_producto + '</td >';
        tblFiltro += '<td class="nombre" style="min-width:220px!">' + e.nombre + '</td >';
        tblFiltro += '<td class="marca" style="min-width:60px;text-align:center!">' + e.marca + '</td >';
        tblFiltro += '<td class="stock" style="min-width:15px;text-align:center!">' + e.stock + '</td >';
        tblFiltro += '<td class="precio" style="min-width:15px;text-align:center!">' + e.precio_venta + '</td >';
        tblFiltro += '<td class="moneda" style="min-width:15px;text-align:center!">' + e.moneda + '</td >';
        tblFiltro += '</tr>';
    })

    $("table#tblProducto tbody").append(tblFiltro);
    table = $('table#tblProducto').DataTable({
        //columns: adjust,
        "scrollY": "200px",
        "scrollX": true,
        responsive: true,
        //scrollCollapse: false,
        //orderCellsTop: false,
        //fixedHeader: true,
        searching: true,
        "searchable": true,
        ordering: false,
        info: false,
        "bPaginate": true,
        "paging": true,
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.3/i18n/es_es.json"
        }
    });

    $.LoadingOverlay("show");
}
function agregarProducto(xthis) {
    tipoCambio = localStorage.getItem("tipoCambio");
    
    let stock = $(xthis).closest("tr").find('.stock').text();

    if (stock <= 0) {
        swal("Sin stock!", "El producto no pudo ser agregado.", "warning");
        return;
    } else {
        let codigo = $(xthis).closest("tr").find('.codigo').text();
        let producto = $(xthis).closest("tr").find('.nombre').text();
        let precio = $(xthis).closest("tr").find('.precio').text();
        let moneda = $(xthis).closest("tr").find('.moneda').text();

        if (moneda.trim() == 'USS') { precio = (precio * tipoCambio).toFixed(2) }

        var content = "";
        content += '<tr>';
        content += "<td style='max-width:30px;min-width:30px;text-align:center'><button style='font-size:8px' class='row-remove btn btn-sm btn-danger'><i class='fas fa-minus'></i></button></td>"
        content += "<td class='codigo' style = 'max-width:50px;min-width:50px;text-align:center'>" + codigo + "</td >"
        content += "<td class='descripcion' style='max-width:300px;min-width:300px;text-align:center'>" + producto + "</td>"
        content += "<td style='max-width:60px;min-width:60px'><input type='number' style='font-size:10px;text-align:right' class='cantidad form-control form-control-sm' /></td>"
        content += "<td class='monto' style='max-width:50px;min-width:50px;' hidden>" + precio + "</td>"
        content += "<td style='max-width:60px;min-width:60px'><input type='number' style='font-size:10px;text-align:right' class='precio form-control form-control-sm' value='" + precio + "'/></td>"
        content += "<td class='total' style='max-width:50px;min-width:50px;text-align:right'>" + 0 + "</td>"

        $("table#tblDetalle tbody").append(content);
    }
}
function calcularTotal() {
    let subTotal = 0;
    let igv = 0;
    let montoTotal = 0;
    $('table#tblDetalle tbody tr td.total').each(function () {
        montoTotal += parseFloat($(this).text());
    });

    igv = montoTotal * 0.18;
    subTotal = montoTotal - igv;
    $("#subTotal").val(subTotal.toFixed(2));
    $("#igv").val(igv.toFixed(2));
    $("#total").val(montoTotal.toFixed(2));
}