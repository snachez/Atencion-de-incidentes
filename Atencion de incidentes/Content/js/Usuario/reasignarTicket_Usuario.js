$(".btnreasignar").click(function (eve) {
    $("#modal-content3").load("/Usuario/Reasignar/" + $(this).data("id"));
});
