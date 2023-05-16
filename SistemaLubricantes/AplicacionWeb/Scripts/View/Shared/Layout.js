$(function () {

    $("#modalTipoCambio").modal("hide");

});

function abrirModalTC() {
    $("#modalTipoCambio").modal("show");

}

function GuardarTipoCambio() {
    let tipoCambio = $("#txtTipoCambio").val();
    $(lblTipoCambio).html(tipoCambio.bold());
    $("#modalTipoCambio").modal("hide");
}