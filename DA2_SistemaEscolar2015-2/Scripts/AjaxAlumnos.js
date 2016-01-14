$().ready(function () {

    $("a#enlaceDetalles").click(function () {
        //Se obtiene el numero de matricula a consultar
        var enlaceClickeado = $(this);
        var noMat = enlaceClickeado.attr("nomatricula");

        //Definir la transaccione AJAX al server
        $.ajax({
            url: "/Alumno/AjaxEdit",
            contentType: "application/html; charset=utf-8",
            type: "GET",
            dataType: "html",
            data: { noMatricula: noMat }
        }).success(function (result) {
            alert(result);

        }).error(function () {
            /*Notificar al usuario de un error de comunicacion
            con el server*/

        })
    })

    


})