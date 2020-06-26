
    $(document).ready(function () {
        $('#datepicker').datepicker();

});

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
        $("#id_profesion").change(function () {
            if ($(this).val() === "otros") {
                $("#id_otros").removeClass("d-none").addClass("d-flex");
                $("#detalle").removeClass("d-none").addClass("d-flex");
            } else {
                $("#id_otros").removeClass("d-flex").addClass("d-none");
                $("#detalle").removeClass("d-flex").addClass("d-none");
            }
        });
});

var counter;
var bandera = false;

    function nuevoItem() {
        
        if (!bandera) {
        counter = 1;
    bandera = true;
 }
 var newdiv = document.createElement('div');
 var seconddiv = document.createElement('div');

 newdiv.id = "divItems["+counter+"]";
 newdiv.className = ("col-md-5 mb-3");
 seconddiv.id = "divDes["+counter+"]";
 seconddiv.className = ("col-md-5 mb-3");


 newdiv.innerHTML =
            "<br><input type='text' class='form-control bg-transparent pl-4' name='PropuestasDonacionesInsumos["+counter+"].Nombre'></div>"+
            "<input type='button' value='-' onclick='remove(divItems["+counter+"]);' > ";
        seconddiv.innerHTML=
            "<br><input type='text' class='form-control bg-transparent pl-4' name='PropuestasDonacionesInsumos["+counter+"].Descripcion'></div>";
            "</div><br>"


        ++counter;
        document.getElementById('itemInsumo').appendChild(newdiv);
        document.getElementById('itemInsumo').appendChild(seconddiv);
    };

    function remove(id) {
        var elem = document.getElementById(id);
                return elem.parentNode.removeChild(elem);
            };
        
    $(function () {
        var bandera = false;
        if (!bandera) {
                $("#propuestaMonetaria").show();
                $("#propuestaInsumos").attr("hidden", true);
                $("#propuestaTrabajo").attr("hidden", true);
                $("#tipoPropuesta").empty();
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
