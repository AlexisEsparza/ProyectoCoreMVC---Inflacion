using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProyectoWEB1.Models;
using ProyectoWEB1.Models.ViewModels;

namespace ProyectoWEB1.Controllers
{
    public class CompararController : Controller
    {
        private Models.dbProyectoWebEntities db = new Models.dbProyectoWebEntities();
        // GET: Comparar
        [Authorize]
        public ActionResult Index()
        {
            //Llenado de dropdown para comparar PROYECTOS
            List<tblProyectosViewModel> lista = null;

            //lista = (from d in db.tblArchivoProyecto
            //         select new tblProyectosViewModel
            //         {
            //             IdProyecto = d.IdArchivoProyecto,
            //             Construccion = d.TipoConstruccion,
            //             Fecha = d.Fecha,

            //         }).ToList();
            //List<SelectListItem> items = lista.ConvertAll(d =>
            //{
            //    DateTime dateX = Convert.ToDateTime(d.Fecha);
            //    string FechaX = d.Fecha.HasValue ? dateX.ToString("dd/MM/yyyy") : "Sin Fecha";
            //    return new SelectListItem()
            //    {
            //        Text = "Tipo: " + d.Construccion.ToString() + "  | Fecha: " + FechaX.ToString(),
            //        Value = d.IdProyecto.ToString(),
            //        Selected = false
            //    };
            //});

            //DEFENSA(Llenado de dropdown para comparar MATERIALES)
            lista = (from d in db.tblMateriales
                     select new tblProyectosViewModel
                     {
                         IdMaterial = d.IdMaterial,
                         Material = d.NombreMaterial

                     }).ToList();
            List<SelectListItem> items = lista.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Material.ToString(),
                    Value = d.IdMaterial.ToString(),
                    Selected = false
                };
            });
            ViewBag.Items = items;

            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection id1, FormCollection id2)
        {
            //Recepción de ID Proyectos
            //int valor1 = int.Parse(id1["NombresProyectos"]);
            //int valor2 = int.Parse(id2["NombresProyectos2"]);

            //Recepción de ID Materiales (DEFENSA)
            int valor1 = int.Parse(id1["Material1"]);
            int valor2 = int.Parse(id2["Material2"]);

            //Extracción de tipo de construcción de la tabla
            string tipo1 = db.tblArchivoProyecto.Where(p => p.IdArchivoProyecto == valor1).First().TipoConstruccion;
            string tipo2 = db.tblArchivoProyecto.Where(p => p.IdArchivoProyecto == valor2).First().TipoConstruccion;

            //Extracción de la fecha desde la tabla
            DateTime date1 = Convert.ToDateTime(db.tblArchivoProyecto.Where(p => p.IdArchivoProyecto == valor1).First().Fecha);
            DateTime date2 = Convert.ToDateTime(db.tblArchivoProyecto.Where(p => p.IdArchivoProyecto == valor2).First().Fecha);

            //Establecer formato de fecha
            string fecha1 = (db.tblArchivoProyecto.Where(p => p.IdArchivoProyecto == valor1).First().Fecha.Date).ToString("dd/MM/yyyy");
            string fecha2 = (db.tblArchivoProyecto.Where(p => p.IdArchivoProyecto == valor2).First().Fecha.Date).ToString("dd/MM/yyyy");

            //(DEFENSA) Búsqueda de proyectos en los que se hayan utilizado los materiales seleccionados 
            List<tblArchivoProyecto_Materiales> listaMateriales1 = db.tblArchivoProyecto_Materiales.Where(x => x.IdMaterialFK == valor1).ToList();
            List<tblArchivoProyecto_Materiales> listaMateriales2 = db.tblArchivoProyecto_Materiales.Where(x => x.IdMaterialFK == valor2).ToList();
            List<tblArchivoProyecto> listaProyectos = new List<tblArchivoProyecto>();
            foreach(var l in listaMateriales1)
            {
                foreach (var o in listaMateriales2)
                {
                    if(l.IdArchivoProyectoFK == o.IdArchivoProyectoFK)
                    {
                        listaProyectos.Add(db.tblArchivoProyecto.Where(x => x.IdArchivoProyecto == l.IdArchivoProyectoFK).First());
                    }
                }
            }

            //Sacar Materiales
            List<tblArchivoProyecto_Materiales> lista1 = db.tblArchivoProyecto_Materiales.Where(p => p.IdArchivoProyectoFK == valor1).ToList();
            List<tblArchivoProyecto_Materiales> lista10 = db.tblArchivoProyecto_Materiales.Where(p => p.IdArchivoProyectoFK == valor2).ToList();
            List<MaterialesViewModel> lista2 = new List<MaterialesViewModel>();
            List<MaterialesViewModel> lista20 = new List<MaterialesViewModel>();
            List<tblArchivoProyecto> lista3 = db.tblArchivoProyecto.Where(p => p.IdArchivoProyecto == valor1).ToList();
            List<tblArchivoProyecto> lista30 = db.tblArchivoProyecto.Where(p => p.IdArchivoProyecto == valor2).ToList();
            string nombreMaterial1;
            string nombreMaterial2;

            foreach (var a in lista1)
            {
                nombreMaterial1 = db.tblMateriales.Where(p => p.IdMaterial == a.IdMaterialFK).First().NombreMaterial;
                lista2.Add(new MaterialesViewModel()
                {
                    IdMaterial = a.IdMaterialFK,
                    NombreMaterial = nombreMaterial1,
                    Cantidad = a.Cantidad
                });
            }
            lista2.Add(null);
            foreach (var b in lista10)
            {
                nombreMaterial2 = db.tblMateriales.Where(p => p.IdMaterial == b.IdMaterialFK).First().NombreMaterial;
                lista20.Add(new MaterialesViewModel()
                {
                    IdMaterial = b.IdMaterialFK,
                    NombreMaterial = nombreMaterial2,
                    Cantidad = b.Cantidad
                });
            }
            lista2 = lista2.Concat(lista20).ToList();

            ViewBag.TipoConstruccion1 = lista3[0].TipoConstruccion;
            ViewBag.TipoConstruccion2 = lista30[0].TipoConstruccion;

            //Cálculo de la diferencia del Precio
            decimal precio1 = Convert.ToDecimal(db.tblArchivoProyecto.Where(p => p.IdArchivoProyecto == valor1).First().PrecioTotal);
            precio1 = decimal.Round(precio1, 2);
            decimal precio2 = Convert.ToDecimal(db.tblArchivoProyecto.Where(p => p.IdArchivoProyecto == valor2).First().PrecioTotal);
            precio2 = decimal.Round(precio2, 2);
            decimal diferencia = Math.Abs(precio1 - precio2);

            //Cálculo de la inflación
            DateTime FechaA;
            DateTime[] fechamin = new DateTime[] { date1, date2 }; //Array de fechas

            if (fechamin.Min() == date1)
                FechaA = date1;
            else
                FechaA = date2;

            //Sacar PrecioAntiguo con la fecha antigua
            decimal precioA = Convert.ToDecimal(db.tblArchivoProyecto.Where(p => p.Fecha == FechaA).First().PrecioTotal);
            decimal inflacion = ((diferencia / precioA)) + 1;
            inflacion = decimal.Round(inflacion, 4);

            //Variables para la vista
            ViewBag.tipo1 = tipo1;
            ViewBag.tipo2 = tipo2;
            ViewBag.fecha1 = fecha1;
            ViewBag.fecha2 = fecha2;
            ViewBag.precio1 = precio1.ToString();
            ViewBag.precio2 = precio2.ToString();
            ViewBag.diferencia = diferencia.ToString();
            ViewBag.inflacion = inflacion.ToString();

            //return View("Details");
            //return View("Prueba2", lista2);
            return View("Materiales", listaProyectos);
        }

        // GET: Comparar/Details/5
        public ActionResult Details(int id)
        {
            
            return View();
        }

        // GET: Comparar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comparar/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Comparar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comparar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Comparar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comparar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
