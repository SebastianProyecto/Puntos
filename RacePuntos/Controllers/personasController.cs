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

namespace RacePuntos.Controllers {
	public class personasController : Controller {
		private RacePuntosEntities db = new RacePuntosEntities();

		// GET: personas
		public async Task<ActionResult> Index() {
			var personas = db.personas.Include(p => p.cargos);
			return View(await personas.ToListAsync());
		}

		// GET: personas/Details/5
		public async Task<ActionResult> Details(string id) {
			if(id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			personas personas = await db.personas.FindAsync(id);
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
		public async Task<ActionResult> Create([Bind(Include = "tipo_documento,documento,contrasena,rol,cargo,id_usuario_creacion,nombres,apellidos,fecha_nacimiento,direccion,numero_celular,correoElectronico")] personas personas) {
			if(ModelState.IsValid) {
				db.personas.Add(personas);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			ViewBag.cargo = new SelectList(db.cargos, "id_cargo", "nombre_cargo", personas.cargo);
			return View(personas);
		}

		// GET: personas/Edit/5
		public async Task<ActionResult> Edit(string id) {
			if(id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			personas personas = await db.personas.FindAsync(id);
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
		public async Task<ActionResult> Edit([Bind(Include = "tipo_documento,documento,contrasena,rol,cargo,id_usuario_creacion,nombres,apellidos,fecha_nacimiento,direccion,numero_celular,correoElectronico")] personas personas) {
			if(ModelState.IsValid) {
				db.Entry(personas).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewBag.cargo = new SelectList(db.cargos, "id_cargo", "nombre_cargo", personas.cargo);
			return View(personas);
		}

		// GET: personas/Delete/5
		public async Task<ActionResult> Delete(string id) {
			if(id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			personas personas = await db.personas.FindAsync(id);
			if(personas == null) {
				return HttpNotFound();
			}
			return View(personas);
		}

		// POST: personas/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(string id) {
			personas personas = await db.personas.FindAsync(id);
			db.personas.Remove(personas);
			await db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		//
		// GET: /personas/Login
		[AllowAnonymous]
		public ActionResult Login() {
			//ViewData["Rol"] = "";
			return View();
		}

		//
		// POST: /personas/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login([Bind(Include = "tipo_documento,documento,contrasena,rol,cargo,id_usuario_creacion,nombres,apellidos,fecha_nacimiento,direccion,numero_celular,correoElectronico")] personas personas) {
			if(!ModelState.IsValid) {
				Response.Redirect("Login");
				return null;
			}



			//var pass = Security.ParseHexString(personas.contrasena);
			var val = db.logeo_persona(personas.documento.ToString(), personas.contrasena.ToString()).ToList();
			/*int ValidateLogin = db.personas.Where(x => (x.documento == personas.documento && x.contrasena == pass)).Count();

			if(ValidateLogin > 0) {
				TempData["Rol"] = db.personas.Where(c => c.documento == personas.documento).First().rol;
				Response.Redirect("~/Home/Index");
				return null;
			}*/
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
