$(function () {
    //Click en la notificacion icono
    $('span.noti').click(function (e) {
        e.stopPropagation();
        $('.noti-content').show();
        var count = 0;
        count = parseInt($('span.count').html()) || 0;
        //solo carga notificacion si no carga
        if (count > 0) {
            updateNotification();
        }
        $('span.count', this).html('&nbsp;');
    })
    // Oculta notificacion
    $('html').click(function () {
        $('.noti-content').hide();
    })
    //Actualiza notificacion
    function updateNotification() {
        $('#notiContent').empty();
        $('#notiContent').append($('<li>Cargando...</li>'));

        $.ajax({
            type: 'GET',
            url: '/Tecnico/GetNotificacion',
            success: function (response) {
                $('#notiContent').empty();
                if (response.length == 0) {
                    $('#notiContent').append($('<li>No datos disponibles</li>'));
                }
                $.each(response, function (index, value) {
                    if (value.idPrioridad == 1) {

                        $('#notiContent').append($('<li><div class="alert alert-warning" role="alert">Nuevo ticket creado ' + value.titulo + ', descripcion: ' + value.descripcion + ' <a href="http://localhost:36534/Tecnico/ticketsDetalleAsignado/' + value.idTicket +'">Ir</a></div></li>'));
                    } else if (value.idPrioridad == 2) {
                        $('#notiContent').append($('<li><div class="alert alert-danger" role="alert">!Nuevo ticket creado ' + value.titulo + ', descripcion: ' + value.descripcion + ' <a href="http://localhost:36534/Tecnico/ticketsDetalleAsignado/' + value.idTicket +'">Ir</a></div></li>'));
                    } else if (value.idPrioridad == 3) {
                        $('#notiContent').append($('<li ><div class="alert alert-primary" role="alert">!Urgente ticket creado ' + value.titulo + ', descripcion: ' + value.descripcion + ' <a href="http://localhost:36534/Tecnico/ticketsDetalleAsignado/' + value.idTicket+'">Ir</a></div></li>'));
                    }

                });
            },
            error: function (error) {
                console.log(error);
            }
        })
    }
    //Actualiza notificacion cuenta
    function updateNotificationCount() {
        var count = 0;
        count = parseInt($('span.count').html()) || 0;
        count++;
        $('span.count').html(count);
    }
    //Signalr js codigo para iniciar y enviar notificaciones recibidas
    var notificationHub = $.connection.notificationHub;
    $.connection.hub.start().done(function () {
        console.log('Notificacion hub iniciada');
    });

    //signalr metodo para push server mensaje al cliente
    notificationHub.client.notify = function (message) {
        if (message && message.toLowerCase() == "added") {
            updateNotificationCount();
        }
    }

})