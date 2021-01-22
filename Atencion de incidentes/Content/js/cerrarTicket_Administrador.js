$(".btncerrar").click(function (eve) {
    $("#modal-content2").load("/Administrador/CerrarTicket/" + $(this).data("id"));
});