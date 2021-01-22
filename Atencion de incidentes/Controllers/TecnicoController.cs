using Atencion_de_incidentes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using OfficeOpenXml;
using System.Drawing;
using Atencion_de_incidentes.Notificaciones;
using PagedList;

namespace Atencion_de_incidentes.Controllers
{
    [Authorize(Roles = "Tecnico")]
    public class TecnicoController : Controller
    {
        private ModeloUnedMS db = new ModeloUnedMS();
        // GET: Tecnico
        public ActionResult Index(string Inicio, int? registro, int? pagina)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int pageSize = 10;
            pageSize = registro.HasValue ? Convert.ToInt32(registro) : 10;
            int pageIndex = 1;
            pageIndex = pagina.HasValue ? Convert.ToInt32(pagina) : 1;
            var id = System.Web.HttpContext.Current.Session["user"].ToString();
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(t => t.IdUsuarioTec == id).ToList().ToPagedList(pageIndex, pageSize);
            ViewBag.Incidencia = db.TBMSMAETickets.Where(t => t.IdUsuarioTec == id).Count(x => x.IdEstado == 1);
            ViewBag.Asignados = db.TBMSMAETickets.Where(t => t.IdUsuarioTec == id).Count(x => x.IdEstado == 2);
            ViewBag.Atendidos = db.TBMSMAETickets.Where(t => t.IdUsuarioTec == id).Count(x => x.IdEstado == 3);
            System.Web.HttpContext.Current.Session["Inicio"] = "";
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers1.NombreCompleto.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower()) && x.IdUsuarioTec == id).ToList().ToPagedList(pageIndex, pageSize);
                System.Web.HttpContext.Current.Session["Inicio"] = Inicio;
            }
            return View(tBMSMAETickets);
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
            var id = System.Web.HttpContext.Current.Session["user"].ToString();
            System.Web.HttpContext.Current.Session["Inicio"] = "";
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers.Id == id && x.TBMSCATEstadosTickets.Descripcion.Contains("Asignado")).ToList().ToPagedList(pageIndex, pageSize);
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers1.NombreCompleto.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower()) && x.AspNetUsers.Id == id && x.TBMSCATEstadosTickets.Descripcion.Contains("Asignado")).ToList().ToPagedList(pageIndex, pageSize);
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
            return View(tBMSMAETickets);
        }

        public ActionResult ticketsAtendido(string Inicio, int? registro, int? pagina)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int pageSize = 10;
            pageSize = registro.HasValue ? Convert.ToInt32(registro) : 10;
            int pageIndex = 1;
            pageIndex = pagina.HasValue ? Convert.ToInt32(pagina) : 1;
            var id = System.Web.HttpContext.Current.Session["user"].ToString();
            System.Web.HttpContext.Current.Session["Inicio"] = "";
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers.Id == id && x.TBMSCATEstadosTickets.Descripcion.Contains("Atendido")).ToList().ToPagedList(pageIndex, pageSize);
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers1.NombreCompleto.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower()) && x.AspNetUsers.Id == id && x.TBMSCATEstadosTickets.Descripcion.Contains("Atendido")).ToList().ToPagedList(pageIndex, pageSize);
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

        public void DescargarExcelTicket()
        {
            var id = System.Web.HttpContext.Current.Session["user"].ToString();
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(t => t.IdUsuarioTec == id);
            string Inicio = System.Web.HttpContext.Current.Session["Inicio"].ToString();
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers1.NombreCompleto.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower()) && x.IdUsuarioTec == id);
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
            var id = System.Web.HttpContext.Current.Session["user"].ToString();
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.TBMSCATEstadosTickets.Descripcion == "Atendido").Where(t => t.IdUsuarioTec == id);
            string Inicio = System.Web.HttpContext.Current.Session["Inicio"].ToString();
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers1.NombreCompleto.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower()) && x.TBMSCATEstadosTickets.Descripcion == "Atendido" && x.IdUsuarioTec == id);
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
            var id = System.Web.HttpContext.Current.Session["user"].ToString();
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.TBMSCATEstadosTickets.Descripcion == "Asignado").Where(t => t.IdUsuarioTec == id);
            string Inicio = System.Web.HttpContext.Current.Session["Inicio"].ToString();
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers1.NombreCompleto.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower()) && x.TBMSCATEstadosTickets.Descripcion == "Asignado" && x.IdUsuarioTec == id);
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
            var iduser = System.Web.HttpContext.Current.Session["user"].ToString();
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.TBMSCATEstadosTickets.Descripcion == "Atendido" && x.idTicket == id && x.IdUsuarioTec == iduser);
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
            var iduser = System.Web.HttpContext.Current.Session["user"].ToString();
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.TBMSCATEstadosTickets.Descripcion == "Asignado" && x.idTicket == id && x.IdUsuarioTec == iduser);
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
        public ActionResult Reasignar([Bind(Exclude = "idUsuario,idPrioridad,IdEstado,idCategoria,idEspecialidad,titulo,descripcion,fechaTicket,estadoTicket,fechaAtendido,adjuntoTicket,idCalificacion", Include = "idTicket,IdUsuarioTec")] TBMSMAETickets tBMSMAETickets)
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
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuarioTec = new SelectList(db.AspNetUsers.Where(x => x.idEspecialidad != null), "Id", "NombreCompleto", tBMSMAETickets.IdUsuarioTec);
            ViewBag.Error = "Error en el sistema";
            return View(tBMSMAETickets);
        }
        public JsonResult GetNotificacion()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var id = System.Web.HttpContext.Current.Session["user"].ToString();
            var response = db.TBMSMAETickets.Where(x => x.IdUsuarioTec == id && x.IdEstado == 2).OrderByDescending(x => x.idPrioridad).ToList();
            //Actualiza session aqui para obtener solo el nuevo ticket notificacion
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Incidencia(int? id)
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

    }
}
