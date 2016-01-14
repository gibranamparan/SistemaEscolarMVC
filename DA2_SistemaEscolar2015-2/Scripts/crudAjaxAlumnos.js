$().ready(function () {
    //Se asocian las llamadas a eventos ocurridos en la vista
    $('a[data-target="#modalDetalles"]').click(function () {
        var noMatricula = $(this).attr("noMatricula");
        $.ajax({
            url: "/Alumno/AjaxDetails",
            contentType: "application/html; charset=utf-8",
            type: "GET",
            dataType: "html",
        })
        .success(function (result) {
            alumno_llenarFormaDetalles(result);
        })
        .error(function (xhr, status) {
            notificarError(status);
        })
    });

    //Se definen las funciones para mostrar resultados de transacciones
    function alumno_llenarFormaDetalles(result) {
        $("input#noMatricula").val(result.noMatricula);
    }

    function notificarError(status) {
        alert(status)
    }
})