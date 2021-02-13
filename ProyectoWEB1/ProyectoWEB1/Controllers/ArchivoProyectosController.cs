using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ProyectoWEB1.Models;
using ProyectoWEB1.Models.ViewModels;

namespace ProyectoWEB1.Controllers
{
    public class ArchivoProyectosController : Controller
    {
        private dbProyectoWebEntities db = new dbProyectoWebEntities();

        // GET: ArchivoProyectos
        [Authorize]
        public ActionResult Index()
        {
            return View(db.tblArchivoProyecto.ToList());
        }

        // GET: ArchivoProyectos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblArchivoProyecto tblArchivoProyecto = db.tblArchivoProyecto.Find(id);
            if (tblArchivoProyecto == null)
            {
                return HttpNotFound();
            }
            return View(tblArchivoProyecto);
        }

        // GET: ArchivoProyectos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArchivoProyectos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArchivoProyecto,NombreArchivo,TipoConstruccion,Fecha,PrecioTotal")] tblArchivoProyecto tblArchivoProyecto)
        {
            if (ModelState.IsValid)
            {
                db.tblArchivoProyecto.Add(tblArchivoProyecto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblArchivoProyecto);
        }

        // GET: ArchivoProyectos/Edit/5
        public ActionResult Edit(int? id)
        {
            DateTime date1 = Convert.ToDateTime(db.tblArchivoProyecto.Where(p => p.IdArchivoProyecto == id).First().Fecha);
            string fecha1 = ((db.tblArchivoProyecto.Where(p => p.IdArchivoProyecto == id).First().Fecha).Date).ToString("dd/MM/yyyy");
            ViewBag.fecha = fecha1;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblArchivoProyecto tblArchivoProyecto = db.tblArchivoProyecto.Find(id);
            if (tblArchivoProyecto == null)
            {
                return HttpNotFound();
            }
            ViewBag.date = tblArchivoProyecto.Fecha.ToString();
            ViewBag.IdArchivoProyecto = id;
            return View(tblArchivoProyecto);
        }

        public decimal? sumatoria(int id)
        {
            decimal? precioTotal = 0;
            List<ArchivoProyectos_RompimientosViewModel> lista = null;
            lista = (from d in db.tblArchivoProyecto_Materiales
                     select new ArchivoProyectos_RompimientosViewModel
                     {
                         IdArchivoProyecto = d.IdArchivoProyectoFK,
                         PrecioParcial = d.PrecioParcial
                     }).ToList();

            foreach (var a in lista)
            {
                
                if (id == a.IdArchivoProyecto)
                    precioTotal = precioTotal + a.PrecioParcial;
            }
            return (precioTotal);
        }

        // POST: ArchivoProyectos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArchivoProyecto,NombreArchivo,TipoConstruccion,Fecha,PrecioTotal")] tblArchivoProyecto tblArchivoProyecto)
        {
            int id = tblArchivoProyecto.IdArchivoProyecto;
            tblArchivoProyecto.PrecioTotal = sumatoria(id);
            if (ModelState.IsValid)
            {
                db.Entry(tblArchivoProyecto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblArchivoProyecto);
        }

        // GET: ArchivoProyectos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblArchivoProyecto tblArchivoProyecto = db.tblArchivoProyecto.Find(id);
            if (tblArchivoProyecto == null)
            {
                return HttpNotFound();
            }
            return View(tblArchivoProyecto);
        }

        // POST: ArchivoProyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblArchivoProyecto tblArchivoProyecto = db.tblArchivoProyecto.Find(id);
            db.tblArchivoProyecto.Remove(tblArchivoProyecto);
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
