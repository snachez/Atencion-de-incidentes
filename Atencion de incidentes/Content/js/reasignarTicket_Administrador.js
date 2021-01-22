$(".btnreasignar").click(function (eve) {
    $("#modal-content3").load("/Administrador/Reasignar/" + $(this).data("id"));
});
