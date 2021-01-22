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
$("#btnagregarcategoria").click(function (eve2) {
    $("#NuevaCategoria").load("/Administrador/NuevaCategoria/");
});