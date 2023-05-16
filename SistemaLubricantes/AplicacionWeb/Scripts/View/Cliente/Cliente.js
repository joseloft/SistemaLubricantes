var dataClientes = [];

$(function () {
    listarClientes();

    $("#tblClientes tbody").on("click", '.btn-editar', function () {
        var filaSeleccionada = $(this).closest("tr");
        var data = tabladata.row(filaSeleccionada).data();

        ModalCliente(data);

    })
});

function listarClientes() {

    var urlApi = "api/clientes/ListarClientes";
    $("table#tblClientes").DataTable().destroy();

    $.ajax({
        type: "GET",
        url: "https://localhost:44380/api/clientes/ListarClientes",
        //url: getPath() + urlApi,
        async: false,
        headers: {'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0'},
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            dataClientes = data.value;
            $("table#tblClientes>tbody").empty();

            var content = '';
            $.each(dataClientes, function (i, e) {
                content += '<tr style= "font-size:11px">';
                content += '<td style="text-align: center;max-width:60px; min-width:60px">';
                content += '<button type="button" style="font-size:11px" class="btn btn-sm btn-primary btn-editar"><i class="fas fa-edit fa-sm"></i></button>';
                content += '<button type="button" style="font-size:11px" class="btn btn-sm btn-danger btn-eliminar"><i class="fas fa-trash fa-sm"></i></button>';
                content += '</td>';
                content += '<td style="max-width:150px; min-width:150px">' + e.nombres + '</td >';
                content += '<td style="max-width:100px; min-width:100px">' + e.apellidos + '</td >';
                content += '<td style="max-width:150px; min-width:150px">' + e.direccion + '</td >';
                content += '<td style="max-width:110px; min-width:110px">' + e.distrito + '</td >';
                content += '<td style="max-width:50px; min-width:50px">' + e.celular + '</td >';
                content += '<td style="max-width:120px; min-width:120px">' + e.correo + '</td >';
                content += '<td style="max-width:70px; min-width:70px">' + e.ruc + '</td >';
                content += '</tr>';
            })

            $("table#tblClientes tbody").empty().append(content);
            table = $('table#tblClientes').DataTable({
                "scrollY": "400px",
                "scrollX": true,
                scrollCollapse: true,
                orderCellsTop: true,
                fixedHeader: true,
                searching: true,
                "ordering": true,
                info: true,
                "searchable": true,
                "bPaginate": true,
                "paging": true,
                "language": {
                    "url": "https://cdn.datatables.net/plug-ins/1.11.3/i18n/es_es.json"
                }
            });
            
        }
    });
}

function ModalCliente(json) {
    $("#txtid").val("0");
    $("#txtnombres").val("");
    $("#txtapellidos").val("");
    $("#txtcorreo").val("");
    $("#txtdireccion").val("");
    $("#txtdistrito").val("");
    $("#txttelefono").val("");
    $("#txtcelular").val("");
    $("#txtdni").val("");
    $("#txtruc").val("");

    $("#MensajeError").hide();

    if (json != null) {
        $("#txtid").val(json.cod_cliente);
        $("#txtnombres").val(json.nombres);
        $("#txtapellidos").val(json.apellidos);
        $("#txtcorreo").val(json.correo);
        $("#txtdireccion").val(json.direccion);
        $("#txtdistrito").val(json.Distrito);
        $("#txttelefono").val(json.telefono);
        $("#txtcelular").val(json.celular);
        $("#txtdni").val(json.dni);
        $("#txtruc").val(json.ruc)
    }

    $("#FormModal").modal("show");
}

function GuardarCliente() {

    var cliente =
    {
        cod_cliente: $("#txtid").val(),
        nombres: $("#txtnombres").val(),
        apellidos: $("#txtapellidos").val(),
        correo: $("#txtcorreo").val(),
        direccion: $("#txtdireccion").val(),
        Distrito: $("#txtdistrito").val(),
        telefono: $("#txttelefono").val(),
        celular: $("#txtcelular").val(),
        dni: $("#txtdni").val(),
        ruc: $("#txtruc").val()
    }

    jQuery.ajax({
        url: '@Url.Action("CrearEditarCliente", "Cliente")',
        type: "POST",
        data: JSON.stringify({ objeto: cliente }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (cliente.cod_cliente == 0) // cliente nuevo
            {
                if (data.resultado != 0) {
                    cliente.cod_cliente = data.resultado;
                    tabladata.row.add(cliente).draw(false);
                    $("#FormModal").modal("hide");
                }
                else {
                    $("#MensajeError").text(data.mensaje);
                    $("#MensajeError").show();
                }

            }

        },
    })
}