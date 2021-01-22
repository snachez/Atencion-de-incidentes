$(".btneditar").click(function (eve2) {
    $("#Editarticket").load("/Usuario/Editarticket/" + $(this).data("id"));
});