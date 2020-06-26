 function activarCasilla(numero) {

            if ($("#check" + numero).is(":checked")) {
                $('#' + numero).prop('disabled', false);

            } else {
                $('#' + numero).prop('disabled', true);
            }

};

$(document).ready(function () {
    var id = $("#miReferencia").val();
    $.ajax({
        url: 'https://localhost:44323/api/Donaciones/' + id + '',
        success: function (respuesta) {
            let listamen = respuesta;
            let template = "";
            listamen.forEach(donaciones => {
                template += `
                  <tr>
                    <td>${donaciones.Fecha}</td>
                    <td>${donaciones.Nombre}</td>
                    <td>${donaciones.TipoDonacion}</td>
                    <td>${donaciones.Estado}</td>
                    <td>${donaciones.TotalRecaudado}</td>
                    <td>${donaciones.MiDonacion}</td>
                    <td><a href="/Propuesta/Detalle/${donaciones.IdPropuesta}" class="btn btn-info">ir al Detalle</a></td>                       
                    </tr>`

            });
            console.log(respuesta);
            $('#pintame').html(template);
        }
    });
    $("#FormI").submit(function () {
        if ($(".checkboxD:checked").length == 0) {
            $("#TextAlMenos").text("Debe Selecionar Un elemento")
            return false;
        } else {
            return true;

        }
    });

   

    

});