using Atencion_de_incidentes.Notificaciones;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Atencion_de_incidentes
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string con = ConfigurationManager.ConnectionStrings["ModeloUnedMS"].ConnectionString;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Aqui iniciamos sql dependency
            SqlDependency.Start(con);
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            NotificacionComponente NC = new NotificacionComponente();
            NC.RegistroNotificacion();
        }
        protected void Application_End()
        {
            //Aqui detenemos sql dependency
            SqlDependency.Stop(con);
        }
    }
}
