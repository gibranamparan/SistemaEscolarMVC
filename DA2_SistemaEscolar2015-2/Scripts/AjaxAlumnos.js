$().ready(function () {

    function rellenarIndexAlumnos() {
        var strBuscado = $("input[name='strBuscado']").val();
        $.ajax({
            url: "/Alumno/AjaxIndex", //Accion a ejecutar en el server
            contentType: "application/html; charset=utf-8",
            type: "GET",
            dataType: "html",
            data: { strBuscado: strBuscado } //Dato enviado al server
        }).success(function (result) {
            var tablaAlumnos = $("#tablaAlumnos tbody");
            tablaAlumnos.html("");
            var conjutoAlumnos = JSON.parse(result);

            for (var indice in conjutoAlumnos) {
                var alumno = conjutoAlumnos[indice];
                tablaAlumnos.append("<tr>"+
                    "<td>" + alumno.grupo + "</td>" + //Nombre grupo
                    "<td>" + alumno.nombre + "</td>" + //nombre
                    "<td>" + alumno.apellidoP + "</td>" + //apellidoP
                    "<td>" + alumno.apellidoM + "</td>" + //apellidoM
                    "<td>" + alumno.fechaNac + "</td>" + //fechaNac
                    "<td>"+
                    "<a id='enlaceDetalles' data-toggle='modal' data-target='#modalDetalles' nomatricula='"+alumno.noMatricula+"'>Detalles</a> |"+
                    "<a id='enlaceBorrar' data-toggle='modal' data-target='#modalBorrar' nomatricula='"+alumno.noMatricula+"'>Borrar</a> |"+
                    "<a id='enlaceEditar' data-toggle='modal' data-target='#modalEditar' nomatricula='"+alumno.noMatricula+"'>Editar</a> |"+
                    "</td>" +
                    "</tr>")
            }

        }).error(function (xhr, status) {

        })
    }


    //Abrir pantalla de Editar y mostrar datos de alumno
    $("a#enlaceEditar").click(function () {
        //Se obtiene el numero de matricula a consultar
        var enlaceClickeado = $(this);
        var noMat = enlaceClickeado.attr("nomatricula");

        //Definir la transaccione AJAX al server
        $.ajax({
            url: "/Alumno/AjaxEdit", //Accion a ejecutar en el server
            contentType: "application/html; charset=utf-8",
            type: "GET",
            dataType: "html",
            data: { noMatricula: noMat } //Dato enviado al server
        }).success(function (result) { //result = {mensaje, status}
            //Se obtiene la respuesta del server en forma de objeto
            var alumno = JSON.parse(result);

            //Con la información recibida, se rellena el formulario
            $("#modalEditar #noMatricula").val(alumno.noMatricula);
            $("#modalEditar #nombre").val(alumno.nombre);
            $("#modalEditar #apellidoP").val(alumno.apellidoP);
            $("#modalEditar #apellidoM").val(alumno.apellidoM);
            fechaRecibida = new Date(alumno.fechaNac);
            //$("#modalEditar #fechaNac").val(fechaRecibida);
            //document.getElementById("fechaNac").valueAsDate = fechaRecibida;
            $("#modalEditar #fechaNac")[0].valueAsDate = fechaRecibida;
            $("#modalEditar #grupoID").val(alumno.grupoID);

        }).error(function (xhr, status) {
            /*Notificar al usuario de un error de comunicacion
            con el server*/
            $("#mensaje").removeClass('alert-danger alert-info');
            $("#mensaje").html("Ha ocurrido un error: " + status).addClass('alert-danger');
            $("#mensaje").fadeIn(500).delay(2000).fadeOut(500);
        })
    })

    /*Confirmar edicion de cambios en registro de alumnos*/
    $("#btnEditar").click(function () {
        alumnoModificado = {
            noMatricula: $("#modalEditar #noMatricula").val(),
            nombre: $("#modalEditar #nombre").val(),
            apellidoP: $("#modalEditar #apellidoP").val(),
            apellidoM: $("#modalEditar #apellidoM").val(),
            fechaNac: $("#modalEditar #fechaNac").val(),
            grupoID: $("#modalEditar #grupoID").val(),
        };

        $.ajax({
            url: '/Alumno/AjaxEdit',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(alumnoModificado),
            type: 'post',
        }).success(function (result) {
            rellenarIndexAlumnos();
        }).error(function (xhr, status) {
            alert("No se encontro el servidor,"+
                " verifique si se encuentra conectado a internet.");

        })
        $("#modalEditar").modal("toggle");
    })


})