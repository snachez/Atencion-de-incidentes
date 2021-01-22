$(document).on("ready", function () {
    $("#Categoria").change(function () {
        var id = $("#Categoria").val();
        $(".idEspecialidad").empty();
        $.ajax({
            type: 'POST',
            url: '/Administrador/GetlistaEspecialidades',
            dataType: 'json',
            data: { id: $("#Categoria").val() },
            success: function (entidad) {
                $.each(entidad, function (i, dato) {
                    $(".idEspecialidad").append('<option value="' + dato.Value + '">' + dato.Text + '</option>');
                });
            }
        })
    });
});