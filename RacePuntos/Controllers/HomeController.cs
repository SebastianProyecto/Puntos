using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RacePuntos.Controllers {
	public class HomeController : Controller {
		public ActionResult Index() {
			if(Session["USUARIO_LOGUEADO"] != null) {
				return View();
			} else {
				Response.Redirect("/Personas/Login");
				return null;
			}

		}

		public ActionResult QuienesSomos() {
			if(Session["USUARIO_LOGUEADO"] != null) {
				ViewBag.Message = "Your application description page.";
				return View();
			} else {
				Response.Redirect("/Personas/Login");
				return null;
			}
		}

		public ActionResult Puntos() {
			if(Session["USUARIO_LOGUEADO"] != null) {
				ViewBag.Message = "Your application description page.";
				return View();
			} else {
				Response.Redirect("/Personas/Login");
				return null;
			}
		}

		public ActionResult Gestion() {
			if(Session["USUARIO_LOGUEADO"] != null) {
				ViewBag.Message = "Your application description page.";
				return View();
			} else {
				Response.Redirect("/Personas/Login");
				return null;
			}
		}

		public ActionResult Ayuda() {
			if(Session["USUARIO_LOGUEADO"] != null) {
				ViewBag.Message = "Your application description page.";
				return View();
			} else {
				Response.Redirect("/Personas/Login");
				return null;
			}
		}

		public ActionResult Contact() {
			if(Session["USUARIO_LOGUEADO"] != null) {
				ViewBag.Message = "Your application description page.";
				return View();
			} else {
				Response.Redirect("/Personas/Login");
				return null;
			}
		}
	}
}