$("#btnincidencia").click(function (eve2) {
    $("#incidencia").load("/Tecnico/Incidencia/" + $(this).data("id"));
});