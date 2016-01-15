using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DA2_SistemaEscolar2015.DAL;
using DA2_SistemaEscolar2015.Models;
using System.Web.Script.Serialization;

namespace DA2_SistemaEscolar2015_2.Controllers
{
    public class AlumnoController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult PruebaAjax()
        {
            return View();
        }

        public JsonResult EntregarDatos()
        {
            //var listaJson = from alumno in 
            //return Json(db.carreras.ToList(),JsonRequestBehavior.AllowGet);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            String dato = "Esto viene del server";
            return Json(jss.Serialize(dato), JsonRequestBehavior.AllowGet);
        }

        // GET: Alumno
        //Valor 
        [Authorize(Roles = "Administrador, Capturista")]
        public ActionResult Index(String strBuscado="")
        {
            //Se declara una lista de alumnos
            IEnumerable<Alumno> alumnos;

            //Se busca una cadena de caracteres por nombre
            alumnos = db.alumnos.Where(algo => algo.nombre.Contains(strBuscado));

            //Recursos de Vista
            ViewBag.grupoID = new SelectList(db.grupos, "grupoID", "nombre");

            //Se envia datos principales a vista
            return View(alumnos.ToList());
        }

        // GET: Alumno/Details/5
        [Authorize(Roles = "Administrador, Capturista")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = db.alumnos.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        // GET: Alumno/Details/5
        [Authorize(Roles = "Administrador, Capturista")]
        public JsonResult AjaxDetails(int? id)
        {
            Alumno alumno = db.alumnos.Find(id);
            VMAlumno vmAlumno = new VMAlumno(alumno);

            return Json(vmAlumno, JsonRequestBehavior.AllowGet);
        }

        // GET: Alumno/Details/5
        [HttpPost]
        [Authorize(Roles = "Administrador, Capturista")]
        public JsonResult AjaxDetails(Alumno alumno)
        {
            db.Entry(alumno).State = EntityState.Modified;
            db.SaveChanges();

            VMAlumno vmAlumno = new VMAlumno(alumno);

            return Json(vmAlumno, JsonRequestBehavior.AllowGet);
        }

        // GET: Alumno/Create
        [Authorize(Roles = "Administrador, Capturista")]
        public ActionResult Create()
        {
            ViewBag.grupoID = new SelectList(db.grupos, "grupoID", "nombre");
            return View();
        }

        // POST: Alumno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Capturista")]
        public ActionResult Create([Bind(Include = "noMatricula,nombre,apellidoP,apellidoM,fechaNac,grupoID")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                db.alumnos.Add(alumno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.grupoID = new SelectList(db.grupos, "grupoID", "nombre", alumno.grupoID);
            return View(alumno);
        }

        // GET: Alumno/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            //User.Identity.
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = db.alumnos.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            ViewBag.grupoID = new SelectList(db.grupos, "grupoID", "nombre", alumno.grupoID);
            return View(alumno);
        }

        // POST: Alumno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "noMatricula,nombre,apellidoP,apellidoM,fechaNac,grupoID")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alumno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //Si llegaste hasta aca, es porque algo anda mal
            ViewBag.grupoID = new SelectList(db.grupos, "grupoID", "nombre", alumno.grupoID);
            return View(alumno);
        }

        [HttpGet]
        public JsonResult AjaxEdit(int noMatricula = 0)
        {
            /*Un objeto instanciado del modelo de datos*/
            Alumno alumno = db.alumnos.Find(noMatricula);

            /*Necesito una instancia del modelo de vista*/
            VMAlumno vmAlumno = new VMAlumno(alumno);

            return Json(vmAlumno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AjaxEdit(Alumno alumno)
        {
            String mensaje = String.Empty;

            try
            {
                db.Entry(alumno).State = EntityState.Modified;
                int c = db.SaveChanges();
                mensaje = "Se ha editado los datos del alumno satisfactoriamente";
            }
            catch(Exception exc)
            {
                mensaje = "Hubo un error en el servidor: "+exc.Message;
            }


            return Json(new { mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }










        /*
        [HttpPost]
        public JsonResult AjaxEdit(Alumno alumno)
        {
            String mensaje = String.Empty;
            int codigo;
            try{
                db.Entry(alumno).State = EntityState.Modified;
                db.SaveChanges();
                mensaje = "Los datos del alumno han sido editados satisfactoriamente";
                codigo = 1;
            }
            catch(Exception exc){
                mensaje = "Hubo un error: "+exc.Message;
                codigo = 0;
            }

            return Json(new { mensaje = mensaje, codigo = codigo }, JsonRequestBehavior.AllowGet);
        }*/

        // GET: Alumno/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = db.alumnos.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            //Si llego hasta aca, es que todo esta bien
            return View(alumno);
        }

        // POST: Alumno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            Alumno alumno = db.alumnos.Find(id);
            db.alumnos.Remove(alumno);
            db.SaveChanges();
            return RedirectToAction("Index");
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
