using Atencion_de_incidentes.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using PagedList;
using PagedList.Mvc;

namespace Atencion_de_incidentes.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdministradorController : Controller
    {
        public AdministradorController()
        {
        }
        public AdministradorController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        private ModeloUnedMS db = new ModeloUnedMS();
        // GET: Administrador
        public ActionResult Index(string Inicio, int? registro, int? pagina)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int pageSize = 10;
            pageSize = registro.HasValue ? Convert.ToInt32(registro) : 10;
            int pageIndex = 1;
            pageIndex = pagina.HasValue ? Convert.ToInt32(pagina) : 1;
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets)
                .Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).ToList().ToPagedList(pageIndex, pageSize);
            ViewBag.Incidencia = db.TBMSMAETickets.Count(x => x.IdEstado == 1);
            ViewBag.Asignados = db.TBMSMAETickets.Count(x => x.IdEstado == 2);
            ViewBag.Atendidos = db.TBMSMAETickets.Count(x => x.IdEstado == 3);
            System.Web.HttpContext.Current.Session["Inicio"] = "";
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias)
                    .Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers1.NombreCompleto.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower())).ToList().ToPagedList(pageIndex, pageSize);

                System.Web.HttpContext.Current.Session["Inicio"] = Inicio;

            }
            return View(tBMSMAETickets);
        }

        public void DescargarExcelTicket()
        {

            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades);
            string Inicio = System.Web.HttpContext.Current.Session["Inicio"].ToString();
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers1.NombreCompleto.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower()));
                System.Web.HttpContext.Current.Session["Inicio"] = "";
            }
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1:F1"].Merge = true;
            Sheet.Cells["A1:F1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Sheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#8EA9DB")));
            Sheet.Cells["A1:F1"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Value = "Reporte de total ticket";
            Sheet.Cells["A1:F1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;//titulo principal
            Sheet.Cells["A2:F2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Sheet.Cells["A2:F2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#8EA9DB")));
            Sheet.Cells["A2:F2"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2"].Value = "N° Ticket";
            Sheet.Cells["B2"].Value = "Nombre del Usuario";
            Sheet.Cells["C2"].Value = "Fecha Ticket";
            Sheet.Cells["D2"].Value = "Titulo";
            Sheet.Cells["E2"].Value = "Categoria";
            Sheet.Cells["F2"].Value = "Descripción";
            int row = 3;
            foreach (var item in tBMSMAETickets)
            {
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#E4E6E7")));
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row)].Value = item.idTicket;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.AspNetUsers1.NombreCompleto;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.fechaTicket;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.titulo;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.TBMSCATCategorias.tipoCategoria;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.TBMSCATEstadosTickets.Descripcion;
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        public void DescargarExcelTicketAtendidos()
        {

            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.TBMSCATEstadosTickets.Descripcion == "Atendido");
            string Inicio = System.Web.HttpContext.Current.Session["Inicio"].ToString();
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers1.NombreCompleto.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower()) && x.TBMSCATEstadosTickets.Descripcion == "Atendido");
                System.Web.HttpContext.Current.Session["Inicio"] = "";
            }
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1:F1"].Merge = true;
            Sheet.Cells["A1:F1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Sheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#8EA9DB")));
            Sheet.Cells["A1:F1"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Value = "Reporte de total ticket";
            Sheet.Cells["A1:F1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;//titulo principal
            Sheet.Cells["A2:F2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Sheet.Cells["A2:F2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#8EA9DB")));
            Sheet.Cells["A2:F2"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2"].Value = "N° Ticket";
            Sheet.Cells["B2"].Value = "Nombre del Usuario";
            Sheet.Cells["C2"].Value = "Fecha Ticket";
            Sheet.Cells["D2"].Value = "Titulo";
            Sheet.Cells["E2"].Value = "Categoria";
            Sheet.Cells["F2"].Value = "Descripción";
            int row = 3;
            foreach (var item in tBMSMAETickets)
            {
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#E4E6E7")));
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row)].Value = item.idTicket;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.AspNetUsers1.NombreCompleto;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.fechaTicket;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.titulo;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.TBMSCATCategorias.tipoCategoria;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.TBMSCATEstadosTickets.Descripcion;
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }
        public void DescargarExcelTicketAsignados()
        {

            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.TBMSCATEstadosTickets.Descripcion == "Asignado");
            string Inicio = System.Web.HttpContext.Current.Session["Inicio"].ToString();
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers1.NombreCompleto.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower()) && x.TBMSCATEstadosTickets.Descripcion == "Asignado");
                System.Web.HttpContext.Current.Session["Inicio"] = "";
            }
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1:F1"].Merge = true;
            Sheet.Cells["A1:F1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Sheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#8EA9DB")));
            Sheet.Cells["A1:F1"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Value = "Reporte de total ticket";
            Sheet.Cells["A1:F1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;//titulo principal
            Sheet.Cells["A2:F2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Sheet.Cells["A2:F2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#8EA9DB")));
            Sheet.Cells["A2:F2"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2"].Value = "N° Ticket";
            Sheet.Cells["B2"].Value = "Nombre del Usuario";
            Sheet.Cells["C2"].Value = "Fecha Ticket";
            Sheet.Cells["D2"].Value = "Titulo";
            Sheet.Cells["E2"].Value = "Categoria";
            Sheet.Cells["F2"].Value = "Descripción";
            int row = 3;
            foreach (var item in tBMSMAETickets)
            {
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#E4E6E7")));
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row)].Value = item.idTicket;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.AspNetUsers1.NombreCompleto;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.fechaTicket;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.titulo;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.TBMSCATCategorias.tipoCategoria;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.TBMSCATEstadosTickets.Descripcion;
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }
        public void DescargarExcelTicketIncidencia()
        {
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.TBMSCATEstadosTickets.Descripcion == "Incidencia");
            string Inicio = System.Web.HttpContext.Current.Session["Inicio"].ToString();
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers1.NombreCompleto.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower()) && x.TBMSCATEstadosTickets.Descripcion == "Incidencia");
                System.Web.HttpContext.Current.Session["Inicio"] = "";
            }
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1:F1"].Merge = true;
            Sheet.Cells["A1:F1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Sheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#8EA9DB")));
            Sheet.Cells["A1:F1"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Value = "Reporte de total ticket";
            Sheet.Cells["A1:F1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;//titulo principal
            Sheet.Cells["A2:F2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Sheet.Cells["A2:F2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#8EA9DB")));
            Sheet.Cells["A2:F2"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2"].Value = "N° Ticket";
            Sheet.Cells["B2"].Value = "Nombre del Usuario";
            Sheet.Cells["C2"].Value = "Fecha Ticket";
            Sheet.Cells["D2"].Value = "Titulo";
            Sheet.Cells["E2"].Value = "Categoria";
            Sheet.Cells["F2"].Value = "Descripción";
            int row = 3;
            foreach (var item in tBMSMAETickets)
            {
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#E4E6E7")));
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row)].Value = item.idTicket;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.AspNetUsers1.NombreCompleto;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.fechaTicket;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.titulo;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.TBMSCATCategorias.tipoCategoria;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.TBMSCATEstadosTickets.Descripcion;
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }
        public void DescargarExcelTicketAtendidosDetalle(int id)
        {

            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.TBMSCATEstadosTickets.Descripcion == "Atendido" && x.idTicket == id);
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1:F1"].Merge = true;
            Sheet.Cells["A1:F1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Sheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#8EA9DB")));
            Sheet.Cells["A1:F1"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Value = "Reporte de total ticket";
            Sheet.Cells["A1:F1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;//titulo principal
            Sheet.Cells["A2:F2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Sheet.Cells["A2:F2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#8EA9DB")));
            Sheet.Cells["A2:F2"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2"].Value = "N° Ticket";
            Sheet.Cells["B2"].Value = "Nombre del Usuario";
            Sheet.Cells["C2"].Value = "Fecha Ticket";
            Sheet.Cells["D2"].Value = "Titulo";
            Sheet.Cells["E2"].Value = "Categoria";
            Sheet.Cells["F2"].Value = "Descripción";
            int row = 3;
            foreach (var item in tBMSMAETickets)
            {
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#E4E6E7")));
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row)].Value = item.idTicket;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.AspNetUsers1.NombreCompleto;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.fechaTicket;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.titulo;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.TBMSCATCategorias.tipoCategoria;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.TBMSCATEstadosTickets.Descripcion;
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }
        public void DescargarExcelTicketAsignadosDetalle(int id)
        {

            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.TBMSCATEstadosTickets.Descripcion == "Asignado" && x.idTicket == id);
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1:F1"].Merge = true;
            Sheet.Cells["A1:F1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Sheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#8EA9DB")));
            Sheet.Cells["A1:F1"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Value = "Reporte de total ticket";
            Sheet.Cells["A1:F1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;//titulo principal
            Sheet.Cells["A2:F2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Sheet.Cells["A2:F2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#8EA9DB")));
            Sheet.Cells["A2:F2"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2"].Value = "N° Ticket";
            Sheet.Cells["B2"].Value = "Nombre del Usuario";
            Sheet.Cells["C2"].Value = "Fecha Ticket";
            Sheet.Cells["D2"].Value = "Titulo";
            Sheet.Cells["E2"].Value = "Categoria";
            Sheet.Cells["F2"].Value = "Descripción";
            int row = 3;
            foreach (var item in tBMSMAETickets)
            {
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#E4E6E7")));
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row)].Value = item.idTicket;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.AspNetUsers1.NombreCompleto;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.fechaTicket;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.titulo;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.TBMSCATCategorias.tipoCategoria;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.TBMSCATEstadosTickets.Descripcion;
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }
        public void DescargarExcelTicketIncidenciaDetalle(int id)
        {
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.TBMSCATEstadosTickets.Descripcion == "Incidencia" && x.idTicket == id);
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1:F1"].Merge = true;
            Sheet.Cells["A1:F1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Sheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#8EA9DB")));
            Sheet.Cells["A1:F1"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A1:F1"].Value = "Reporte de total ticket";
            Sheet.Cells["A1:F1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;//titulo principal
            Sheet.Cells["A2:F2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            Sheet.Cells["A2:F2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#8EA9DB")));
            Sheet.Cells["A2:F2"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2:F2"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            Sheet.Cells["A2"].Value = "N° Ticket";
            Sheet.Cells["B2"].Value = "Nombre del Usuario";
            Sheet.Cells["C2"].Value = "Fecha Ticket";
            Sheet.Cells["D2"].Value = "Titulo";
            Sheet.Cells["E2"].Value = "Categoria";
            Sheet.Cells["F2"].Value = "Descripción";
            int row = 3;
            foreach (var item in tBMSMAETickets)
            {
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#E4E6E7")));
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row) + ":" + string.Format("F{0}", row)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                Sheet.Cells[string.Format("A{0}", row)].Value = item.idTicket;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.AspNetUsers1.NombreCompleto;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.fechaTicket;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.titulo;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.TBMSCATCategorias.tipoCategoria;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.TBMSCATEstadosTickets.Descripcion;
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }
        public ActionResult ticketsDetalleIncidencia(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBMSMAETickets tBMSMAETickets = db.TBMSMAETickets.Find(id);
            if (tBMSMAETickets == null)
            {
                return HttpNotFound();
            }
            List<Mensajes> listamensajes = this.MostrarMensajeporticket(id);
            ViewBag.Mensajes = listamensajes;
            return View(tBMSMAETickets);
        }

        public ActionResult ticketsIncidencia(string Inicio, int? registro, int? pagina)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int pageSize = 10;
            pageSize = registro.HasValue ? Convert.ToInt32(registro) : 10;
            int pageIndex = 1;
            pageIndex = pagina.HasValue ? Convert.ToInt32(pagina) : 1;
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.TBMSCATEstadosTickets.Descripcion == "Incidencia").ToList().ToPagedList(pageIndex, pageSize);
            System.Web.HttpContext.Current.Session["Inicio"] = "";
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers1.NombreCompleto.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower()) && x.TBMSCATEstadosTickets.Descripcion == "Incidencia").ToList().ToPagedList(pageIndex, pageSize);
                System.Web.HttpContext.Current.Session["Inicio"] = Inicio;
            }
            return View(tBMSMAETickets);
        }

        public List<Atencion_de_incidentes.Models.Mensajes> MostrarMensajeporticket(int? id)
        {
            var consulta = db.Mensajes.Include(t => t.AspNetUsers).Where(t => t.idTicket == id);

            if (consulta.Count() == 0)
            {
                var mensaje = new List<Atencion_de_incidentes.Models.Mensajes>();
                return mensaje;
            }
            else
            {
                return consulta.ToList();
            }
        }
        public ActionResult ticketsDetalleAsignado(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBMSMAETickets tBMSMAETickets = db.TBMSMAETickets.Find(id);
            if (tBMSMAETickets == null)
            {
                return HttpNotFound();
            }
            List<Mensajes> listamensajes = this.MostrarMensajeporticket(id);
            ViewBag.Mensajes = listamensajes;
            ViewBag.idticket = id;
            return View(tBMSMAETickets);
        }

        public ActionResult ticketsAsignado(string Inicio, int? registro, int? pagina)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int pageSize = 10;
            pageSize = registro.HasValue ? Convert.ToInt32(registro) : 10;
            int pageIndex = 1;
            pageIndex = pagina.HasValue ? Convert.ToInt32(pagina) : 1;
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).ToList().Where(x => x.TBMSCATEstadosTickets.Descripcion.Contains("Asignado")).ToPagedList(pageIndex, pageSize);
            System.Web.HttpContext.Current.Session["Inicio"] = "";
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers1.NombreCompleto.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower()) && x.TBMSCATEstadosTickets.Descripcion.Contains("Asignado")).ToList().ToPagedList(pageIndex, pageSize);
                System.Web.HttpContext.Current.Session["Inicio"] = Inicio;
            }
            return View(tBMSMAETickets);
        }

        public ActionResult ticketsDetalleAtendido(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBMSMAETickets tBMSMAETickets = db.TBMSMAETickets.Find(id);
            if (tBMSMAETickets == null)
            {
                return HttpNotFound();
            }
            List<Mensajes> listamensajes = this.MostrarMensajeporticket(id);
            ViewBag.Mensajes = listamensajes;
            ViewBag.BaseDescripcion = db.TBMSMAEBaseConoc.Where(x => x.idTicket == id).FirstOrDefault().BaseDescripcion;
            return View(tBMSMAETickets);
        }

        public ActionResult ticketsAtendido(string Inicio, int? registro, int? pagina)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int pageSize = 10;
            pageSize = registro.HasValue ? Convert.ToInt32(registro) : 10;
            int pageIndex = 1;
            pageIndex = pagina.HasValue ? Convert.ToInt32(pagina) : 1;
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.TBMSCATEstadosTickets.Descripcion == "Atendido").ToList().ToPagedList(pageIndex, pageSize);
            System.Web.HttpContext.Current.Session["Inicio"] = "";
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers1.NombreCompleto.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower()) && x.TBMSCATEstadosTickets.Descripcion == "Atendido").ToList().ToPagedList(pageIndex, pageSize);
                System.Web.HttpContext.Current.Session["Inicio"] = Inicio;
            }
            return View(tBMSMAETickets);
        }

        public ActionResult Categorias(string Inicio, int? registro, int? pagina)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int pageSize = 10;
            pageSize = registro.HasValue ? Convert.ToInt32(registro) : 10;
            int pageIndex = 1;
            pageIndex = pagina.HasValue ? Convert.ToInt32(pagina) : 1;
            var tBMSCATCategorias = db.TBMSESPECIALIDADXCATEGORIAs.Include(t => t.TBMSCATCategorias).Include(t => t.TBMSMAEEspecialidades).ToList().ToPagedList(pageIndex, pageSize);
            System.Web.HttpContext.Current.Session["Inicio"] = "";
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSCATCategorias = db.TBMSESPECIALIDADXCATEGORIAs.Include(t => t.TBMSCATCategorias).Include(t => t.TBMSMAEEspecialidades).Where(x => x.TBMSCATCategorias.tipoCategoria.ToLower().Contains(Inicio.ToLower()) || x.TBMSMAEEspecialidades.tipoEspecialidad.ToLower().Contains(Inicio.ToLower())).ToList().ToPagedList(pageIndex, pageSize);
                System.Web.HttpContext.Current.Session["Inicio"] = Inicio;
            }
            return View(tBMSCATCategorias);
        }
        public ActionResult VisualizeStudentResult(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var result = db.Database.SqlQuery<REPORTE>("REPORTE @p0", id).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult reportes(int? id)
        {
            return View();
        }

        public ActionResult Especialidad(string Inicio, int? registro, int? pagina)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int pageSize = 10;
            pageSize = registro.HasValue ? Convert.ToInt32(registro) : 10;
            int pageIndex = 1;
            pageIndex = pagina.HasValue ? Convert.ToInt32(pagina) : 1;
            var tBMSMAEs = db.AspNetUsers.Include(t => t.TBMSMAEEspecialidades).Include(t => t.AspNetRoles).Where(x => x.idEspecialidad != null).ToList().ToPagedList(pageIndex, pageSize);
            System.Web.HttpContext.Current.Session["Inicio"] = "";
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAEs = db.AspNetUsers.Include(t => t.TBMSMAEEspecialidades).Include(t => t.AspNetRoles).Where(x => x.TBMSMAEEspecialidades.tipoEspecialidad.ToLower().Contains(Inicio.ToLower()) || x.NombreCompleto.ToLower().Contains(Inicio.ToLower()) && x.idEspecialidad != null).ToList().ToPagedList(pageIndex, pageSize);
                System.Web.HttpContext.Current.Session["Inicio"] = Inicio;
            }
            return View(tBMSMAEs);
        }

        public ActionResult CancelarCategoria(int? id)
        {
            System.Threading.Thread.Sleep(2000);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBMSESPECIALIDADXCATEGORIA especialidades = db.TBMSESPECIALIDADXCATEGORIAs.Find(id);
            if (especialidades == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEspecialidad = especialidades.IdEspecialidad;
            ViewBag.IdCategoria = especialidades.IdCategoria;
            return View(especialidades);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CancelarCategoria([Bind(Include = "IdEspecialidad,IdCategoria")] TBMSESPECIALIDADXCATEGORIA especialidades)
        {
            var respuesta = new ResponseModel
            {
                respuesta = true,
                redirect = "/Administrador/Categorias",
                error = "",
                mensaje = "Se borro categoria correctamente!"
            };
            try
            {
                db.Database.ExecuteSqlCommand("InactivarCategoria @idEspecialidad ='" + especialidades.IdEspecialidad + "', @idCategoria='" + especialidades.IdCategoria + "'");
            }
            catch
            {
                respuesta = new ResponseModel
                {
                    respuesta = false,
                    redirect = "",
                    error = "Se produjo un error al borrar la categoria!",
                    mensaje = ""
                };
            }
            return Json(respuesta);
        }
        public ActionResult EditarCategoria(int? id)
        {
            System.Threading.Thread.Sleep(2000);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBMSESPECIALIDADXCATEGORIA especialidades = db.TBMSESPECIALIDADXCATEGORIAs.Find(id);
            if (especialidades == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEspe = especialidades.IdEspecialidad;
            ViewBag.idCate = especialidades.IdCategoria;
            ViewBag.Categoria = new SelectList(db.TBMSCATCategorias, "idCategoria", "tipoCategoria");
            ViewBag.Especialidad = new SelectList(db.TBMSMAEEspecialidades, "idEspecialidad", "tipoEspecialidad");
            return View(especialidades);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditarCategoria(int idEspe, int idCate, string tipoCategoria, string tipoEspecialidad)
        {
            var respuesta = new ResponseModel
            {
                respuesta = true,
                redirect = "/Administrador/Categorias",
                error = "",
                mensaje = "Se modifico la categoria correctamente!"
            };
            if (tipoCategoria != "" || tipoEspecialidad != "")
            {
                try
                {
                    db.Database.ExecuteSqlCommand("EditarCategoria @idEspecialidad ='" + idEspe + "', @idCategoria='" + idCate + "', @Espe='" + tipoEspecialidad + "', @Cate='" + tipoCategoria + "'");

                }
                catch
                {
                    respuesta = new ResponseModel
                    {
                        respuesta = false,
                        redirect = "",
                        error = "Se produjo un error al modificar la categoria!",
                        mensaje = ""
                    };
                }
            }
            else
            {
                respuesta = new ResponseModel
                {
                    respuesta = false,
                    redirect = "",
                    error = "Se produjo un error al modificar la categoria!",
                    mensaje = ""
                };
            }

            return Json(respuesta);
        }
        public ActionResult NuevaCategoria()
        {
            System.Threading.Thread.Sleep(2000);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult NuevaCategoria(TBMSESPECIALIDADXCATEGORIA tBMSESPECIALIDADXCATEGORIA)
        {
            var respuesta = new ResponseModel
            {
                respuesta = true,
                redirect = "/Administrador/Categorias",
                error = "",
                mensaje = "La categoria fue creada correctamente!"
            };
            if (!ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand("CrearCategoria @tipoEspecialidad ='" + tBMSESPECIALIDADXCATEGORIA.TBMSMAEEspecialidades.tipoEspecialidad + "', @tipoCategoria='" + tBMSESPECIALIDADXCATEGORIA.TBMSCATCategorias.tipoCategoria + "'");
                }
                catch
                {
                    respuesta = new ResponseModel
                    {
                        respuesta = false,
                        redirect = "",
                        error = "Se produjo un error al crear la categoria!",
                        mensaje = ""
                    };
                }
            }
            return Json(respuesta);
        }

        public ActionResult GetlistaCategorias()
        {
            var cate = new SelectList(db.TBMSCATCategorias, "idCategoria", "tipoCategoria");
            return Json(cate, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetlistaEspecialidades(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var entidad = new SelectList(db.TBMSMAEEspecialidades.Where(x => x.idEspecialidad == id), "idEspecialidad", "tipoEspecialidad");
            return Json(entidad, JsonRequestBehavior.AllowGet);
        }

        public ActionResult nuevoTecnico()
        {
            System.Threading.Thread.Sleep(2000);
            ViewBag.idEspecialidad = new SelectList(db.TBMSMAEEspecialidades, "idEspecialidad", "tipoEspecialidad");
            return View();
        }
        // POST: TBMSMAETickets/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult nuevoTecnico([Bind(Include = "Email,PasswordHash,PhoneNumber,NombreCompleto,Departamento,idEspecialidad")] AspNetUsers aspNetUsers)
        {
            var respuesta = new ResponseModel
            {
                respuesta = true,
                redirect = "/Administrador/Especialidad",
                error = "",
                mensaje = "El Tecnico fue creado correctamente!"
            };
            if (!ModelState.IsValid)
            {
                var roleManager = System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
                var user = new ApplicationUser { UserName = aspNetUsers.NombreCompleto.ToLower().Substring(0, 1) + string.Join("", aspNetUsers.NombreCompleto.ToLower().Split(' ').Skip(1).Take(1).ToArray()), Email = aspNetUsers.Email };
                user.NombreCompleto = aspNetUsers.NombreCompleto;
                user.Departamento = aspNetUsers.Departamento;
                user.EmailConfirmed = true;
                user.PhoneNumberConfirmed = false;
                user.TwoFactorEnabled = false;
                user.LockoutEnabled = false;
                user.AccessFailedCount = 0;
                user.PhoneNumber = aspNetUsers.PhoneNumber;
                user.idEspecialidad = aspNetUsers.idEspecialidad;
                var result = UserManager.Create(user, aspNetUsers.PasswordHash);
                if (result.Succeeded)
                {
                    try
                    {
                        var role = roleManager.FindByName("Tecnico");
                        var rslt = UserManager.AddToRole(user.Id, role.Name);
                    }
                    catch (Exception)
                    {
                        respuesta = new ResponseModel
                        {
                            respuesta = false,
                            redirect = "",
                            error = "Se produjo un error al crear el tecnico!",
                            mensaje = ""
                        };
                    }
                }

            }
            return Json(respuesta);
        }

        public ActionResult TecnicoEditar(string id)
        {
            System.Threading.Thread.Sleep(2000);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categoria = new SelectList(db.TBMSCATCategorias, "idCategoria", "tipoCategoria");
            ViewBag.Id = aspNetUsers.Id;
            return View(aspNetUsers);
        }
        // POST: TBMSMAETickets/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TecnicoEditar([Bind(Include = "Id,Address,City,PostalCode,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,NombreCompleto,Departamento,idEspecialidad,Imagen")] AspNetUsers aspNetUsers, TBMSMAEEspecialidades tBMSMAEEspecialidades)
        {
            byte[] imagenActual = null;
            HttpPostedFileBase fileBase = Request.Files[0];

            if (fileBase == null)
            {
                imagenActual = db.AspNetUsers.SingleOrDefault(t => t.Id == aspNetUsers.Id).Imagen;
            }
            else
            {
                WebImage image = new WebImage(fileBase.InputStream);
                aspNetUsers.Imagen = image.GetBytes();
            }
            if (aspNetUsers.Email != null)
            {
                var user = await UserManager.FindByIdAsync(aspNetUsers.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                user.UserName = aspNetUsers.Email;
                user.Email = aspNetUsers.Email;
                user.NombreCompleto = aspNetUsers.NombreCompleto;
                user.Departamento = aspNetUsers.Departamento;
                user.EmailConfirmed = true;
                user.PhoneNumberConfirmed = false;
                user.TwoFactorEnabled = false;
                user.LockoutEnabled = false;
                user.AccessFailedCount = 0;
                user.PhoneNumber = aspNetUsers.PhoneNumber;
                user.idEspecialidad = tBMSMAEEspecialidades.idEspecialidad;
                user.Imagen = aspNetUsers.Imagen;
                if (aspNetUsers.PasswordHash != null)
                {
                    var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    var result = await UserManager.ResetPasswordAsync(user.Id, code, aspNetUsers.PasswordHash);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }

            return View(aspNetUsers);
        }

        public ActionResult TecnicoCancelar(string id)
        {
            System.Threading.Thread.Sleep(2000);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = aspNetUsers.Id;
            return View(aspNetUsers);
        }
        // POST: TBMSMAETickets/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TecnicoCancelar([Bind(Include = "Id")] AspNetUsers aspNetUsers)
        {
            try
            {
                var aspNetUser = db.AspNetUsers.Find(aspNetUsers.Id);
                db.AspNetUsers.Remove(aspNetUser);
                db.SaveChanges();
                return RedirectToAction("Especialidad");
            }
            catch
            {
                TempData["Error"] = "No se puede borrar porque tiene ticket";
                ViewBag.Error = TempData["Error"];
                return RedirectToAction("Especialidad");
            }
        }
        public ActionResult Reactivar(int? id)
        {
            System.Threading.Thread.Sleep(2000);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBMSMAETickets tBMSMAETickets = db.TBMSMAETickets.Find(id);
            if (tBMSMAETickets == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTicket = tBMSMAETickets.idTicket;
            return View();
        }

        // POST: TBMSMAETickets/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reactivar([Bind(Exclude = "idUsuario,IdUsuarioTec,idPrioridad,IdEstado,idCategoria,idEspecialidad,titulo,descripcion,fechaTicket,estadoTicket,fechaAtendido,adjuntoTicket,idCalificacion,Solucion", Include = "idTicket")] TBMSMAETickets tBMSMAETickets)
        {
            if (tBMSMAETickets.idTicket != 0)
            {
                TBMSMAETickets tBMSMAE = db.TBMSMAETickets.Where(x => x.idTicket == tBMSMAETickets.idTicket).FirstOrDefault();
                tBMSMAE.Solucion = null;
                tBMSMAE.IdEstado = 2;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tBMSMAETickets);
        }

        public ActionResult Incidencia(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBMSMAETickets tBMSMAETickets = db.TBMSMAETickets.Find(id);
            if (tBMSMAETickets == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTicket = tBMSMAETickets.idTicket;
            return View();
        }

        // POST: TBMSMAETickets/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incidencia([Bind(Exclude = "idUsuario,IdUsuarioTec,idPrioridad,IdEstado,idCategoria,idEspecialidad,titulo,descripcion,fechaTicket,estadoTicket,fechaAtendido,adjuntoTicket,idCalificacion", Include = "idTicket,Solucion")] TBMSMAETickets tBMSMAETickets)
        {
            if (tBMSMAETickets.Solucion != null)
            {
                TBMSMAETickets tBMSMAE = db.TBMSMAETickets.Where(x => x.idTicket == tBMSMAETickets.idTicket).FirstOrDefault();
                tBMSMAE.Solucion = tBMSMAETickets.Solucion;
                tBMSMAE.IdEstado = 1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tBMSMAETickets);
        }

        public ActionResult MostrarCalificacion(int? id)
        {
            System.Threading.Thread.Sleep(2000);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBMSMAETickets tBMSMAETickets = db.TBMSMAETickets.Find(id);
            if (tBMSMAETickets == null)
            {
                return HttpNotFound();
            }
            ViewBag.valoracion = db.TBMCALICalificacion.Where(x => x.idCalificacion == tBMSMAETickets.idCalificacion).FirstOrDefault().valoracion;
            return View();
        }

        public ActionResult CerrarTicket(int? id)
        {
            System.Threading.Thread.Sleep(2000);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBMSMAETickets tBMSMAETickets = db.TBMSMAETickets.Find(id);
            if (tBMSMAETickets == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTicket = tBMSMAETickets.idTicket;
            ViewBag.idCategoria = tBMSMAETickets.idCategoria;
            return View();
        }

        // POST: TBMSMAETickets/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CerrarTicket([Bind(Include = "idBase,idTicket,idCategoria,BaseDescripcion")] TBMSMAEBaseConoc tBMSMAEBase)
        {
            if (ModelState.IsValid)
            {
                db.TBMSMAEBaseConoc.Add(tBMSMAEBase);
                TBMSMAETickets tBMSMAE = db.TBMSMAETickets.Where(x => x.idTicket == tBMSMAEBase.idTicket).FirstOrDefault();
                tBMSMAE.IdEstado = 3;
                tBMSMAE.fechaAtendido = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tBMSMAEBase);
        }

        public ActionResult Reasignar(int? id)
        {
            System.Threading.Thread.Sleep(2000);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBMSMAETickets tBMSMAETickets = db.TBMSMAETickets.Find(id);
            if (tBMSMAETickets == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUsuarioTec = new SelectList(db.AspNetUsers.Where(x => x.idEspecialidad != null), "Id", "NombreCompleto", tBMSMAETickets.IdUsuarioTec);
            return View(tBMSMAETickets);
        }

        // POST: TBMSMAETickets/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Reasignar([Bind(Exclude = "idUsuario,idPrioridad,IdEstado,idCategoria,idEspecialidad,titulo,descripcion,fechaTicket,estadoTicket,fechaAtendido,adjuntoTicket,idCalificacion", Include = "idTicket,IdUsuarioTec")] TBMSMAETickets tBMSMAETickets)
        {
            var respuesta = new ResponseModel
            {
                respuesta = true,
                redirect = "/Administrador/Index",
                error = "",
                mensaje = "El Ticket fue reasignado correctamente!"
            };
            try
            {
                if (tBMSMAETickets.IdUsuarioTec != null)
                {
                    TBMSMAETickets tBMSMAE = db.TBMSMAETickets.Where(x => x.idTicket == tBMSMAETickets.idTicket).FirstOrDefault();
                    AspNetUsers aspNetUsers = db.AspNetUsers.Where(x => x.Id == tBMSMAETickets.IdUsuarioTec).FirstOrDefault();
                    TBMSESPECIALIDADXCATEGORIA tBMSESPECIALIDADXCATEGORIA = db.TBMSESPECIALIDADXCATEGORIAs.Where(x => x.IdEspecialidad == aspNetUsers.idEspecialidad).FirstOrDefault();
                    tBMSMAE.IdUsuarioTec = tBMSMAETickets.IdUsuarioTec;
                    tBMSMAE.idCategoria = aspNetUsers.idEspecialidad;
                    tBMSMAE.idEspecialidad = tBMSESPECIALIDADXCATEGORIA.IdCategoria;
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("IdUsuarioTec-mensaje", "Debe seleccionar un tecnico");
                    respuesta.respuesta = false;
                    respuesta.error = "Debe seleccionar un tecnico";
                    respuesta.redirect = "";
                    respuesta.mensaje = "";
                }
                ViewBag.IdUsuarioTec = new SelectList(db.AspNetUsers.Where(x => x.idEspecialidad != null), "Id", "NombreCompleto", tBMSMAETickets.IdUsuarioTec);
            }
            catch (Exception)
            {
                respuesta = new ResponseModel
                {
                    respuesta = false,
                    redirect = "",
                    error = "Se produjo un error al reasignar el ticket!",
                    mensaje = ""
                };
            }
            return Json(respuesta);
        }
    }
}
