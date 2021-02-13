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
    public class RompimientosController : Controller
    {
        private dbProyectoWebEntities db = new dbProyectoWebEntities();

        // GET: Rompimientos
        public ActionResult Index()
        {
            var tblArchivoProyecto_Materiales = db.tblArchivoProyecto_Materiales.Include(t => t.tblArchivoProyecto).Include(t => t.tblMateriales);
            return View(tblArchivoProyecto_Materiales.ToList());
        }

        // GET: Rompimientos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblArchivoProyecto_Materiales tblArchivoProyecto_Materiales = db.tblArchivoProyecto_Materiales.Find(id);
            if (tblArchivoProyecto_Materiales == null)
            {
                return HttpNotFound();
            }
            return View(tblArchivoProyecto_Materiales);
        }

        // GET: Rompimientos/Create
        public ActionResult Create(int? id)
        {
            ViewBag.IdArchivoProyectoFK = new SelectList(db.tblArchivoProyecto.Where(p => p.IdArchivoProyecto == id), "IdArchivoProyecto", "NombreArchivo");
            //ViewBag.IdArchivoProyectoFKdsy = db.tblArchivoProyecto.Where(p => p.IdArchivoProyecto == id).First().NombreArchivo;
            ViewBag.IdMaterialFK = new SelectList(db.tblMateriales, "IdMaterial", "NombreMaterial");
            ViewBag.IdArchivoProyecto = id;
            return View();
        }

        // POST: Rompimientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArchivoProyecto_Materiales,Cantidad,IdArchivoProyectoFK,IdMaterialFK")] tblArchivoProyecto_Materiales tblArchivoProyecto_Materiales)
        {
            int idArchivoProyecto = tblArchivoProyecto_Materiales.IdArchivoProyectoFK;
            int idMaterial = tblArchivoProyecto_Materiales.IdMaterialFK;
            decimal precioMat = db.tblMateriales.Where(p => p.IdMaterial == idMaterial).First().PrecioMaterial;
            tblArchivoProyecto_Materiales.PrecioParcial = tblArchivoProyecto_Materiales.Cantidad * precioMat;
            if (ModelState.IsValid)
            {
                db.tblArchivoProyecto_Materiales.Add(tblArchivoProyecto_Materiales);
                db.SaveChanges();
                return RedirectToAction("Edit","ArchivoProyectos", new { id = idArchivoProyecto });
            }

            ViewBag.IdArchivoProyectoFK = new SelectList(db.tblArchivoProyecto, "IdArchivoProyecto", "NombreArchivo", tblArchivoProyecto_Materiales.IdArchivoProyectoFK);
            ViewBag.IdMaterialFK = new SelectList(db.tblMateriales, "IdMaterial", "NombreMaterial", tblArchivoProyecto_Materiales.IdMaterialFK);
            return View(tblArchivoProyecto_Materiales);
        }

        // GET: Rompimientos/Edit/5
        public ActionResult Edit(int? id, int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblArchivoProyecto_Materiales tblArchivoProyecto_Materiales = db.tblArchivoProyecto_Materiales.Find(id);
            if (tblArchivoProyecto_Materiales == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdArchivoProyecto = id2;
            ViewBag.IdArchivoProyectoFK = db.tblArchivoProyecto.Where(p => p.IdArchivoProyecto == id2).First().NombreArchivo;
            //ViewBag.IdArchivoProyectoFK = new SelectList(db.tblArchivoProyecto, "IdArchivoProyecto", "NombreArchivo", tblArchivoProyecto_Materiales.IdArchivoProyectoFK);
            ViewBag.IdMaterialFK = new SelectList(db.tblMateriales, "IdMaterial", "NombreMaterial", tblArchivoProyecto_Materiales.IdMaterialFK);
            return View(tblArchivoProyecto_Materiales);
        }

        // POST: Rompimientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArchivoProyecto_Materiales,Cantidad,IdArchivoProyectoFK,IdMaterialFK")] tblArchivoProyecto_Materiales tblArchivoProyecto_Materiales)
        {
            int idArchivoProyecto = tblArchivoProyecto_Materiales.IdArchivoProyectoFK;
            int idMaterial = tblArchivoProyecto_Materiales.IdMaterialFK;
            decimal precioMat = db.tblMateriales.Where(p => p.IdMaterial == idMaterial).First().PrecioMaterial;
            tblArchivoProyecto_Materiales.PrecioParcial = tblArchivoProyecto_Materiales.Cantidad * precioMat;
            if (ModelState.IsValid)
            {
                db.Entry(tblArchivoProyecto_Materiales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "ArchivoProyectos", new { id = idArchivoProyecto });
            }
            ViewBag.IdArchivoProyectoFK = new SelectList(db.tblArchivoProyecto, "IdArchivoProyecto", "NombreArchivo", tblArchivoProyecto_Materiales.IdArchivoProyectoFK);
            ViewBag.IdMaterialFK = new SelectList(db.tblMateriales, "IdMaterial", "NombreMaterial", tblArchivoProyecto_Materiales.IdMaterialFK);
            return View(tblArchivoProyecto_Materiales);
        }

        // GET: Rompimientos/Delete/5
        public ActionResult Delete(int? id, int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblArchivoProyecto_Materiales tblArchivoProyecto_Materiales = db.tblArchivoProyecto_Materiales.Find(id);
            if (tblArchivoProyecto_Materiales == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdArchivoProyecto = id2;
            return View(tblArchivoProyecto_Materiales);
        }

        // POST: Rompimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblArchivoProyecto_Materiales tblArchivoProyecto_Materiales = db.tblArchivoProyecto_Materiales.Find(id);
            int idArchivoProyecto = tblArchivoProyecto_Materiales.IdArchivoProyectoFK;
            db.tblArchivoProyecto_Materiales.Remove(tblArchivoProyecto_Materiales);
            db.SaveChanges();
            return RedirectToAction("Edit", "ArchivoProyectos", new { id = idArchivoProyecto });
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
