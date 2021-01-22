$(".btnreactivar").click(function (eve2) {
    $("#modalreactivar").load("/Usuario/Reactivar/" + $(this).data("id"));
});