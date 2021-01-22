$(document).on("ready", function () {
    $("#registro").change(function () {
        var id = $("#registro").val();
        var Inicio = $("#Inicio").val();
        window.location.href = "/Tecnico/Index?registro=" + id + "&Inicio=" + Inicio + "";
    });
});