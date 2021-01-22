$(".btncerrar").click(function (eve) {
    $("#modal-content2").load("/Tecnico/CerrarTicket/" + $(this).data("id"));
});