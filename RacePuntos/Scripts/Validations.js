jQuery(function ($) {
    $("#contrasena").focusout(function () {
        Validar_Password($("#contrasena").val());
    });

    $("#submit_login").click(function () {
        if ($("#documento").val() != "" && $("#contrasena").val() != "") {
            $("#Login_Form").submit();
        } else {
            swal("Error", "Se debe diligenciar todos los campos.", "error");
        }
    });

    $("#guarda_usr").click(function () {
        $("#Create_User").submit();
    });

    if ($("#LisAquisicion").length > 0 || $("#LisUsuarios").length > 0) {
        $('#LisAquisicion, #LisUsuarios').DataTable({
            "language": {
                "sPaginationType": "full_numbers",
                "sProcessing": "",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "NingÃºn dato disponible en esta tabla",
                "sInfo": "Registros _START_ al _END_ de _TOTAL_",
                "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                "sInfoPostFix": "",
                "sSearch": "Buscar:",
                "oPaginate": {
                    "sFirst": "Primero",
                    "sLast": "Ãšltimo",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                },
            },
            "bLengthChange": false, //thought this line could hide the LengthMenu
            "bInfo": false,
            lengthMenu: [[5], [5]],
            destroy: true,
            "sDom": 'T<"clear">lfrtip',
            "tableTools": {
                "sSwfPath": '/Scripts/plugins/dataTables/copy_csv_xls_pdf.swf',
                "aButtons": [
                  {
                      "sExtends": "collection",
                      "sButtonText": "exportar",
                      "aButtons": [
                        {
                            "sExtends": "copy",
                            "sButtonText": "Copiar al portapapeles",
                            "bHeader": true
                        },
                        {
                            "sExtends": "xls",
                            "bHeader": true,
                            "sTitle": "Reporte Ticket Empresa"
                        },
                        {
                            "sExtends": "pdf",
                            "sPdfOrientation": "landscape",
                            "sTitle": "Reporte"
                        }
                      ]
                  }
                ]
            }
        });
    }

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

    if (!expreg.test(clave) && $("#contrasena").val().trim() != "") {
        alert("La clave ingresa no es valida, verifique.");
        $("#contrasena").val("");
        return false;
    }
}