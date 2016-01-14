$().ready(function () {
    //Muestra enlaces para realizar el CRUD sobre el elemento seleccionado en la tabla
    $("input#seleccion").click(function () {
        $("#opcionesCRUD").fadeIn(500);
        var id=$(this).val();

        $(".lnkCRUD").each(function () {
            var url = $(this).attr("url");
            $(this).attr("href", url + id);
        })
    })




})
