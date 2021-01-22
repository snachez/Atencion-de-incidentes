$(".btneditarCategoria").click(function (eve2) {
    $("#EditarCategoria").load("/Administrador/EditarCategoria/" + $(this).data("id"));
});