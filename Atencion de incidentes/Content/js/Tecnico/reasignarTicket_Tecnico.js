$(".btnreasignar").click(function (eve) {
    $("#modal-content3").load("/Tecnico/Reasignar/" + $(this).data("id"));
});
