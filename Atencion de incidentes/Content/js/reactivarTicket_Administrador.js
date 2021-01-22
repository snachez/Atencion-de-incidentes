$(".btnreactivar").click(function (eve2) {
    $("#modalreactivar").load("/Administrador/Reactivar/" + $(this).data("id"));
});