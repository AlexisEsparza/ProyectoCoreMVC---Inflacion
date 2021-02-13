using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoWEB1.Models.ViewModels
{
    public class MaterialesViewModel
    {
        [Key]
        public int IdArchivoProyecto { get; set; }
        public string TipoConstruccion { get; set; }
        public int IdMaterial { get; set; }
        public string NombreMaterial { get; set; }
        public decimal Cantidad { get; set; }
    }
}