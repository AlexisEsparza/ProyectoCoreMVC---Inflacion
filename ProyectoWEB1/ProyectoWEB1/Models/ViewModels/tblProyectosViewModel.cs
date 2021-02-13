using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoWEB1.Models.ViewModels
{
    public class tblProyectosViewModel
    {
        public int IdProyecto { get; set; }
        public int IdMaterial { get; set; }
        public string NombreProyecto { get; set; }
        public decimal Precio { get; set; }
        public string Construccion { get; set; }
        public string Material { get; set; }
        public decimal Cantidad { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Fecha { get; set; }
    }
}