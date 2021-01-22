using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Atencion_de_incidentes.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Atencion_de_incidentes.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ModeloUnedMS db = new ModeloUnedMS();
        public HomeController() { }
        public HomeController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ActionResult convertirimagen(string id)
        {
            var imagen = db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
            if (imagen.Imagen == null)
            {
                string path = Server.MapPath("~/Content/img/user.jpg");
                return File(path, "image/jpeg");
            }
            return File(imagen.Imagen, "image/jpeg");
        }
        public ActionResult Index()
        {
            var id = System.Web.HttpContext.Current.Session["user"].ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.AspNetUsers.Where(x => x.Id == id).SingleOrDefault();
            //var user = await UserManager.FindByIdAsync(id);
            try
            {
                ViewBag.Mensaje = TempData["Mensaje"].ToString();
            }
            catch { }
            return View(user);
        }

        [HttpPost]
        public ActionResult Recargarimagen()
        {
            var id = System.Web.HttpContext.Current.Session["user"].ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers user = db.AspNetUsers.Find(id);

            try
            {
                HttpPostedFileBase fileBase = Request.Files[0];
                WebImage image = new WebImage(fileBase.InputStream);
                user.Imagen = image.GetBytes();
                db.Database.ExecuteSqlCommand("EXEC ModificarImagen @Id, @Imagen",
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Imagen", user.Imagen));
                TempData["Mensaje"] = "Se actualizo la foto correctamente";
                ViewBag.Mensaje = TempData["Mensaje"];
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Mensaje"] = "Base de datos no conectada";
                ViewBag.Mensaje = TempData["Mensaje"];
                return RedirectToAction("Index");
            }

        }

        [Authorize]
        public ActionResult Contact()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult LoadMenu()
        {
            var menu = from a in db.MenuTemp
                       select a;
            return View(menu.ToList());
        }

        public ActionResult Soporte()
        {
            return View();
        }

    }
}