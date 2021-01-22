function asignarTicket_Administrador(){
  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: 'btn btn-success',
      cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
  })

  swalWithBootstrapButtons.fire({
    title: '¿Está seguro de asignar el técnico?',
    text: "",
    type: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Guardar',
    cancelButtonText: 'Salir',
    reverseButtons: true
  }).then((result) => {
    if (result.value) {
      swalWithBootstrapButtons.fire(
        'Técnico asignado!',
        'El técnico fue asignado correctamente!',
        'success'
      )
    }
  })
}
$(".btncancelarCategoria").click(function (eve2) {
    $("#CancelarCategoria").load("/Administrador/CancelarCategoria/" + $(this).data("id"));
});