$(document).ready(function () {
    $("#idCategoria").change(function () {
        var id = $("#idCategoria").val();
        $(".idEspecialidad").empty();
        $.ajax({
            type: 'POST',
            url: '/Usuario/GetlistaEspecialidades',
            dataType: 'json',
            data: { id: $("#idCategoria").val() },
            success: function (entidad) {
                $.each(entidad, function (i, dato) {
                    $(".idEspecialidad").append('<option value="' + dato.Value + '">' + dato.Text + '</option>');
                });
            }
        })
    });
});