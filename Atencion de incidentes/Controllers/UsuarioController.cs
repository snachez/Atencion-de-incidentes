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
using PagedList;
using System.IO;

namespace Atencion_de_incidentes.Controllers
{
    [Authorize(Roles = "Empleado")]
    public class UsuarioController : Controller
    {
        private ModeloUnedMS db = new ModeloUnedMS();
        // GET: Usuario
        public ActionResult Index(string Inicio, int? registro, int? pagina)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int pageSize = 10;
            pageSize = registro.HasValue ? Convert.ToInt32(registro) : 10;
            int pageIndex = 1;
            pageIndex = pagina.HasValue ? Convert.ToInt32(pagina) : 1;
            var id = System.Web.HttpContext.Current.Session["user"].ToString();
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(t => t.AspNetUsers1.Id == id).ToList().ToPagedList(pageIndex, pageSize);
            ViewBag.Incidencia = db.TBMSMAETickets.Where(t => t.AspNetUsers1.Id == id).Count(x => x.IdEstado == 1);
            ViewBag.Asignados = db.TBMSMAETickets.Where(t => t.AspNetUsers1.Id == id).Count(x => x.IdEstado == 2);
            ViewBag.Atendidos = db.TBMSMAETickets.Where(t => t.AspNetUsers1.Id == id).Count(x => x.IdEstado == 3);
            System.Web.HttpContext.Current.Session["Inicio"] = "";
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.TBMSCATCategorias.tipoCategoria.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower()) && x.AspNetUsers1.Id == id).ToList().ToPagedList(pageIndex, pageSize);
                System.Web.HttpContext.Current.Session["Inicio"] = Inicio;
            }
            return View(tBMSMAETickets);
        }

        public JsonResult GetlistaEspecialidades(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var entidad = new SelectList(db.TBMSESPECIALIDADXCATEGORIAs.Include(x => x.TBMSMAEEspecialidades).Where(x => x.IdCategoria == id), "TBMSMAEEspecialidades.idEspecialidad", "TBMSMAEEspecialidades.tipoEspecialidad");
            return Json(entidad, JsonRequestBehavior.AllowGet);
        }
        // GET: TBMSMAETickets/Create
        public ActionResult CrearTicket()
        {
            System.Threading.Thread.Sleep(2000);
            ViewBag.IdUsuarioTec = new SelectList(db.AspNetUsers, "Id", "Address");
            ViewBag.idUsuario = new SelectList(db.AspNetUsers, "Id", "Address");
            ViewBag.idCalificacion = new SelectList(db.TBMCALICalificacion, "idCalificacion", "comentario");
            ViewBag.idCategoria = new SelectList(db.TBMSCATCategorias, "idCategoria", "tipoCategoria");
            ViewBag.IdEstado = new SelectList(db.TBMSCATEstadosTickets, "IdEstado", "Descripcion");
            ViewBag.idEspecialidad = new SelectList(db.TBMSMAEEspecialidades, "idEspecialidad", "tipoEspecialidad");
            ViewBag.idPrioridad = new SelectList(db.TBMSMAEPrioridades, "idPrioridad", "nombrePrioridad");
            return View();
        }

        // POST: TBMSMAETickets/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearTicket([Bind(Include = "idCategoria,idEspecialidad,titulo,descripcion,adjuntoTicket")] TBMSMAETickets tBMSMAETickets, HttpPostedFileBase file)
        {
            var respuesta = new ResponseModel
            {
                respuesta = true,
                redirect = "/Usuario/Index",
                error = "",
                mensaje = "El ticket fue creado correctamente!"
            };
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/archivos"), file.FileName);
                        file.SaveAs(path);
                        var id = System.Web.HttpContext.Current.Session["user"].ToString();
                        db.Database.ExecuteSqlCommand("CrearTicket @idUsuario ='" + id + "'," +
                            " @IdEstado='" + 2 + "', @idCategoria='" + tBMSMAETickets.idCategoria + "', " +
                            "@idEspecialidad='" + tBMSMAETickets.idEspecialidad + "', @titulo='" + tBMSMAETickets.titulo + "', " +
                            "@descripcion='" + tBMSMAETickets.descripcion + "', @fechaTicket='" + DateTime.Now + "'," +
                            " @estadoTicket='" + 1 + "', @adjuntoTicket='" + file.FileName + "'");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var id = System.Web.HttpContext.Current.Session["user"].ToString();
                        db.Database.ExecuteSqlCommand("CrearTicket @idUsuario ='" + id + "'," +
                            " @IdEstado='" + 2 + "', @idCategoria='" + tBMSMAETickets.idCategoria + "', " +
                            "@idEspecialidad='" + tBMSMAETickets.idEspecialidad + "', @titulo='" + tBMSMAETickets.titulo + "', " +
                            "@descripcion='" + tBMSMAETickets.descripcion + "', @fechaTicket='" + DateTime.Now + "'," +
                            " @estadoTicket='" + 1 + "', @adjuntoTicket='" + null + "'");
                        return RedirectToAction("Index");
                    }

                }
                catch
                {
                    respuesta = new ResponseModel
                    {
                        respuesta = false,
                        redirect = "",
                        error = "Se produjo un error al crear el ticket!",
                        mensaje = ""
                    };
                }

            }

            ViewBag.IdUsuarioTec = new SelectList(db.AspNetUsers, "Id", "Address", tBMSMAETickets.IdUsuarioTec);
            ViewBag.idUsuario = new SelectList(db.AspNetUsers, "Id", "Address", tBMSMAETickets.idUsuario);
            ViewBag.idCalificacion = new SelectList(db.TBMCALICalificacion, "idCalificacion", "comentario", tBMSMAETickets.idCalificacion);
            ViewBag.idCategoria = new SelectList(db.TBMSCATCategorias, "idCategoria", "tipoCategoria", tBMSMAETickets.idCategoria);
            ViewBag.IdEstado = new SelectList(db.TBMSCATEstadosTickets, "IdEstado", "Descripcion", tBMSMAETickets.IdEstado);
            ViewBag.idEspecialidad = new SelectList(db.TBMSMAEEspecialidades, "idEspecialidad", "tipoEspecialidad", tBMSMAETickets.idEspecialidad);
            ViewBag.idPrioridad = new SelectList(db.TBMSMAEPrioridades, "idPrioridad", "nombrePrioridad", tBMSMAETickets.idPrioridad);
            return Json(respuesta);
        }

        public ActionResult Editarticket(int? id)
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
            ViewBag.Categoria = new SelectList(db.TBMSCATCategorias, "idCategoria", "tipoCategoria");
            ViewBag.idTicket = tBMSMAETickets.idTicket;
            return View();
        }

        // POST: TBMSMAETickets/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editarticket([Bind(Exclude = "idUsuario,IdUsuarioTec,idPrioridad,IdEstado,idEspecialidad,fechaTicket,estadoTicket,fechaAtendido,adjuntoTicket,idCalificacion,Solucion", Include = "idTicket,idCategoria,titulo,descripcion")] TBMSMAETickets tBMSMAETickets)
        {
            if (tBMSMAETickets.titulo != null || tBMSMAETickets.descripcion != null || tBMSMAETickets.idCategoria != null)
            {
                TBMSMAETickets tBMSMAE = db.TBMSMAETickets.Where(x => x.idTicket == tBMSMAETickets.idTicket).FirstOrDefault();
                tBMSMAE.titulo = tBMSMAETickets.titulo;
                tBMSMAE.descripcion = tBMSMAETickets.descripcion;
                tBMSMAE.idCategoria = tBMSMAETickets.idCategoria;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            var tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(t => t.AspNetUsers1.Id == id);
            string Inicio = System.Web.HttpContext.Current.Session["Inicio"].ToString();
            if (!String.IsNullOrEmpty(Inicio))
            {
                tBMSMAETickets = db.TBMSMAETickets.Include(t => t.AspNetUsers).Include(t => t.AspNetUsers1).Include(t => t.TBMCALICalificacion).Include(t => t.TBMSCATCategorias).Include(t => t.TBMSCATEstadosTickets).Include(t => t.TBMSMAEEspecialidades).Include(t => t.TBMSMAEPrioridades).Where(x => x.AspNetUsers1.NombreCompleto.ToLower().Contains(Inicio.ToLower()) || x.titulo.ToLower().Contains(Inicio.ToLower()) && x.AspNetUsers1.Id == id);
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

        public ActionResult Calificar(int? id)
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
            ViewBag.Tecnico = tBMSMAETickets.AspNetUsers.NombreCompleto;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Calificar(TBMSMAETickets tBMSMAETickets)
        {
            var respuesta = new ResponseModel
            {
                respuesta = true,
                redirect = "/Usuario/Index",
                error = "",
                mensaje = "Se guardo correctamente la calificacion!"
            };
            if (!ModelState.IsValid)
            {
                try
                {
                    TBMCALICalificacion cali = new TBMCALICalificacion();
                    cali.comentario = tBMSMAETickets.TBMCALICalificacion.comentario;
                    cali.valoracion = tBMSMAETickets.TBMCALICalificacion.valoracion;
                    db.TBMCALICalificacion.Add(cali);
                    var idcalificacion = db.TBMCALICalificacion.OrderByDescending(x => x.idCalificacion).FirstOrDefault().idCalificacion;
                    TBMSMAETickets tBMSMAE = db.TBMSMAETickets.Where(x => x.idTicket == tBMSMAETickets.idTicket).FirstOrDefault();
                    tBMSMAE.idCalificacion = idcalificacion;
                    db.SaveChanges();
                }
                catch
                {
                    respuesta = new ResponseModel
                    {
                        respuesta = false,
                        redirect = "",
                        error = "Se produjo un error al guardar la calificacion!",
                        mensaje = ""
                    };
                }

            }
            return Json(respuesta);
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
    }
}
