
function Valorar(IdPropuesta, valoracion) {
        $.ajax({
            type: 'POST',
            url: '../Home/Valorar/',
            data: { IdPropuesta: IdPropuesta, valoracion: valoracion },
            success: function (Response) {
                window.location.reload(true);
            },
            error: function () {
                console.log("Error al Valorar")
            }
        });
}
