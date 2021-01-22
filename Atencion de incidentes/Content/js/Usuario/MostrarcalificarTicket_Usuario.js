$(".btncalificacion").click(function (eve2) {
    $("#modal4").load("/Usuario/MostrarCalificacion/" + $(this).data("id"));
});