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

namespace DA2_SistemaEscolar2015_2.Controllers
{
    public class GrupoController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Grupo
        [Authorize]
        public ActionResult Index()
        {
            var grupos = db.grupos.Include(g => g.carrera);
            return View(grupos.ToList());
        }

        // GET: Grupo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // GET: Grupo/Create
        public ActionResult Create()
        {
            ViewBag.carreraID = new SelectList(db.carreras, "carreraID", "nombre");
            return View();
        }

        // POST: Grupo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "grupoID,nombre,carreraID")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.grupos.Add(grupo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.carreraID = new SelectList(db.carreras, "carreraID", "nombre", grupo.carreraID);
            return View(grupo);
        }

        // GET: Grupo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            ViewBag.carreraID = new SelectList(db.carreras, "carreraID", "nombre", grupo.carreraID);
            return View(grupo);
        }

        // POST: Grupo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "grupoID,nombre,carreraID")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.carreraID = new SelectList(db.carreras, "carreraID", "nombre", grupo.carreraID);
            return View(grupo);
        }

        // GET: Grupo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // POST: Grupo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grupo grupo = db.grupos.Find(id);
            db.grupos.Remove(grupo);
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
