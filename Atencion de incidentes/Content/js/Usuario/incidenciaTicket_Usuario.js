$(".btnincidencia").click(function (eve2) {
    $("#incidencia").load("/Usuario/Incidencia/" + $(this).data("id"));
});