function validar() {
    var fecha, mail, pass, pass2, expresion;
    fecha = document.getElementById("FechaNacimiento").value;
    mail = document.getElementById("Email").value;
    pass = document.getElementById("Password").value;
    pass2 = document.getElementById("Password2").value;
    expresion = /\w+@\w+\.+[a-z]/;

    calcularEdad(fecha);


    if (fecha === "" || mail === "" || pass === "" || pass2 === "") {
        alert("Debe llenar todos los campos");
        return false;
    }

    else if (pass != pass2) {
        alert("Las claves deben coincidir");
        return false;
    }
    //else if (!expresion.test(mail)) {
    //    alert("El correo no es valido");
    //    return false;
    //}

};


function calcularEdad(fecha) {
    var hoy = new Date();
    var cumpleanos = new Date(fecha);
    var edad = hoy.getFullYear() - cumpleanos.getFullYear();


    if (edad < 18) {
        alert("menores de 18 no estan permitidos");
        return false;
    }


};