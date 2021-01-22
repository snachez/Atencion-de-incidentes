$(".btncalificacion").click(function (eve2) {
    $("#modal4").load("/Administrador/MostrarCalificacion/" + $(this).data("id"));
});