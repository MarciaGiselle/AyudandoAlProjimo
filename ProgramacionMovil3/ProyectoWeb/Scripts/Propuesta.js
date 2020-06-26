$(document).ready(function () {
        $('.datepicker').datepicker({
            format: 'dd/mm/yyyy',
            autoclose: true,
            language: "es",
            startDate: "tomorrow"
        });
});

$(function () { $.validator.methods.date = function (value, element) { return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid(); }});
    (function () {
        'use strict';
        window.addEventListener('load', function () {
            // Fetch all the forms we want to apply custom Bootstrap validation styles to
            var forms = document.getElementsByClassName('needs-validation');
// Loop over them and prevent submission
            var validation = Array.prototype.filter.call(forms, function (form) {
        form.addEventListener('submit', function (event) {
            if (form.checkValidity() === false) {
                event.preventDefault();
                event.stopPropagation();
            }
            form.classList.add('was-validated');
        }, false);
});
}, false);
})();

$(function () {
    if ($("#otraProfesion").val() != undefined && $("#otraProfesion").val() != "" ){
        $("#detalle").removeClass("d-none");
        $("#otraProfesion").removeClass("d-none").addClass("d-flex");
        $("#otraProfesion").prop("required");
    };
});

$(function () {
    $("#id_profesion").change(function () {
        if ($(this).val() === "10") {
            $("#otraProfesion").removeClass("d-none").addClass("d-flex");
            $("#detalle").removeClass("d-none");
            $("#otraProfesion").prop("required", true);

        } else {
            $("#otraProfesion").removeClass("d-flex").addClass("d-none");
            $("#detalle").addClass("d-none");
            $(".otraProfesion").attr("disabled", true);
            $("#otraProfesion").val(null);
            $("#otraProfesion").prop("required", false);

        }
    });
 });


var counter;
var bandera = false;
var counterPlaceholder;

function nuevoItem(count) {

        if (!bandera) {
            counter = count;
            counterPlaceholder = count + 1;
            bandera = true;
        }
        var newdiv = document.createElement('div');
        var seconddiv = document.createElement('div');

        newdiv.id = "divItems[" + counter + "]";
        newdiv.className = ("col-md-5 mb-4 mt-1");
        seconddiv.id = "divDes[" + counter + "]";
        seconddiv.className = ("col-md-5 mb-4 mt-1");
        var button = document.createElement('div');


        newdiv.innerHTML =
            "<input type='text' class='form-control bg-transparent pl-4' placeholder='Nombre item " + counterPlaceholder + "' name='PropuestasDonacionesInsumos[" + counter + "].Nombre' required maxlength='50'></div>";

        seconddiv.innerHTML =
            "<input type='number' class='form-control bg-transparent pl-4' placeholder='Cantidad item " + counterPlaceholder + "' name='PropuestasDonacionesInsumos[" + counter + "].Descripcion' required min='1'></div>";
        "</div><br>";


        count--;
        document.getElementById("itemInsumo[" + count + "]").appendChild(newdiv);
        document.getElementById("itemInsumo[" + count + "]").appendChild(seconddiv);

        /* var identificador = "divItems[" + counter + "]";
         button.innerHTML = "<input type='button' value='-' onclick='(remove( " + identificador + " ) )'>";
         document.getElementById('itemInsumo').appendChild(button);*/
        ++counter;
        ++counterPlaceholder;
};
   

function increment() {
    $('#indice').val(function (i, oldval) {
        return ++oldval;
    });
}

var count;
var banderaCrear = false;
var counterPlaceholderCrear;


function nuevoItemCrear() {

    if (!banderaCrear) {
        count = 1;
        banderaCrear = true;
        counterPlaceholderCrear = count + 1;
    }
    var newdiv = document.createElement('div');
    var seconddiv = document.createElement('div');

    newdiv.id = "divItems[" + count + "]";
    newdiv.className = ("col-md-5 mb-3");
    seconddiv.id = "divDes[" + count + "]";
    seconddiv.className = ("col-md-5 mb-3");

    newdiv.innerHTML =
        "<input type='text' class='form-control  bg-transparent pl-4' placeholder='Nombre item " + counterPlaceholderCrear + "' name='PropuestasDonacionesInsumos[" + count + "].Nombre' required maxlength='50' /></div> ";
    seconddiv.innerHTML =
        "<input type='number' class='form-control   bg-transparent pl-4' placeholder='Cantidad item " + counterPlaceholderCrear + "' name='PropuestasDonacionesInsumos[" + count + "].Cantidad'  min='1' required /></div><br> ";


    

    document.getElementById('itemInsumo').appendChild(newdiv);
    document.getElementById('itemInsumo').appendChild(seconddiv);
  /* $("#remove").attr("onclick", "remove('divItems[" + count + "]')");*/
    ++counterPlaceholderCrear;
    ++count;
};

function remove(id) {
    debugger;
    $("#"+id+"").remove();
};
               
$(function () {
        var bandera = false;
        if (!bandera) {
                $("#propuestaMonetaria").show();
                $("#propuestaInsumos").attr("hidden", true);
                $("#propuestaTrabajo").attr("hidden", true);
                $("#tipoPropuesta").attr("value", 1);
               
                $(".monetaria").attr("disabled", false);
                $(".insumo").attr("disabled", true);
                $(".trabajo").attr("disabled", true);
                bandera = true;
            }

        $("#monetaria").click(function () {
            if ($(this).is(":checked")) {
                    $("#propuestaMonetaria").attr("hidden", false);
            $("#propuestaInsumos").attr("hidden", true);
            $("#propuestaTrabajo").attr("hidden", true);
            $("#tipoPropuesta").attr("value", 1);

            $(".monetaria").attr("disabled", false);
            $(".insumo").attr("disabled", true);
            $(".trabajo").attr("disabled", true);

        }


    });

        $("#insumo").click(function () {
            if ($(this).is(":checked")) {
                    $("#propuestaInsumos").attr("hidden", false);
                $("#propuestaMonetaria").attr("hidden", true);
                $("#propuestaTrabajo").attr("hidden", true);
                $("#tipoPropuesta").attr("value", 2);
                $(".monetaria").attr("disabled", true);
                $(".insumo").attr("disabled", false);
                $(".trabajo").attr("disabled", true);
            }
        });

        $("#trabajo").click(function () {
            if ($(this).is(":checked")) {
                    $("#propuestaTrabajo").attr("hidden", false);
                $("#propuestaMonetaria").attr("hidden", true);
                $("#propuestaInsumos").attr("hidden", true);
                $("#tipoPropuesta").attr("value", 3);
                $(".monetaria").attr("disabled", true);
                $(".insumo").attr("disabled", true);
                $(".trabajo").attr("disabled", false);
            } 

        });
    });
