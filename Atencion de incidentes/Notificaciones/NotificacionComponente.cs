using Atencion_de_incidentes.Hubs;
using Atencion_de_incidentes.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Atencion_de_incidentes.Notificaciones
{
    public class NotificacionComponente
    {
        public void RegistroNotificacion()
        {
            string conStr = ConfigurationManager.ConnectionStrings["ModeloUnedMS"].ConnectionString;
            string sqlCommand = @"SELECT [idPrioridad],[titulo],[fechaTicket] from [dbo].[TBMSMAETickets] where [IdEstado] = 2 ";

            using (SqlConnection con = new SqlConnection(conStr))
            {
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand(sqlCommand, con))
                {
                    cmd.Notification = null;
                    SqlDependency sqlDep = new SqlDependency(cmd);
                    sqlDep.OnChange += sqlDep_OnChange;
                    cmd.ExecuteReader();
                }
            }
        }

        void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= sqlDep_OnChange;

                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                notificationHub.Clients.All.notify("added");

                RegistroNotificacion();
            }
        }
    }
}