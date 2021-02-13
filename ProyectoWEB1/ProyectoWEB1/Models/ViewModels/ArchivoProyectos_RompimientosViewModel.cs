using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoWEB1.Models.ViewModels
{
    public class ArchivoProyectos_RompimientosViewModel
    {
        public int IdArchivoProyecto { get; set; }
        public decimal? PrecioParcial { get; set; }
        public virtual tblArchivoProyecto tblArchivoProyecto2 { get; set; }
        public virtual tblArchivoProyecto_Materiales tblArchivoProyecto_Materiales2 { get; set; }
    }
}