$("#btnincidencia").click(function (eve2) {
    $("#incidencia").load("/Administrador/Incidencia/" + $(this).data("id"));
});