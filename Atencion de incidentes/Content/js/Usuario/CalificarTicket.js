$(".btncalificar").click(function (eve2) {
    $("#calificar").load("/Usuario/Calificar/" + $(this).data("id"));
});