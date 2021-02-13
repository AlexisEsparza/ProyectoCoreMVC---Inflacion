using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoWEB1.Models;

namespace ProyectoWEB1.Controllers
{
    public class MaterialesController : Controller
    {
        private dbProyectoWebEntities db = new dbProyectoWebEntities();

        // GET: Materiales
        [Authorize]
        public ActionResult Index()
        {
            return View(db.tblMateriales.ToList());
        }

        // GET: Materiales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMateriales tblMateriales = db.tblMateriales.Find(id);
            if (tblMateriales == null)
            {
                return HttpNotFound();
            }
            return View(tblMateriales);
        }

        // GET: Materiales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Materiales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMaterial,NombreMaterial,PrecioMaterial")] tblMateriales tblMateriales)
        {
            if (ModelState.IsValid)
            {
                db.tblMateriales.Add(tblMateriales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblMateriales);
        }

        // GET: Materiales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMateriales tblMateriales = db.tblMateriales.Find(id);
            if (tblMateriales == null)
            {
                return HttpNotFound();
            }
            return View(tblMateriales);
        }

        // POST: Materiales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMaterial,NombreMaterial,PrecioMaterial")] tblMateriales tblMateriales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblMateriales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblMateriales);
        }

        // GET: Materiales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMateriales tblMateriales = db.tblMateriales.Find(id);
            if (tblMateriales == null)
            {
                return HttpNotFound();
            }
            return View(tblMateriales);
        }

        // POST: Materiales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblMateriales tblMateriales = db.tblMateriales.Find(id);
            db.tblMateriales.Remove(tblMateriales);
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
