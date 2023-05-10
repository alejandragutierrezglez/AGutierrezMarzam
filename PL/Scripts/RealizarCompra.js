function Modal() {
    $('#myModal').modal('show');
    RealizarCompra();
}

function ModalClose() {
    $('#myModal').modal('hide');
}


function RealizarCompra() {
    var compra = {
        Tarjeta: $('#txtTarjeta').val(''),
        CVV: $('#txtCVV').val(''),
        FechaExpiracion: $('#txtFechaExpiracion').val(''),
        Banco: $('#txtBanco').val(''),
        Calle: $('#txtCalle').val(''),
        NumeroExterior: $('#txtNumeroExterior').val(''),
        NumeroInterior: $('#txtNumeroInterior').val(''),
        Colonia: $('#txtColonia').val(''),
        Municipio: $('#txtMunicipio').val(''),
        Estado: $('#txtEstado').val(''),
        CodigoPostal: $('#txtCodigoPostal').val(''),
    }
}
function Validar() {
    $('#ModalOk').modal('show');
}