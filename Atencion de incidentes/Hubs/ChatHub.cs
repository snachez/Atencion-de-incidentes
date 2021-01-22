using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Atencion_de_incidentes.Models;

namespace Atencion_de_incidentes.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string nombre,int idticket,string mensajetexto,string idUser)
        {
            string fecha = DateTime.Now.ToString();
            using (ModeloUnedMS db = new ModeloUnedMS())
            {
                var Mensaje = new Mensajes();
                Mensaje.idTicket = idticket;
                Mensaje.idUsuario = idUser;
                Mensaje.texto = mensajetexto;
                Mensaje.fechaCreacion = DateTime.Now;
                Mensaje.Estado = true;
                db.Mensajes.Add(Mensaje);
                db.SaveChanges();
            }
                Clients.All.SendChat(nombre, mensajetexto, fecha, idUser);
        }
    }
}