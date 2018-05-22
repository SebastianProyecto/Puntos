using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using RacePuntos.Datos;

namespace RacePuntos.Controllers
{
    public class vehiculoController : Controller
    {
        private RacePuntosEntities db = new RacePuntosEntities();

        // GET: vehiculo
        public ActionResult Index()
        {
            var vehiculo = db.vehiculo.Include(v => v.detalle_vehiculos).Include(v => v.personas);
            return View();
        }

        // GET: vehiculo/Create
        public ActionResult Create()
        {
            ViewBag.codigo_vehiculo = new SelectList(db.detalle_vehiculos, "codigo", "marca");
            ViewBag.documento_usuario = new SelectList(db.personas, "documento", "tipo_documento");
            return View();
        }

        // POST: vehiculo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string documento_usuario, string placa_vehiculo_cliente, string marca_vehiculo, string referencia_vehiculo, string cilindraje_vehiculo)
        {
            if (ModelState.IsValid)
            {
                int result = db.vehiculo.Where(c => c.documento_usuario == documento_usuario && c.placa_vehiculo_cliente == placa_vehiculo_cliente ).Count();
                if (result == 0)
                {
                    db.spInsertVehiculo(documento_usuario, placa_vehiculo_cliente, marca_vehiculo, referencia_vehiculo, cilindraje_vehiculo).ToString();
                    TempData["Mensaje"] = "0~Vehiculo Creado con exito";
                }
                else
                {
                    TempData["Mensaje"] = "1~Vehiculo ya existe, verifique";
                }
                
                Response.Redirect("/Personas/Create");
                return null;
            }
            return null;
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
