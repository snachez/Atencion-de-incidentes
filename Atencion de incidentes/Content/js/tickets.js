/*Utilizaremos ventanas emergentes con sweetalert*/
/*Inicio para asignarTicket_Adimistrador*/
function asignarTicket_Administrador(){/*Cuando el Administrador desea asignar un técnico al ticket*/
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
/*Final para asignarTicket_Adimistrador*/
/*Inicio para cerrarTicket_Adimistrador*/
function cerrarTicket_Administrador(){/*Cuando el administrador desea cerrar un ticket*/
  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: 'btn btn-success',
      cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
  })

  swalWithBootstrapButtons.fire({
    title: '¿Realmente desea cerrar el ticket?',
    text: "",
    type: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Guardar',
    cancelButtonText: 'Salir',
    reverseButtons: true
  }).then((result) => {
    if (result.value) {
      swalWithBootstrapButtons.fire(
        'Ticket finalizado!',
        'El ticket fue finalizado correctamente!',
        'success'
      )
    }
  })
}
/*Final para cerrarTicket_Adimistrador*/
/*Inicio para reasignarTicket_Adimistrador*/
function reasignarTicket_Administrador(){/*Cuando el administrador desea reasignar un técnico a otro técnico*/
  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: 'btn btn-success',
      cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
  })

  swalWithBootstrapButtons.fire({
    title: '¿Está seguro de reasignar el técnico?',
    text: "",
    type: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Guardar',
    cancelButtonText: 'Salir',
    reverseButtons: true
  }).then((result) => {
    if (result.value) {
      swalWithBootstrapButtons.fire(
        'Técnico reasignado!',
        'El técnico fue reasignado correctamente!',
        'success'
      )
    }
  })
}
/*Final para reasignarTicket_Adimistrador*/
/*Inicio para agregarCategoria_Adimistrador*/
function agregarCategoria_Administrador(){/*Cuando el administrador desea agregar una categoria al ticket*/
  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: 'btn btn-success',
      cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
  })

  swalWithBootstrapButtons.fire({
    title: '¿Realmente desea agregar una nueva categoria al ticket?',
    text: "",
    type: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Guardar',
    cancelButtonText: 'Salir',
    reverseButtons: true
  }).then((result) => {
    if (result.value) {
      swalWithBootstrapButtons.fire(
        'Categoria creada!',
        'La categoria del ticket fue creado correctamente!',
        'success'
      )
    }
  })
}
/*Final para agregarCategoria_Adimistrador*/
/*Inicio para editarCategoria_Adimistrador*/
function editarCategoria_Administrador(){/*Cuando el administrador desde editar una categoría en especifico*/
  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: 'btn btn-success',
      cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
  })

  swalWithBootstrapButtons.fire({
    title: '¿¿Realmente desea editar está categoría?',
    text: "",
    type: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Guardar',
    cancelButtonText: 'Salir',
    reverseButtons: true
  }).then((result) => {
    if (result.value) {
      swalWithBootstrapButtons.fire(
        'Categoría editado!',
        'La categoría fue editado correctamente!',
        'success'
      )
    }
  })
}
/*Final para editarCategoria_Adimistrador*/
/*Inicio para cancelarCategoria_Adimistrador*/
function cancelarCategoria_Administrador(){/*Cuando el administrador desea cancelar una categoria en especifico*/
  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: 'btn btn-success',
      cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
  })

  swalWithBootstrapButtons.fire({
    title: '¿Realmente desea cancelar está categoria?',
    text: "",
    type: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Guardar',
    cancelButtonText: 'Salir',
    reverseButtons: true
  }).then((result) => {
    if (result.value) {
      swalWithBootstrapButtons.fire(
        'Categoria cancelada!',
        'La categoria fue cancelada correctamente!',
        'success'
      )
    }
  })
}
/*Final para cancelarCategoria_Adimistrador*/
/*Inicio para agregarEspecialidad_Adimistrador*/
function agregarEspecialidad_Administrador(){/*Cuando el administrador desea agregar una especialidad al técnico*/
  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: 'btn btn-success',
      cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
  })

  swalWithBootstrapButtons.fire({
    title: '¿Realmente desea agregar está especialidad al técnico?',
    text: "",
    type: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Guardar',
    cancelButtonText: 'Salir',
    reverseButtons: true
  }).then((result) => {
    if (result.value) {
      swalWithBootstrapButtons.fire(
        'Especialidad creada!',
        'La especialidad fue creada correctamente!',
        'success'
      )
    }
  })
}
/*Final para agregarEspecialidad_Adimistrador*/
/*Inicio para agregarIntegrante_Adimistrador*/
function agregarIntegrante_Administrador(){/*Cuando el administrador desea agregar un nuevo integrante a la empresa*/
  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: 'btn btn-success',
      cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
  })

  swalWithBootstrapButtons.fire({
    title: '¿Realmente desea agregar un nuevo integrante?',
    text: "",
    type: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Guardar',
    cancelButtonText: 'Salir',
    reverseButtons: true
  }).then((result) => {
    if (result.value) {
      swalWithBootstrapButtons.fire(
        'Integrante creado!',
        'El integrante fue creado correctamente!',
        'success'
      )
    }
  })
}
/*Final para agregarIntegrante_Adimistrador*/
/*Inicio para cancelarIntegrante_Adimistrador*/
function cancelarIntegrante_Administrador(){/*Cuando el administrador desea cancelar un nuevo integrante de la empresa*/
  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: 'btn btn-success',
      cancelButton: 'btn btn-danger'
    },
    buttonsStyling: false
  })

  swalWithBootstrapButtons.fire({
    title: '¿Realmente desea cancelar este integrante?',
    text: "",
    type: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Guardar',
    cancelButtonText: 'Salir',
    reverseButtons: true
  }).then((result) => {
    if (result.value) {
      swalWithBootstrapButtons.fire(
        'Integrante cancelado!',
        'El integrante fue cancelado correctamente!',
        'success'
      )
    }
  })
}
/*Final para cancelarIntegrante_Adimistrador*/
