var dataTipoCambio = [];
var _tpID = 0;
var _tipoCambio = 0;

$(function () {
    $("#modalTipoCambio").modal("hide");
    mostrarTipoCambio();
});
function mostrarTipoCambio() {
    $.ajax({
        type: "GET",
        url: "https://localhost:44380/api/home/ListarTipoCambio",
        async: false,
        headers: { 'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0' },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
			dataTipoCambio = data.value;
            $(txtTipoCambio).text(dataTipoCambio.tipoCambio.toFixed(2));
            _tpID = dataTipoCambio.tipoCambioID;
            _tipoCambio = dataTipoCambio.tipoCambio.toFixed(2);
            localStorage.setItem("tpID", _tpID);
            localStorage.setItem("tipoCambio", _tipoCambio);
        }
    });
}
function abrirModalTC() {
    $("#modalTipoCambio").modal("show");

}
function GuardarTipoCambio() {
    let tipoCambio = $("#txtNuevoTipoCambio").val();

    var objTipoCambio = {
        "tipoCambioID": 0,
        "tipoCambio": tipoCambio
    }

	$.ajax({
		type: 'POST',
		url: "https://localhost:44380/api/home/GuardarTipoCambio",
		async: false,
		headers: {
			'Cache-Control': 'no-cache, no-store, must-revalidate', 'Pragma': 'no-cache', 'Expires': '0'
		},
		contentType: 'application/json; charset=utf-8',
		dataType: 'json',
		data: JSON.stringify(objTipoCambio),
		success: function (data) {
            let bResult = false;
            if (data.hasOwnProperty('statusCode')) {
                switch (data.statusCode) {
                    case null: bResult = true; break;
                    case 9:
                        sawl("Oops", data.message, "error"); break;
                    default: sweet_alert_error("Oops", "Ocurrió un problema!", "error"); break;
                }
            }
            if (!bResult) return false;
            swal("Buen trabajo!", "El cambio se realizó exitosamente", "success");
		},
		error: function (datoError) {
			swal("Oops!", "No se pudo guardar el cambio.", "error");
		},
		complete: function () {
			mostrarTipoCambio();
			$("#txtNuevoTipoCambio").val('');
            $("#modalTipoCambio").modal("hide");
		}
	})
	    
}