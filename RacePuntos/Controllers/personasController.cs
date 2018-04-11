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
			if(Session["USUARIO_LOGUEADO"] != null) {
				var personas = db.personas.Include(p => p.cargos);
				return View(await personas.ToListAsync());
			} else {
				Response.Redirect("/Personas/Login");
				return null;
			}
		}

		// GET: personas/Details/5
		public async Task<ActionResult> Details(string id) {
			if(Session["USUARIO_LOGUEADO"] != null) {
				if(id == null) {
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
				personas personas = await db.personas.FindAsync(id);
				if(personas == null) {
					return HttpNotFound();
				}
				return View(personas);
			} else {
				Response.Redirect("/Personas/Login");
				return null;
			}
		}

		// GET: personas/Create
		public ActionResult Create() {
			if(Session["USUARIO_LOGUEADO"] != null) {
				ViewBag.cargo = new SelectList(db.cargos, "id_cargo", "nombre_cargo");
				return View();
			} else {
				Response.Redirect("/Personas/Login");
				return null;
			}
		}

		// POST: personas/Create
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(string tipo_documento, string documento, string contrasena, string rol, string cargo, string id_usuario_creacion, string nombres, string apellidos, string fecha_nacimiento, string direccion, string numero_celular, string correoElectronico) {
			if(Session["USUARIO_LOGUEADO"] != null) {
				TempData["Mensaje"] = "";
				if(ModelState.IsValid) {
					int usr = db.personas.Where(c => c.documento == documento).Count();
					if(usr == 0) {
						db.registro_persona(tipo_documento, documento, contrasena, rol, cargo, id_usuario_creacion, nombres, apellidos, fecha_nacimiento, direccion, numero_celular, correoElectronico).ToString();
						TempData["Mensaje"] = "0~Usuario Creado con exito";
					}else {
						TempData["Mensaje"] = "1~Usuario ya existe, verifique";
					}

					//db.personas.Add(personas);
					//await db.SaveChangesAsync();
					Response.Redirect("/Personas/Create");
					return null;
				}

				ViewBag.cargo = new SelectList(db.cargos, "id_cargo", "nombre_cargo", cargo);
				Response.Redirect("/Personas/Create");
				return null;
			} else {
				Response.Redirect("/Personas/Login");
				return null;
			}
		}

		// GET: personas/Edit/5
		public async Task<ActionResult> Edit(string id) {
			if(Session["USUARIO_LOGUEADO"] != null) {
				if(id == null) {
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
				personas personas = await db.personas.FindAsync(id);
				if(personas == null) {
					return HttpNotFound();
				}
				ViewBag.cargo = new SelectList(db.cargos, "id_cargo", "nombre_cargo", personas.cargo);
				return View(personas);
			} else {
				Response.Redirect("/Personas/Login");
				return null;
			}
		}

		// POST: personas/Edit/5
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "tipo_documento,documento,contrasena,rol,cargo,id_usuario_creacion,nombres,apellidos,fecha_nacimiento,direccion,numero_celular,correoElectronico")] personas personas) {
			if(Session["USUARIO_LOGUEADO"] != null) {
				if(ModelState.IsValid) {
					db.Entry(personas).State = EntityState.Modified;
					await db.SaveChangesAsync();
					return RedirectToAction("Index");
				}
				ViewBag.cargo = new SelectList(db.cargos, "id_cargo", "nombre_cargo", personas.cargo);
				return View(personas);
			} else {
				Response.Redirect("/Personas/Login");
				return null;
			}
		}

		// GET: personas/Delete/5
		public async Task<ActionResult> Delete(string id) {
			if(Session["USUARIO_LOGUEADO"] != null) {
				if(id == null) {
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
				personas personas = await db.personas.FindAsync(id);
				if(personas == null) {
					return HttpNotFound();
				}
				return View(personas);
			} else {
				Response.Redirect("/Personas/Login");
				return null;
			}
		}

		// POST: personas/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(string id) {
			if(Session["USUARIO_LOGUEADO"] != null) {
				personas personas = await db.personas.FindAsync(id);
				db.personas.Remove(personas);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			} else {
				Response.Redirect("/Personas/Login");
				return null;
			}
		}

		// GET: personas/servicios
		public async Task<ActionResult> Servicios() {
			if(Session["USUARIO_LOGUEADO"] != null) {
				return View();
			} else {
				Response.Redirect("/Personas/Login");
				return null;
			}
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
		public ActionResult Login(string documento, string contrasena) {
			try {
				if(!ModelState.IsValid) {
					Response.Redirect("Login");
					return null;
				}
				TempData["Error"] = "";
				var ValidateLogin = db.logeo_persona(documento.ToString(), contrasena.ToString()).Count();


				if(ValidateLogin > 0) {
					personas User = new personas {
						documento = documento,
						nombres = db.personas.Where(c => c.documento == documento).First().nombres,
						apellidos = db.personas.Where(c => c.documento == documento).First().apellidos
					};
					Session["USUARIO_LOGUEADO"] = User;

					Session["DOCUMENTO"] = db.personas.Where(c => c.documento == documento).First().documento;
					Session["NOMBRES"] = db.personas.Where(c => c.documento == documento).First().nombres;
					Session["APELLIDOS"] = db.personas.Where(c => c.documento == documento).First().apellidos;
					Session["ROL"] = db.personas.Where(c => c.documento == documento).First().rol;
					Session["CARGO"] = db.personas.Where(c => c.documento == documento).First().cargo;

					Response.Redirect("/Home/Index");
					return null;
				} else {
					TempData["Error"] = "Datos incorrectos, verifique.";
					Response.Redirect("~/Personas/Login");
					return null;
				}
			} catch(Exception ex) {

			} finally {
				db.Dispose();
			}
			return null;

		}

		//protected override void Dispose(bool disposing) {
		//	if(disposing) {
		//		db.Dispose();
		//	}
		//	base.Dispose(disposing);
		//}
	}
}
