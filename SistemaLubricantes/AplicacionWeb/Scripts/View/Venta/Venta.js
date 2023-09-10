var dataProductos = [];
var dataClientes = [];
var dataBusqueda = [];
var dataMoneda = [];
var dataCategoria = [];
var dataMarca = [];
var dataTipo = [];
var dataBalde = [];
var dataEnvase = [];
var dataPaquete = [];
var dataCilindro = [];
var tipoCambio = localStorage.getItem("tipoCambio");
var dataLastVentas = [];
var dataDetalle = [];
var tablaLast;
var table;
$(document).ready(function () {    

    $.LoadingOverlay("show");

    //$("#dCredito").css("display", "none");
    listarProductos();    
    listarClientes();

    $.LoadingOverlay("hide");
    $('#chkCredito').click(function () {
        //$("#dCredito").css("display", "block");
        if ($("#selectCliente").val() == 'C0000004') {
            $('#chkCredito').prop('checked', false);
            $('#chkContado').prop('checked', true);
            swal("Solo contado!", "El cliente publico solo admite al contado", "warning");
            return
        }
        
        $('#chkContado').prop('checked', false);
    });
    $('#chkContado').click(function () {
        //$("#dCredito").css("display", "none");
        $('#chkCredito').prop('checked', false);
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
    //$(document).on("change", "#iptPlaca", function () {
    //    $("#iptDocumento").val('');
    //    loadCombo([], 'selectCliente', true);
    //});
    $('#btnAgregar').click(function () {
        limpiarClientes();
        $("#modalCliente").modal("show");
        document.getElementById('sinCorreo').style.display = 'block';
        document.getElementById('conCorreo').style.display = 'none';
    });
    $(document).on("keydown", "#txtNroDocumento", function (e) {
        if ($('#chkNatural').is(":checked") || $('#chkJuridica').is(":checked")) {

        } else {
            swal("Atencion!", "Marque el tipo de cliente.", "warning");
        }
    });
    $('#chkNatural').click(function () {
        $('#chkJuridica').prop('checked', false);
    });
    $('#chkJuridica').click(function () {
        $('#chkNatural').prop('checked', false);
    });
    $('#btnGuardarCliente').click(function () {
        guardarCliente();
    });
    $('#btnLastVentas').click(function () {
        if ($("#iptPlaca").val() == '' && $("#selectCliente").val() == '')
        {
            swal("Falto algo!", "Ingrese la placa o seleccione un cliente", "warning");
            return;
        }
        
        listarLastVentas();        
    });
    $('#tblLastVenta tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');;
        var row = tablaLast.row(tr);

        if (row.child.isShown()) {
            // La fila ya tiene detalles visibles, ocultarlos
            row.child.hide();
            tr.removeClass('shown');
        } else {
            // Obtener el ID del elemento de la fila
            let codigo_venta = $(this).closest("tr").find('td.cod_venta').text();
            var html = '';
            // Llamar a la API para obtener los detalles del elemento
            $.ajax({
                url: getPath() + "api/ventas/ListarDetalleVentaPendientes?codigo_venta=" + codigo_venta,
                method: 'GET',
                success: function (data) {
                    // Crear el contenido HTML de los detalles
                    dataDetalle = data.value;                    
                    $.each(dataDetalle, function (i, e) {
                        html += '<tr>';
                        html += '<td class="item">' + e.nro_detalle + '</td >';
                        html += '<td class="producto">' + e.producto + '</td >';
                        html += '<td class="marca">' + e.marca + '</td >';
                        html += '<td class="cantidad">' + e.cantidad + '</td >';
                        html += '<td class="precio">' + e.precio + '</td >';
                        html += '<td class="total">' + e.total + '</td >';
                        html += '</tr>';
                    })
                    // Mostrar los detalles en la fila
                    row.child('<div class="details"><p><strong>ID:</p></div>').show();
                    tr.addClass('shown');
                }
            });            
        }
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
        limpiarProductos();
        cargarProductos();
        $("#modalProducto").modal("show");
        $('#selectTipo').prop('disabled', 'disabled');
        $('#selectMarca').prop('disabled', 'disabled');
    });
    $('#btnAgregarMarca').click(function () {
        cargarMarca();
        $("#modalMarca").modal("show");
    });
    $('#btnAgregarTipo').click(function () {
        cargarTipo();
        $("#modalTipo").modal("show");
    });
    $('#selectCategoria').click(function () {
        if ($('#chkFormarNombre').is(":checked")) {

        } else {
            swal("Atencion!", "Marca la opcion para formar el nombre.", "warning");
            return
        }
    });
    $(document).on("change", "#selectCategoria", function () {        
        $('#selectTipo').removeAttr('disabled');
        $('#selectMarca').removeAttr('disabled');
        filtroCategoria($(this).val());
    });
    $(document).on("keydown", "#iptNombreProducto", function (e) {
        if ($('#chkIngresarNombre').is(":checked")) {

        } else {
            swal("Atencion!", "Marca la opcion para ingresar el nombre.", "warning");
        }
    });
    $('#chkFormarNombre').click(function () {
        if ($('#chkFormarNombre').is(":checked")) {
            $("#chkIngresarNombre").prop("checked", false);
            $('#iptNombreProducto').prop('disabled', 'disabled');
            $('#selectCategoria').removeAttr('disabled');
            $('#selectMarca').removeAttr('disabled');
            loadCombo(dataCategoria, 'selectCategoria', true, 'Seleccione Categoria');
            $('selectCategoria').selectpicker('refresh');
            loadCombo([], 'selectTipo', false);
            loadCombo([], 'selectMarca', false);
        }
    });
    $('#chkIngresarNombre').click(function () {
        if ($('#chkIngresarNombre').is(":checked")) {
            $("#chkFormarNombre").prop("checked", false);
            $('#iptNombreProducto').removeAttr('disabled');
            $('#selectCategoria').prop('disabled', 'disabled');
            //$('#selectMarca').prop('disabled', 'disabled');
            let categorias = dataCategoria.filter(item => item.id == 'C00001');
            loadCombo(categorias, 'selectCategoria', false);
            $('selectCategoria').selectpicker('refresh');
            filtroCategoria('C00001');
        }
    });
    $('#chkBalde').click(function () {
        if ($('#chkBalde').is(":checked")) {
            seleccionarBalde();
        }
    });
    $('#chkEnvase').click(function () {
        if ($('#chkEnvase').is(":checked")) {
            seleccionarEnvase();
        } 
    });
    $('#chkPaquete').click(function () {
        if ($('#chkPaquete').is(":checked")) {
            seleccionarPaquete();
        }
    });
    $('#chkCilindro').click(function () {
        if ($('#chkCilindro').is(":checked")) {
            seleccionarCilindro();
        }
    });
    $('#chkBaldeC').click(function () {
        if ($('#chkBaldeC').is(":checked")) {
            seleccionarBaldeC();
        }
    });
    $('#chkEnvaseC').click(function () {
        if ($('#chkEnvaseC').is(":checked")) {
            seleccionarEnvaseC();
        }
    });
    $('#chkPaqueteC').click(function () {
        if ($('#chkPaqueteC').is(":checked")) {
            seleccionarPaqueteC();
        }
    });
    $('#chkCilindroC').click(function () {
        if ($('#chkCilindroC').is(":checked")) {
            seleccionarCilindroC();
        }
    });
    $(document).on("change", "#selectTipo", function () {
        formarNombreProducto();
    });
    $(document).on("keyup", "#iptModelo", function () {
        formarNombreProducto();
    });
    $('#btnGuardarMarca').click(function () {
        guardarMarca();
    });
    $('#btnGuardarTipo').click(function () {
        guardarTipo();
    });
    $('#btnGuardarProducto').click(function () {
        guardarProductos();
    });    
    $(document).on("click", ".row-remove", function () {
        $(this).parents('tr').detach();
        calcularTotal();
    });
    $(document).on("keyup change", "input.cantidad", function (e) {
        let stock = (parseFloat($(this).closest("tr").find('td.stock').text()));
        let cantidad = (parseFloat($(this).closest("tr").find('input.cantidad').val()));
        let precio = $(this).closest("tr").find('input.precio').val();
        //let precio = $(this).closest("tr").find('td.precio>input').val();

        if (cantidad > stock) {
            swal("Falta STOCK!", "La cantidad del producto es mayor al stock.", "warning");
            $(this).closest("tr").find('input.cantidad').val(0);
            $(this).closest("tr").find('td.total').text(0);
            calcularTotal();
            return;
        }

        let total = cantidad * precio;
        $(this).closest("tr").find('td.total').text(total.toFixed(2));

        calcularTotal();
    });
    $(document).on("keyup change", "input.precio", function (e) {
        let precio = $(this).closest("tr").find('input.precio').val();
        let cantidad = (parseFloat($(this).closest("tr").find('input.cantidad').val()));
        
        let total = cantidad * precio;
        $(this).closest("tr").find('td.total').text(total.toFixed(2));

        calcularTotal();
    });
    $('#btnManual').click(function () {
        agregarManual();
    });    
    $('#btnGenerar').click(function () {
        guardarVenta();
    });
    $('#btnCancelar').click(function () {
        limpiarVenta();
    });

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
        url: getPath() + "api/clientes/ListarClientes",
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
                            "name": obj.nombres + ' ' + obj.apellidos,
                            "documento": obj.nroDocumento,//obj.dni == '' ? obj.ruc : obj.dni,
                            "placa": ''
                        }
                    }
                );
            }
            loadCombo(dataClientes, 'selectCliente', true, 'Seleccione el cliente');
            $('selectCliente').selectpicker('refresh');
        }
    });
}
function listarProductos() {    
    $("table#tblProducto").DataTable().destroy();

    $.ajax({
        type: "GET",
        url: getPath() + "api/productos/ListarProductos",
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            dataProductos = data.value;
            $("table#tblProducto>tbody").empty();
            var content = '';
            $.each(dataProductos, function (i, e) {
                content += '<tr>';
                content += '<td style="min-width:5px;max-width:5px">';
                content += '<button type="button" style="font-size:5px" class="btn btn-sm btn-primary" onclick="agregarProducto(this)"><i class="fas fa-plus fa-sm"></i></button>';
                content += '</td>';
                content += '<td class="categoria" style="" hidden>' + e.cod_categoria + '</td >';
                content += '<td class="codigo" style="min-width:15px;max-width:15px">' + e.cod_producto + '</td >';
                content += '<td class="nombre" style="min-width:250px;max-width:250px">' + e.nombre + '</td >';
                content += '<td class="marca" style="min-width:30px;max-width:30px">' + e.marca + '</td >';
                content += '<td class="stock" style="min-width:15px;max-width:15px">' + e.stock + '</td >';
                content += '<td class="precio" style="min-width:15px;max-width:15px">' + e.precio_venta + '</td >';
                content += '<td class="moneda" style="min-width:15px;max-width:15px">' + e.moneda + '</td >';
                content += '</tr>';
            })

            $("table#tblProducto>tbody").append(content);

            table = $('table#tblProducto').DataTable({
                "scrollY": "200px",
                "scrollX": true,                
                scrollCollapse: true,
                orderCellsTop: true,
                fixedHeader: false,
                //responsive: true,
                //searching: true,
                //"searchable": true,
                ordering: false,
                info: false,
                //"bPaginate": true,
                //"paging": true,
                "language": {
                    "url": "https://cdn.datatables.net/plug-ins/1.11.3/i18n/es_es.json"
                }
            });
        }
        
    });
    
}
function buscarCliente() {
    let cod_cliente = $("#selectCliente").val();
    let documento = $("#iptDocumento").val().trim();
    let placa = $("#iptPlaca").val().trim();

    $.ajax({
        type: "GET",
        url: getPath() + "api/clientes/BuscarClientes?cod_cliente=" + cod_cliente + "&documento=" + documento + "&placa=" + placa,
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            dataBusqueda = data.value;
            if (data.value != null) {
                $(iptDocumento).val(dataBusqueda.documento);
                $(iptPlaca).val(dataBusqueda.placa);
                //$('#selectCliente').val(dataBusqueda.cod_cliente).change();
                let data = dataClientes.filter(item => item.id == dataBusqueda.cod_cliente).map(
                    obj => {
                        return {
                            "id": obj.id,
                            "name": obj.name
                        }
                    });
                loadCombo(data, 'selectCliente', false);
                $('selectCliente').selectpicker('refresh');
            } else {
                dataBusqueda = [];
                $("#iptDocumento").val('');
                listarClientes();
                $("#iptPlaca").val('');
                swal("Oops!", "No se encontro ningún cliente.", "warning");
                return;
            }         
        },
        error: function (datoEr) {
            swal("Oops!", "No se encontro ningún cliente.", "warning");
        },
        complete: function () {      
        }
    });
}
function listarLastVentas() {
    $("#modalVentasAnteriores").modal("show");

    let placa = $("#iptPlaca").val().trim();
    let cod_cliente = $("#selectCliente").val();  

    $("table#tblLastVenta").DataTable().destroy();

    tablaLast = $('#tblLastVenta').DataTable({

        ajax: {
            type: "GET",
            url: getPath() + "api/ventas/ListaUltimasVentas?placa=" + placa + "&cod_cliente=" + cod_cliente,
            async: false,
            headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                dataLastVentas = data.value;
                if (dataLastVentas.length > 0) {
                    $("table#tblLastVenta>tbody").empty();
                    var last = '';
                    $.each(dataLastVentas, function (i, e) {
                        last += '<tr>';
                        last += '<td class="details-control" style="text-align:center">';
                        last += '<button type="button" style="font-size:10px" class="btn btn-sm btn-primary" onclick="mostrarLastDetalle(this)"><i class="fas fa-share fa-sm"></i></button>';
                        last += '</td>';
                        last += '<td class="cod_venta" hidden>' + e.cod_venta + '</td >';
                        last += '<td class="nro_venta">' + e.nro_venta + '</td >';
                        last += '<td class="fecha_venta">' + e.fecha_venta + '</td >';
                        last += '<td class="total">' + e.total + '</td >';
                        last += '<td class="estado">' + e.estado + '</td >';
                        last += '<td class="placa">' + e.placa + '</td >';
                        last += '<td class="tipo_cambio">' + e.tipo_cambio + '</td >';
                        last += '</tr>';
                    })
                    $("table#tblLastVenta>tbody").append(last);

                    $("#modalVentasAnteriores").modal("show");
                }
            },
            error: function (datoError) {
                $("#modalVentasAnteriores").modal("hide");
                swal("Cliente nuevo!", "No se encontro ventas anteriores", "warning");
                return;
            },
        },
        responsive: true,
        searching: false,
        "searchable": false,
        ordering: false,
        info: false,
        "bPaginate": false,
        "paging": false
    });
}
function mostrarLastDetalle(xthis) {
    var tr = $(this);
    var row = table.row(tr);

    if (row.child.isShown()) {
        // La fila ya tiene detalles visibles, ocultarlos
        row.child.hide();
        tr.removeClass('shown');
    } else {
        // Obtener el ID del elemento de la fila
        let codigo_venta = $(xthis).closest("tr").find('.cod_venta').text();
        //var codigo_venta = tr.attr('nro_venta');

        // Llamar a la API para obtener los detalles del elemento
        $.ajax({
            url: getPath() + "api/ventas/ListarDetalleVentaPendientes?codigo_venta=" + codigo_venta,
            method: 'GET',
            success: function (data) {
                // Crear el contenido HTML de los detalles
                var html = '<div class="details">' +
                    '<p><strong>ID:</strong> ' + data.marca + '</p>' +
                    '<p><strong>Detalles:</strong> ' + data.producto + '</p>' +
                    '</div>';

                // Mostrar los detalles en la fila
                row.child(html).show();
                tr.addClass('shown');
            }
        });
    }
}
function guardarCliente() {
    if (!obligatorioClientes()) {
        return
    }

    let codigo_cliente = "";

    let nombres = $("#txtnombres").val();
    let apellidos = $("#txtapellidos").val();
    let celular = $("#txtcelular").val();
    let distrito = $("#txtdistrito").val();
    let telefono = $("#txttelefono").val();
    let direccion = $("#txtdireccion").val();
    let correo = $("#txtcorreo").val().length == 0 ? '' : $("#txtcorreo").val();
    let tipoCliente = $('#chkNatural').is(":checked") ? 'N' : $('#chkJuridica').is(":checked") ? 'J' : '';
    let nroDocumento = $("#txtNroDocumento").val();

    var objCliente = {
        "distrito": distrito,
        "telefono": telefono,
        "correo": correo,
        "nombres": nombres,
        "apellidos": apellidos,
        "direccion": direccion,
        "celular": celular,
        "tipoCliente": tipoCliente,
        "nroDocumento": nroDocumento
    }

    $.ajax({
        type: 'POST',
        url: getPath() + "api/clientes/GuardarClientes",
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
                        swal("Oops", data.message, "error"); break;
                    default: swal("Oops", "Ocurrió un problema!", "error"); break;
                }
            }
            if (!bResult) return false;

            let mensaje = data.value.split(',');
            codigo_cliente = mensaje[0];
            let resultado = mensaje[1];

            if (resultado == 'El correo del cliente ya existe') {
                swal("Oops!", resultado, "warning");
                return
            } else {
                swal("Buen trabajo!", resultado, "success");
            }
        },
        error: function (datoError) {
            swal("Oops!", "No se pudo guardar el cambio.", "error");
        },
        complete: function () {
            listarClientes();            
            let cliente = dataClientes.filter(item => item.id == codigo_cliente);
            $(iptDocumento).val(cliente[0].documento);
            //$('#selectCliente').val(cliente[0].id).change();
            loadCombo(cliente, 'selectCliente', false);
            $('selectCliente').selectpicker('refresh');
            $(iptPlaca).val(cliente[0].placa);            

            $("#modalCliente").modal("hide");
        }
    })

}
function obligatorioClientes() {
    if ($(txtnombres).val() == '') {
        swal('Datos incompletos', 'Falta ingresar el nombre o razon social', "warning");
        return false;
    }
    if ($(txtdistrito).val() == '') {
        swal('Datos incompletos', 'Falta ingresar el distrito', "warning");
        return false;
    }
    if ($(txtdireccion).val() == '') {
        swal('Datos incompletos', 'Falta ingresar la direccion', "warning");
        return false;
    }
    if ($(txtcorreo).val() != '') {
        let validarEmail = /^\w+([.-_+]?\w+)*@\w+([.-]?\w+)*(\.\w{2,10})+$/;
        if (!validarEmail.test($(txtcorreo).val())) {
            swal('Correo invalido', 'El formato del correo es incorrecto', "warning");
            return false;
        }
    }
    if ($('#chkNatural').is(":checked")) {
        if ($(txtNroDocumento).val().toString().length != 8) {
            swal('DNI invalido', 'El DNI debe tener 8 digitos', "warning");
            return false;
        }        
    }
    if ($('#chkJuridica').is(":checked")) {
        if ($(txtNroDocumento).val().toString().length != 11) {
            swal('RUC invalido', 'El RUC debe tener 11 digitos', "warning");
            return false;
        }
    }
    if ($(txtNroDocumento).val() != '' && $(txtcorreo).val() == '') {
        swal('Datos incompletos', 'Falta ingresar el correo electronico', "warning");
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
    $("#chkNatural").prop("checked", false);
    $("#chkJuridica").prop("checked", false);
    $("#txtNroDocumento").val('');
}
function filtradoProductos(data) {    
    $.LoadingOverlay("show");

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
        
}
function limpiarProductos() {
    $("#txtCodigoProducto").val('');
    $("#chkFormarNombre").prop("checked", false);
    loadCombo([], 'selectTipo', false);
    $("#iptModelo").val('');
    $("#chkIngresarNombre").prop("checked", false);
    $("#iptNombreProducto").val('');
    loadCombo([], 'selectMarca', false);
    $("#iptStock").val('0');
    $("#iptPrecioCompra").val('0');
    $("#iptPrecioVenta").val('0');
    $("#chkBalde").prop("checked", false);
    $("#chkEnvase").prop("checked", false);
    $("#chkPaquete").prop("checked", false);
    $("#chkCilindro").prop("checked", false);
    $('#selectBalde').removeAttr('disabled');
    $('#selectEnvase').removeAttr('disabled');
    $('#selectPaquete').removeAttr('disabled');
    $('#selectCilindro').removeAttr('disabled');   
    $("#chkBaldeC").prop("checked", false);
    $("#chkEnvaseC").prop("checked", false);
    $("#chkPaqueteC").prop("checked", false);
    $("#chkCilindroC").prop("checked", false);
    $('#selectBaldeC').removeAttr('disabled');
    $('#selectEnvaseC').removeAttr('disabled');
    $('#selectPaqueteC').removeAttr('disabled');
    $('#selectCilindroC').removeAttr('disabled');

}
function cargarProductos() {
    $.ajax({
        type: "GET",
        url: getPath() + "api/productos/ObtenerCodigoProducto",
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            let codigoProducto = data.value;
            $("#txtCodigoProducto").val(codigoProducto);
        }
    });

    loadCombo([], 'selectMoneda', false);
    $.ajax({
        type: "GET",
        url: getPath() + "api/productos/ListarMoneda",
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            let monedas = data.value;
            if (monedas.length > 0) {
                dataMoneda = monedas.map(
                    obj => {
                        return {
                            "id": obj.tipo_moneda,
                            "name": obj.moneda
                        }
                    }
                );
            }
            loadCombo(dataMoneda, 'selectMoneda', false);
            $('selectMoneda').selectpicker('refresh');
        }
    });
   
    loadCombo([], 'selectCategoria', false);
    $.ajax({
        type: "GET",
        url: getPath() + "api/productos/ListarCategoria",
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            let categorias = data.value;
            if (categorias.length > 0) {
                dataCategoria = categorias.map(
                    obj => {
                        return {
                            "id": obj.codigo_categoria,
                            "name": obj.categoria
                        }
                    }
                );
            }
            loadCombo(dataCategoria, 'selectCategoria', true, 'Seleccione Categoria');
            $('selectCategoria').selectpicker('refresh');
        }
    });       

    loadCombo([], 'selectBalde', false);
    loadCombo([], 'selectBaldeC', false);
    $.ajax({
        type: "GET",
        url: getPath() + "api/productos/ListarBalde?parametro=" + 1,
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            let balde = data.value;
            if (balde.length > 0) {
                dataBalde = balde.map(
                    obj => {
                        return {
                            "id": obj.codigo_balde,
                            "name": obj.balde
                        }
                    }
                );
            }
            loadCombo(dataBalde, 'selectBalde', true, 'Seleccione Balde');
            $('selectBalde').selectpicker('refresh');
            loadCombo(dataBalde, 'selectBaldeC', true, 'Seleccione Balde');
            $('selectBaldeC').selectpicker('refresh');
        }
    });

    loadCombo([], 'selectEnvase', false);
    loadCombo([], 'selectEnvaseC', false);
    $.ajax({
        type: "GET",
        url: getPath() + "api/productos/ListarEnvase?parametro=" + 2,
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            let envase = data.value;
            if (envase.length > 0) {
                dataEnvase = envase.map(
                    obj => {
                        return {
                            "id": obj.codigo_envase,
                            "name": obj.envase
                        }
                    }
                );
            }
            loadCombo(dataEnvase, 'selectEnvase', true, 'Seleccione Envase');
            $('selectEnvase').selectpicker('refresh');
            loadCombo(dataEnvase, 'selectEnvaseC', true, 'Seleccione Envase');
            $('selectEnvaseC').selectpicker('refresh');
        }
    });

    loadCombo([], 'selectPaquete', false);
    loadCombo([], 'selectPaqueteC', false);
    $.ajax({
        type: "GET",
        url: getPath() + "api/productos/ListarPaquete?parametro=" + 3,
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            let paquete = data.value;
            if (paquete.length > 0) {
                dataPaquete = paquete.map(
                    obj => {
                        return {
                            "id": obj.codigo_paquete,
                            "name": obj.paquete
                        }
                    }
                );
            }
            loadCombo(dataPaquete, 'selectPaquete', true, 'Seleccione Paquete');
            $('selectPaquete').selectpicker('refresh');
            loadCombo(dataPaquete, 'selectPaqueteC', true, 'Seleccione Paquete');
            $('selectPaqueteC').selectpicker('refresh');
        }
    });

    loadCombo([], 'selectCilindro', false);
    loadCombo([], 'selectCilindroC', false);
    $.ajax({
        type: "GET",
        url: getPath() + "api/productos/ListarCilindro?parametro=" + 4,
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            let cilindro = data.value;
            if (cilindro.length > 0) {
                dataCilindro = cilindro.map(
                    obj => {
                        return {
                            "id": obj.codigo_cilindro,
                            "name": obj.cilindro
                        }
                    }
                );
            }
            loadCombo(dataCilindro, 'selectCilindro', true, 'Seleccione Cilindro');
            $('selectCilindro').selectpicker('refresh');
            loadCombo(dataCilindro, 'selectCilindroC', true, 'Seleccione Cilindro');
            $('selectCilindroC').selectpicker('refresh');
        }
    });
    
}
function filtroCategoria(pCategoria) {    

    loadCombo([], 'selectTipo', false);
    $.ajax({
        type: "GET",
        url: getPath() + "api/productos/ListarTipo?codigoCategoria=" + pCategoria,
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            let tipos = data.value;
            if (tipos.length > 0) {
                dataTipo = tipos.map(
                    obj => {
                        return {
                            "id": obj.codigo_tipo,
                            "name": obj.tipo
                        }
                    }
                );
            }
            loadCombo(dataTipo, 'selectTipo', true, 'Seleccione Tipo');
            $('selectTipo').selectpicker('refresh');
        }
    });

    loadCombo([], 'selectMarca', false);
    $.ajax({
        type: "GET",
        url: getPath() + "api/productos/ListarMarca?codigoCategoria=" + pCategoria,
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            let marcas = data.value;
            if (marcas.length > 0) {
                dataMarca = marcas.map(
                    obj => {
                        return {
                            "id": obj.codigo_marca,
                            "name": obj.marca
                        }
                    }
                );
            }
            loadCombo(dataMarca, 'selectMarca', true, 'Seleccione Marca');
            $('selectMarca').selectpicker('refresh');
        }
    });

    if (pCategoria == 'C00001') {
        let tipos = dataTipo.filter(item => item.id == 'TP000001');
        loadCombo(tipos, 'selectTipo', false);

        $('#selectMarca').removeAttr('disabled');
        $('#selectMarca').val('M00001').change();
        //let marcas = dataMarca.filter(item => item.id == 'M00001');
        //loadCombo(marcas, 'selectMarca', false);
    }
}
function cargarMarca() {
    loadCombo(dataCategoria, 'selectCategoriaM', true, 'Seleccione Categoria');
    $('selectCategoriaM').selectpicker('refresh');
    $('#txtNombreMarca').val('');
}
function cargarTipo() {
    loadCombo(dataCategoria, 'selectCategoriaT', true, 'Seleccione Categoria');
    $('selectCategoriaT').selectpicker('refresh');
    $('#txtNombreTipo').val('');
}
function seleccionarBalde() {
    $('#selectBalde').removeAttr('disabled');
    $('#selectEnvase').prop('disabled', 'disabled');
    $('#selectPaquete').prop('disabled', 'disabled');
    $('#selectCilindro').prop('disabled', 'disabled');
    $("#chkBalde").prop("checked", true);
    $("#chkEnvase").prop("checked", false);
    $("#chkPaquete").prop("checked", false);
    $("#chkCilindro").prop("checked", false);
}
function seleccionarEnvase() {
    $('#selectBalde').prop('disabled', 'disabled');
    $('#selectEnvase').removeAttr('disabled');
    $('#selectPaquete').prop('disabled', 'disabled');
    $('#selectCilindro').prop('disabled', 'disabled');
    $("#chkBalde").prop("checked", false);
    $("#chkEnvase").prop("checked", true);
    $("#chkPaquete").prop("checked", false);
    $("#chkCilindro").prop("checked", false);
}
function seleccionarPaquete() {
    $('#selectBalde').prop('disabled', 'disabled');
    $('#selectEnvase').prop('disabled', 'disabled');
    $('#selectPaquete').removeAttr('disabled');
    $('#selectCilindro').prop('disabled', 'disabled');
    $("#chkBalde").prop("checked", false);
    $("#chkEnvase").prop("checked", false);
    $("#chkPaquete").prop("checked", true);
    $("#chkCilindro").prop("checked", false);
}
function seleccionarCilindro() {
    $('#selectBalde').prop('disabled', 'disabled');
    $('#selectEnvase').prop('disabled', 'disabled');
    $('#selectPaquete').prop('disabled', 'disabled');
    $('#selectCilindro').removeAttr('disabled');
    $("#chkBalde").prop("checked", false);
    $("#chkEnvase").prop("checked", false);
    $("#chkPaquete").prop("checked", false);
    $("#chkCilindro").prop("checked", true);
}
function seleccionarBaldeC() {
    $('#selectBaldeC').removeAttr('disabled');
    $('#selectEnvaseC').prop('disabled', 'disabled');
    $('#selectPaqueteC').prop('disabled', 'disabled');
    $('#selectCilindroC').prop('disabled', 'disabled');
    $("#chkBaldeC").prop("checked", true);
    $("#chkEnvaseC").prop("checked", false);
    $("#chkPaqueteC").prop("checked", false);
    $("#chkCilindroC").prop("checked", false);
}
function seleccionarEnvaseC() {
    $('#selectBaldeC').prop('disabled', 'disabled');
    $('#selectEnvaseC').removeAttr('disabled');
    $('#selectPaqueteC').prop('disabled', 'disabled');
    $('#selectCilindroC').prop('disabled', 'disabled');
    $("#chkBaldeC").prop("checked", false);
    $("#chkEnvaseC").prop("checked", true);
    $("#chkPaqueteC").prop("checked", false);
    $("#chkCilindroC").prop("checked", false);
}
function seleccionarPaqueteC() {
    $('#selectBaldeC').prop('disabled', 'disabled');
    $('#selectEnvaseC').prop('disabled', 'disabled');
    $('#selectPaqueteC').removeAttr('disabled');
    $('#selectCilindroC').prop('disabled', 'disabled');
    $("#chkBaldeC").prop("checked", false);
    $("#chkEnvaseC").prop("checked", false);
    $("#chkPaqueteC").prop("checked", true);
    $("#chkCilindroC").prop("checked", false);
}
function seleccionarCilindroC() {
    $('#selectBaldeC').prop('disabled', 'disabled');
    $('#selectEnvaseC').prop('disabled', 'disabled');
    $('#selectPaqueteC').prop('disabled', 'disabled');
    $('#selectCilindroC').removeAttr('disabled');
    $("#chkBaldeC").prop("checked", false);
    $("#chkEnvaseC").prop("checked", false);
    $("#chkPaqueteC").prop("checked", false);
    $("#chkCilindroC").prop("checked", true);
}
function formarNombreProducto() {
    if ($('#chkFormarNombre').is(":checked") && $("#selectCategoria").val() != ''
        && $("#selectTipo").val() != '' && $(iptModelo).val() != '') {
        let nombreProducto = dataTipo.filter(item => item.id == $("#selectTipo").val());
        $(iptNombreProducto).val(nombreProducto[0].name);
    }
}
function guardarMarca() {
    if ($("#selectCategoriaM").val() == '') {
        swal('Datos incompletos', 'Seleccione la categoria', "warning");
        return;
    }
    if ($(txtNombreMarca).val() == '') {
        swal('Datos incompletos', 'Falta ingresar el nombre', "warning");
        return;
    }

    let cod_categoria = $("#selectCategoriaM").val();
    let nombre = $("#txtNombreMarca").val().trim();

    $.ajax({
        type: "PUT",
        url: getPath() + "api/productos/GuardarMarca?categoriaID=" + cod_categoria + "&nombre=" + nombre,
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
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
            swal("Oops!", "No se pudo guardar el cambio.", "error");
        },
        complete: function () {
            $("#modalMarca").modal("hide");
        }
    });
}
function guardarTipo() {
    if ($("#selectCategoriaT").val() == '') {
        swal('Datos incompletos', 'Seleccione la categoria', "warning");
        return;
    }
    if ($(txtNombreTipo).val() == '') {
        swal('Datos incompletos', 'Falta ingresar la descripcion', "warning");
        return;
    }

    let cod_categoria = $("#selectCategoriaT").val();
    let descripcion = $("#txtNombreTipo").val().trim();

    $.ajax({
        type: "PUT",
        url: getPath() + "api/productos/GuardarTipo?categoriaID=" + cod_categoria + "&descripcion=" + descripcion,
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
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
            swal("Oops!", "No se pudo guardar el cambio.", "error");
        },
        complete: function () {
            $("#modalTipo").modal("hide");
        }
    });
}
function guardarProductos() {
    if (!obligatorioProductos()) {
        return
    }

    let cod_producto = $("#txtCodigoProducto").val();
    let nombre = $("#iptNombreProducto").val();
    let tipo_moneda = $("#selectMoneda").val();
    let cod_categoria = $("#selectCategoria").val();
    let codigo_tipo = $("#selectTipo").val();
    let modelo = $("#iptModelo").val();
    let codigo_marca = $("#selectMarca").val();
    let stock = $("#iptStock").val();
    let precio_venta = $("#iptPrecioVenta").val();
    let precio_compra = $("#iptPrecioCompra").val();
    let codigo_UM = $('#chkBalde').is(":checked") ? $("#selectBalde").val() : $("#chkEnvase").is(":checked") ? $("#selectEnvase").val() : $('#chkPaquete').is(":checked") ? $("#selectPaquete").val() : $("#selectCilindro").val();
    let codigo_UMC = $('#chkBalde').is(":checked") ? $("#selectBaldeC").val() : $("#chkEnvase").is(":checked") ? $("#selectEnvaseC").val() : $('#chkPaquete').is(":checked") ? $("#selectPaqueteC").val() : $("#selectCilindroC").val();

    var objProducto = {
        "cod_producto": cod_producto,
        "nombre": nombre,
        "tipo_moneda": tipo_moneda,
        "cod_categoria": cod_categoria,        
        "codigo_tipo": codigo_tipo,
        "modelo": modelo,
        "codigo_marca": codigo_marca,        
        "stock": stock,
        "precio_venta": precio_venta,
        "precio_compra": precio_compra,        
        "codigo_UM": codigo_UM,
        "codigo_UMC": codigo_UMC
    }

    $.ajax({
        type: 'POST',
        url: getPath() + "api/productos/GuardarProductos",
        async: false,
        headers: {
            'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0'
        },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(objProducto),
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
            $("#modalProducto").modal("hide");
            listarProductos();
            $.LoadingOverlay("hide");
        }
    })
}
function obligatorioProductos() {
    if ($("#selectMoneda").val() == '00') {
        swal('Datos incompletos', 'Seleccione la moneda', "warning");
        return false;
    }
    if ($("#selectCategoria").val() == '') {
        swal('Datos incompletos', 'Seleccione la categoria', "warning");
        return false;
    }
    if ($("#selectTipo").val() == '') {
        swal('Datos incompletos', 'Seleccione el tipo', "warning");
        return false;
    }
    if ($(iptModelo).val() == '') {
        swal('Datos incompletos', 'Falta ingresar el modelo', "warning");
        return false;
    }
    if ($("#selectMarca").val() == '') {
        swal('Datos incompletos', 'Seleccione la marca', "warning");
        return false;
    }
    if ($(iptNombreProducto).val() == '') {
        swal('Datos incompletos', 'Falta ingresar el nombre', "warning");
        return false;
    }
    if ($(iptStock).val() == 0) {
        swal('Datos incompletos', 'El stock debe ser mayor a 0', "warning");
        return false;
    }
    if ($(iptPrecioCompra).val() == 0) {
        swal('Datos incompletos', 'El precio de compra debe ser mayor a 0', "warning");
        return false;
    }
    if ($(iptPrecioVenta).val() == 0) {
        swal('Datos incompletos', 'El precio de venta debe ser mayor a 0', "warning");
        return false;
    }
    if ($('#chkBalde').is(":checked")) {
        if ($("#selectBalde").val() == '') {
            swal('Datos incompletos', 'Seleccione el balde de venta', "warning");
            return false;
        }
    }
    if ($('#chkEnvase').is(":checked")) {
        if ($("#selectEnvase").val() == '') {
            swal('Datos incompletos', 'Seleccione el envase de venta', "warning");
            return false;
        }
    }
    if ($('#chkPaquete').is(":checked")) {
        if ($("#selectPaquete").val() == '') {
            swal('Datos incompletos', 'Seleccione el paquete de venta', "warning");
            return false;
        }
    }
    if ($('#chkCilindro').is(":checked")) {
        if ($("#selectCilindro").val() == '') {
            swal('Datos incompletos', 'Seleccione el cilindro de venta', "warning");
            return false;
        }
    }
    if ($('#chkBaldeC').is(":checked")) {
        if ($("#selectBaldeC").val() == '') {
            swal('Datos incompletos', 'Seleccione el balde de compra', "warning");
            return false;
        }
    }
    if ($('#chkEnvaseC').is(":checked")) {
        if ($("#selectEnvaseC").val() == '') {
            swal('Datos incompletos', 'Seleccione el envase de compra', "warning");
            return false;
        }
    }
    if ($('#chkPaqueteC').is(":checked")) {
        if ($("#selectPaqueteC").val() == '') {
            swal('Datos incompletos', 'Seleccione el paquete de compra', "warning");
            return false;
        }
    }
    if ($('#chkCilindroC').is(":checked")) {
        if ($("#selectCilindroC").val() == '') {
            swal('Datos incompletos', 'Seleccione el cilindro de compra', "warning");
            return false;
        }
    }
    if ($('#chkBalde').prop('checked') == false && $('#chkEnvase').prop('checked') == false &&
        $('#chkPaquete').prop('checked') == false && $('#chkCilindro').prop('checked') == false) {
        swal('Datos incompletos', 'Marque la unidad de medida de venta del producto', "warning");
        return false;
    }
    if ($('#chkBaldeC').prop('checked') == false && $('#chkEnvaseC').prop('checked') == false &&
        $('#chkPaqueteC').prop('checked') == false && $('#chkCilindroC').prop('checked') == false) {
        swal('Datos incompletos', 'Marque la unidad de medida de compra del producto', "warning");
        return false;
    }
    if (parseFloat($(iptPrecioCompra).val()) >= parseFloat($(iptPrecioVenta).val())) {
        swal('Validacion de precios', 'El precio de venta debe ser mayor al precio de compra', "warning");
        return false;
    }
    return true;
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
        content += "<td style='max-width:35px;min-width:35px;text-align:center'><button style='font-size:8px' class='row-remove btn btn-sm btn-danger'><i class='fas fa-minus'></i></button></td>"
        content += "<td class='codigo' style = 'max-width:50px;min-width:50px;text-align:center'>" + codigo + "</td >"
        content += "<td class='descripcion' style='max-width:300px;min-width:300px;text-align:center'>" + producto + "</td>"
        content += "<td class='stock' style='max-width:50px;min-width:50px;' hidden>" + stock + "</td>"
        content += "<td style='max-width:60px;min-width:60px'><input type='number' style='font-size:10px;text-align:right' class='cantidad form-control form-control-sm' /></td>"
        content += "<td class='monto' style='max-width:50px;min-width:50px;' hidden>" + precio + "</td>"
        content += "<td style='max-width:60px;min-width:60px'><input type='number' style='font-size:10px;text-align:right' class='precio form-control form-control-sm' value='" + precio + "'/></td>"
        content += "<td class='total' style='max-width:50px;min-width:50px;text-align:center'>" + 0 + "</td>"

        $("table#tblDetalle tbody").append(content);
    }
}
function agregarManual() {
    var manual = "";
    manual += '<tr>';
    manual += "<td style='max-width:35px;min-width:35px;text-align:center'><button style='font-size:8px' class='row-remove btn btn-sm btn-danger'><i class='fas fa-minus'></i></button></td>"
    manual += "<td class='codigo' style='max-width:50px;min-width:50px;text-align:center'>" + 'P00000' + "</td >"
    manual += "<td class='descripcion' style='max-width:300px;min-width:300px;'><input type='text' style='font-size:11px;text-align:center' class='descripcion form-control form-control-sm'/></td>"
    manual += "<td class='stock' style='max-width:50px;min-width:50px;' hidden>" + 100 + "</td>"
    manual += "<td style='max-width:60px;min-width:60px'><input type='number' style='font-size:10px;text-align:right' class='cantidad form-control form-control-sm'/></td>"
    manual += "<td class='monto' style='max-width:50px;min-width:50px;' hidden>" + 0 + "</td>"
    manual += "<td style='max-width:60px;min-width:60px'><input type='number' style='font-size:10px;text-align:right' class='precio form-control form-control-sm'/></td>"
    manual += "<td class='total' style='max-width:50px;min-width:50px;text-align:center'>" + 0 + "</td>"

    $("table#tblDetalle tbody").append(manual);
}
function calcularTotal() {
    let subTotal = 0;
    let igv = 0;
    let montoTotal = 0;
    $('table#tblDetalle tbody tr td.total').each(function () {
        montoTotal += parseFloat($(this).text());
    });

    subTotal = montoTotal / 1.18;
    igv = montoTotal - subTotal;
    $("#iptSubTotal").val(subTotal.toFixed(2));
    $("#iptIGV").val(igv.toFixed(2));
    $("#iptTotal").val(montoTotal.toFixed(2));
}

function guardarVenta() {
    if (!obligatorioVentas()) {
        return
    }
    
    let codigo_venta = '';
    let sub_total = $("#iptSubTotal").val();
    let igv = $("#iptIGV").val();
    let total = $("#iptTotal").val();
    let codigo_cliente = $("#selectCliente").val();
    let codigo_usuario = 'U0000001';
    let placa = $("#iptPlaca").val();
    let codigo_tc = localStorage.getItem("tpID");
    let condicion = $('#chkContado').is(":checked") ? true : false;

    var objVenta = {
        "codigo_venta": codigo_venta,
        "sub_total": sub_total,
        "igv": igv,
        "total": total,
        "codigo_cliente": codigo_cliente,
        "codigo_usuario": codigo_usuario,
        "placa": placa,
        "codigo_tc": codigo_tc,
        "condicion": condicion,
        "detalleVenta": []
    }

    $("table#tblDetalle>tbody>tr").each(function (iTr, tr) {
        var item = {}

        let cod_prod = $(tr).find('td.codigo').text();
        let cantidad = $(tr).find('input.cantidad').val();
        let pre_venta = $(tr).find('td.total').text();
        let subtotal = pre_venta / 1.18;
        let igv = pre_venta - subtotal;        
        let monto_real = $(tr).find('td.monto').text();

        item.cod_prod = cod_prod;
        item.cantidad = cantidad;
        item.pre_venta = pre_venta;
        item.igv = igv;
        item.subtotal = subtotal;
        item.monto_real = monto_real;

        objVenta.detalleVenta.push(item);
    })

    $.ajax({
        type: 'POST',
        url: getPath() + "api/ventas/GuardarVenta",
        async: false,
        headers: {
            'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0'
        },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(objVenta),
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
            limpiarVenta();
            listarProductos();
            $.LoadingOverlay("hide");
        }
    })
}
function obligatorioVentas() {
    if ($("#selectCliente").val() == '') {
        swal('Faltan datos', 'Seleccione el cliente', "warning");
        return false;
    }    
    if ($("#iptTotal").val() == 0.00) {
        swal('Importe total no admitido', 'El monto total debe ser mayor a 0', "warning");
        return false;
    }
    let table = $('#tblDetalle').DataTable();
    if (!table.data().count()) {
        swal('Sin detalle de venta', 'No hay ningun producto en el carrito de ventas', "warning");
        return false;
    }
    let total = 0;
    $('table#tblDetalle tbody td.total').each(function () {
        total += parseFloat($(this).text());
    });
    if ($("#iptTotal").val() != total) {
        swal('Incosistencia de venta', 'No coincide el precio total con el detalle', "warning");
        return false;
    }

    return true;
}
function limpiarVenta() {
    $("#iptDocumento").val('');
    listarClientes();
    $("#iptPlaca").val('');
    $("table#tblDetalle").DataTable().destroy();
    $("table#tblDetalle>tbody").empty();
    $('#chkCredito').removeAttr('checked');
    $('#chkContado').attr('checked', true);    
    $(iptSubTotal).val(0.00);
    $(iptIGV).val(0.00);
    $(iptTotal).val(0.00);
}