using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RacePuntos.Datos;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace RacePuntos.Controllers
{
    public class puntosController : Controller
    {
        private RacePuntosEntities db = new RacePuntosEntities();
        // GET: puntos
        public async Task<ActionResult> Index()
        {
            if (Session["USUARIO_LOGUEADO"] != null)
            {
                List<sp_RepHistAdqPuntos_Result> lstUsu = db.sp_RepHistAdqPuntos().ToList();
                Table ta = new Table();
                string TableHistorial1 = TableHistorial1T(ta, lstUsu);
                ViewData["_TableHistorial1"] = TableHistorial1;
                return View();
            }
            else
            {
                Response.Redirect("/Personas/Login");
                return null;
            }
        }

        public string TableHistorial1T(Table ta, List<sp_RepHistAdqPuntos_Result> lsHistory1)
        {
            string ListUsr = "";
            foreach (var item in lsHistory1)
            {
                ListUsr += "<tr>";
                ListUsr += "<td>" + item.documento + " </td>";
                ListUsr += "<td>" + item.nombres + " " + item.apellidos + "  </td>";
                ListUsr += "  <td>" + item.puntos_acumulados + "  </td>";
                ListUsr += " </tr>";
            }
            return ListUsr;
        }


        // GET: puntos
        public async Task<ActionResult> Index2()
        {
            if (Session["USUARIO_LOGUEADO"] != null)
            {
                ViewData["Servicios"] = VehService();
                List<sp_RepHistRedmPuntos_Result> lstHist2 = db.sp_RepHistRedmPuntos(null, null, null).ToList();
                ViewData["Parameters"] = "cDesde='" + null + "', cHasta='" + null + "', servicios='" + null + "'";
                Table ta = new Table();
                string TableHistorial2 = TableHistorial2T(ta, lstHist2);
                ViewData["_TableHistorial2"] = TableHistorial2;
                return View();
            }
            else
            {
                Response.Redirect("/Personas/Login");
                return null;
            }
        }

        [HttpPost]
        public HtmlString FiltroHist2(string cDesde = null, string cHasta = null, string servicios = null)
        {

            cDesde = (cDesde != "") ? cDesde : null;
            cHasta = (cHasta != "") ? cHasta : null;
            servicios = (servicios != "") ? servicios : null;

            List<sp_RepHistRedmPuntos_Result> lstHist2 = db.sp_RepHistRedmPuntos(cDesde, cHasta, servicios).ToList();
            Table ta = new Table();
            string TableHistorial2 = TableHistorial2T(ta, lstHist2);
            ViewData["Parameters"] = "cDesde='" + cDesde + "', cHasta='" + cHasta + "', servicios='" + servicios + "'";
            return new HtmlString(TableHistorial2);
        }

        public string VehService()
        {
            var Servicios = (from ser in db.servicios
                             select ser).ToList();
            string s_Service = "<select class='form-control' name='servicios' id='servicios'>";
            s_Service += "<option value=''>[SELECCIONE]</option>";
            foreach (var item in Servicios.ToList())
            {
                s_Service += "<option value='" + item.id_servicio + "'>" + item.nombre_servicio + "</option>";
            }
            s_Service += "</select>";
            return s_Service;
        }

        public string TableHistorial2T(Table ta, List<sp_RepHistRedmPuntos_Result> lsHistory2)
        {
            string ListHist2 = "";
            foreach (var item in lsHistory2)
            {
                DateTime fecha = DateTime.Parse(item.fecha_reserva.ToString());
                ListHist2 += "<tr>";
                ListHist2 += "<td>" + item.documento + " </td>";
                ListHist2 += "<td>" + item.Nombre + "  </td>";
                ListHist2 += "<td>" + item.nombre_servicio + "  </td>";
                ListHist2 += "<td>" + fecha.ToShortDateString() + "  </td>";
                ListHist2 += "</tr>";
            }
            return ListHist2;
        }

        public ActionResult Report(string id, string rdlc, string NameDataSet, string cDesde, string cHasta, string servicios)
        {
            cDesde = (cDesde != "" && cDesde != "null") ? cDesde : null;
            cHasta = (cHasta != "" && cHasta != "null") ? cHasta : null;
            servicios = (servicios != "" && servicios != "null") ? servicios : null;
            LocalReport lr = new LocalReport();

            string path = Path.Combine(Server.MapPath("~/ReportViewer"), rdlc + ".rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<sp_RepHistRedmPuntos_Result> cm = new List<sp_RepHistRedmPuntos_Result>();
            using (RacePuntosEntities dc = new RacePuntosEntities())
            {
                cm = dc.sp_RepHistRedmPuntos(cDesde, cHasta, servicios).ToList();
            }
            ReportDataSource rd = new ReportDataSource(NameDataSet, cm);
            lr.DataSources.Add(rd);
            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("Fecha_I", cDesde);
            parameters[1] = new ReportParameter("Fecha_F", cHasta);
            parameters[2] = new ReportParameter("Servicios", servicios);
            lr.SetParameters(parameters);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
