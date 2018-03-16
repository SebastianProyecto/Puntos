using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RacePuntos.Controllers {
	public class HomeController : Controller {

		public ActionResult Index() {
			return View();
		}

		public ActionResult QuienesSomos() {
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Puntos() {
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Gestion() {
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Ayuda() {
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact() {
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}