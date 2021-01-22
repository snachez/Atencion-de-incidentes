/*Crear ticket*/
function SubmitForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var files = $('#file')[0].files[0];
        form.data("file", files);
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: false
        })

        swalWithBootstrapButtons.fire({
            title: '¿Está seguro de crear nuevo ticket?',
            text: "",
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Guardar',
            cancelButtonText: 'Salir',
            reverseButtons: true
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    url: form.action,
                    type: 'post',
                    data: $(form).serialize(),
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data.respuesta) {
                            swalWithBootstrapButtons.fire({
                                title: 'Ticket creado!',
                                text: data.mensaje,
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500
                            })
                            setTimeout(function () {
                                window.location.href = data.redirect;
                            }, 2000);

                        }
                        else {
                            swalWithBootstrapButtons.fire({
                                title: 'Ticket no creado!',
                                text: data.error,
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500
                            })
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        swalWithBootstrapButtons.fire({
                            title: 'Ticket no creado!',
                            text: errorThrown,
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }
                });
            }
        })

    }
    return false;
}
/*Fin de Crear Ticket*/
/*Crear Nueva Categoria*/
function SubmitFormNuevaCategoria(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: false
        })

        swalWithBootstrapButtons.fire({
            title: '¿Está seguro de crear nueva categoria?',
            text: "",
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Guardar',
            cancelButtonText: 'Salir',
            reverseButtons: true
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    type: "POST",
                    url: form.action,
                    data: $(form).serialize(),
                    success: function (data) {
                        if (data.respuesta) {
                            swalWithBootstrapButtons.fire({
                                title: 'Categoria creada!',
                                text: data.mensaje,
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500
                            })
                            setTimeout(function () {
                                window.location.href = data.redirect;
                            }, 2000);

                        }
                        else {
                            swalWithBootstrapButtons.fire({
                                title: 'Categoria no creada!',
                                text: data.error,
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500
                            })
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        swalWithBootstrapButtons.fire({
                            title: 'Categoria no creada!',
                            text: errorThrown,
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }
                });
            }
        })

    }
    return false;
}
/*Fin de Crear Nueva Categoria*/
/*Crear nuevo Tecnico*/
function SubmitFormNuevoTecnico(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: false
        })

        swalWithBootstrapButtons.fire({
            title: '¿Está seguro de crear nuevo tecnico?',
            text: "",
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Guardar',
            cancelButtonText: 'Salir',
            reverseButtons: true
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    type: "POST",
                    url: form.action,
                    data: $(form).serialize(),
                    success: function (data) {
                        if (data.respuesta) {
                            swalWithBootstrapButtons.fire({
                                title: 'Tecnico creado!',
                                text: data.mensaje,
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500
                            })
                            setTimeout(function () {
                                window.location.href = data.redirect;
                            }, 2000);

                        }
                        else {
                            swalWithBootstrapButtons.fire({
                                title: 'Tecnico no creado!',
                                text: data.error,
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500
                            })
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        swalWithBootstrapButtons.fire({
                            title: 'Tecnico no creado!',
                            text: errorThrown,
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }
                });
            }
        })

    }
    return false;
}
/*Fin de Crear Nuevo Tecnico*/
/*Guardar Calificacion*/
function SubmitFormGuardarCalificacion(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: false
        })

        swalWithBootstrapButtons.fire({
            title: '¿Está seguro de guardar la calificacion?',
            text: "",
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Guardar',
            cancelButtonText: 'Salir',
            reverseButtons: true
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    type: "POST",
                    url: form.action,
                    data: $(form).serialize(),
                    success: function (data) {
                        if (data.respuesta) {
                            swalWithBootstrapButtons.fire({
                                title: 'Calificacion guardada!',
                                text: data.mensaje,
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500
                            })
                            setTimeout(function () {
                                window.location.href = data.redirect;
                            }, 2000);

                        }
                        else {
                            swalWithBootstrapButtons.fire({
                                title: 'Calificacion no guardada!',
                                text: data.error,
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500
                            })
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        swalWithBootstrapButtons.fire({
                            title: 'Calificacion no guardada!',
                            text: errorThrown,
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }
                });
            }
        })

    }
    return false;
}
/*Fin Guardar Calificacion*/
/*Cancelar Categoria*/
function SubmitFormCancelarCategoria(form) {
    $.ajax({
        type: "POST",
        url: form.action,
        data: $(form).serialize(),
        success: function (data) {
            if (data.respuesta) {
                swalWithBootstrapButtons.fire({
                    title: 'Categoria borrada!',
                    text: data.mensaje,
                    icon: 'success',
                    showConfirmButton: false,
                    timer: 1500
                })
                setTimeout(function () {
                    window.location.href = data.redirect;
                }, 2000);

            }
            else {
                swalWithBootstrapButtons.fire({
                    title: 'Categoria no se borro!',
                    text: data.error,
                    icon: 'error',
                    showConfirmButton: false,
                    timer: 1500
                })
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            swalWithBootstrapButtons.fire({
                title: 'Categoria no se borro!',
                text: errorThrown,
                icon: 'error',
                showConfirmButton: false,
                timer: 1500
            })
        }
    });
}
/*Fin Cancelar Categoria*/
/*Editar Categoria*/
function SubmitFormEditarCategoria(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: false
        })

        swalWithBootstrapButtons.fire({
            title: '¿Está seguro de editar la categoria?',
            text: "",
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Guardar',
            cancelButtonText: 'Salir',
            reverseButtons: true
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    type: "POST",
                    url: form.action,
                    data: $(form).serialize(),
                    success: function (data) {
                        if (data.respuesta) {
                            swalWithBootstrapButtons.fire({
                                title: 'Categoria modificada!',
                                text: data.mensaje,
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500
                            })
                            setTimeout(function () {
                                window.location.href = data.redirect;
                            }, 2000);

                        }
                        else {
                            swalWithBootstrapButtons.fire({
                                title: 'Categoria no se modifico!',
                                text: data.error,
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500
                            })
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        swalWithBootstrapButtons.fire({
                            title: 'Categoria no se modifico!',
                            text: errorThrown,
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }
                });
            }
        })
    }
    return false;
}
/*Fin Editar Categoria*/
/*Reasignar ticket*/
function SubmitFormReasignarTicket(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: false
        })

        swalWithBootstrapButtons.fire({
            title: '¿Está seguro de reasignar el ticket?',
            text: "",
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Guardar',
            cancelButtonText: 'Salir',
            reverseButtons: true
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    url: form.action,
                    type: 'post',
                    data: $(form).serialize(),
                    success: function (data) {
                        if (data.respuesta) {
                            swalWithBootstrapButtons.fire({
                                title: 'Ticket reasignado!',
                                text: data.mensaje,
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500
                            })
                            setTimeout(function () {
                                window.location.href = data.redirect;
                            }, 2000);

                        }
                        else if (data.error == "Debe seleccionar un tecnico") {
                            $("#IdUsuarioTec-validacion").text(data.error)
                                .show();
                        }
                        else {
                            swalWithBootstrapButtons.fire({
                                title: 'Ticket no reasignado!',
                                text: data.error,
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500
                            })
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        swalWithBootstrapButtons.fire({
                            title: 'Ticket no reasignado!',
                            text: errorThrown,
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }
                });
            }
        })

    }
    return false;
}
/*Fin de Reasignar ticket*/