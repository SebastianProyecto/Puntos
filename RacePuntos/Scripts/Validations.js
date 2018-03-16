jQuery(function ($) {
    $("#contrasena").focusout(function () {
        Validar_Password($("#contrasena").val());
    });
});

function validaNum(e) {
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }

    // Patron de entrada, en este caso solo acepta numeros
    patron = /[0-9]/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);
}

function Validar_Password(clave) {
    var expreg = /(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*/;

    if (!expreg.test(clave) && $("#CLAVE").val() != "") {
        alert("La clave ingresa no es valida, verifique.");
        $("#contrasena").val("");
        return false;
    }
}