using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RacePuntos.Datos;

namespace RacePuntos.Controllers {
	public class personasController : Controller {
		private RacePuntosEntities db = new RacePuntosEntities();

		// GET: personas
		public ActionResult Index() {
			var personas = db.personas.Include(p => p.cargos);
			return View(personas.ToList());
		}

		// GET: personas/Details/5
		public ActionResult Details(string id) {
			if(id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			personas personas = db.personas.Find(id);
			if(personas == null) {
				return HttpNotFound();
			}
			return View(personas);
		}

		// GET: personas/Create
		public ActionResult Create() {
			ViewBag.cargo = new SelectList(db.cargos, "id_cargo", "nombre_cargo");
			return View();
		}

		// POST: personas/Create
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "tipo_documento,documento,contrasena,cargo,rol,id_usuario_creacion,nombres,apellidos,fecha_nacimiento,direccion,numero_celular")] personas personas) {
			//try {
			if(ModelState.IsValid) {
				int ValidateInsert = db.personas.Where(x => x.documento == personas.documento).Count();

				if(ValidateInsert == 0) {
					string id_usuario = db.personas.Select(x => x.id_usuario_creacion).Max();
					id_usuario = (id_usuario == null) ? "1" : id_usuario;
					personas.id_usuario_creacion = id_usuario;
					personas.rol = "USUARIO";
					personas.cargo = "100";
					db.personas.Add(personas);
					db.SaveChanges();
					ViewBag.Message = "1";
					Response.Redirect("Create");
					return null;
				} else {
					ViewBag.Message = "0";
					Response.Redirect("Create");
					return null;
				}

			}

			ViewBag.cargo = new SelectList(db.cargos, "id_cargo", "nombre_cargo", personas.cargo);
			return View(personas);
			//} catch(Exception exc) {
			//	throw new Exception("Erro: " + exc);
			//}
		}

		// GET: personas/Edit/5
		public ActionResult Edit(string id) {
			if(id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			personas personas = db.personas.Find(id);
			if(personas == null) {
				return HttpNotFound();
			}
			ViewBag.cargo = new SelectList(db.cargos, "id_cargo", "nombre_cargo", personas.cargo);
			return View(personas);
		}

		// POST: personas/Edit/5
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "tipo_documento,documento,contrasena,rol,cargo,id_usuario_creacion,nombres,apellidos,fecha_nacimiento,direccion,numero_celular")] personas personas) {
			if(ModelState.IsValid) {
				db.Entry(personas).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.cargo = new SelectList(db.cargos, "id_cargo", "nombre_cargo", personas.cargo);
			return View(personas);
		}

		// GET: personas/Delete/5
		public ActionResult Delete(string id) {
			if(id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			personas personas = db.personas.Find(id);
			if(personas == null) {
				return HttpNotFound();
			}
			return View(personas);
		}

		// POST: personas/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(string id) {
			personas personas = db.personas.Find(id);
			db.personas.Remove(personas);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		//
		// GET: /Account/Login
		[AllowAnonymous]
		public ActionResult Login() {
			return View();
		}

		//
		// POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login([Bind(Include = "documento,contrasena")] personas personas) {
			if(!ModelState.IsValid) {
				Response.Redirect("Login");
				return null;
			}

			int ValidateLogin = db.personas.Where(x => (x.documento == personas.documento && x.contrasena == personas.contrasena)).Count();

			if(ValidateLogin > 0) {

				Response.Redirect("../Manage/Index");
				return null;
			}
			Response.Redirect("Login");
			return null;
		}

		protected override void Dispose(bool disposing) {
			if(disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
