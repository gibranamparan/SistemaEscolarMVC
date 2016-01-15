$().ready(function () {

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
        //Se construye un objeto JSON que representa los valores nuevos para los atributos del alumno
        var alumnoModificado = {
            noMatricula: $("#noMatricula").val(),
            nombre: $("#nombre").val(),
            apellidoP: $("#apellidoP").val(),
            apellidoM: $("#apellidoM").val(),
            fechaNac: $("#fechaNac").val(),
            grupoID: $("#grupoID").val(),
        }
        
        $.ajax({
            url: "/Alumno/AjaxEdit", //Accion a ejecutar en el server
            contentType: "application/json; charset=utf-8",
            type: "POST", //Accion en modalidad POST
            dataType: "json",
            data: JSON.stringify(alumnoModificado) //Dato enviado al server
        }).success(function (result) {//Si la comunicacion con el server se llevo a cabo
            $("#mensaje").removeClass('alert-danger alert-info');

            claseMensaje = '';
            if (result.codigo == 0)
                claseMensaje = 'alert-danger';
            else
                claseMensaje = 'alert-info';

            $("#mensaje").addClass(claseMensaje);
            $("#mensaje").text(result.mensaje);
            $("#mensaje").fadeIn(500).delay(2000).fadeOut(500);

        }).error(function (xhr, status) {
            /*Notificar al usuario de un error de comunicacion
            con el server*/
            $("#mensaje").removeClass('alert-danger alert-info');
            $("#mensaje").addClass('alert-danger');
            $("#mensaje").text("Ha ocurrido un error: " + status + '.'+xhr);
            $("#mensaje").fadeIn(500).delay(2000).fadeOut(500);
        });

        //Se cierra el modal
        $("#modalEditar").modal('toggle');
    })


})