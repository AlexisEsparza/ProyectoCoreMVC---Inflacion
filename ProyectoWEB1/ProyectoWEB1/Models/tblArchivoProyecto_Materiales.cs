//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoWEB1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblArchivoProyecto_Materiales
    {
        public int IdArchivoProyecto_Materiales { get; set; }
        public decimal Cantidad { get; set; }
        [Display(Name = "Subtotal")]
        public Nullable<decimal> PrecioParcial { get; set; }
        [Display(Name = "Nombre de Proyecto")]
        public int IdArchivoProyectoFK { get; set; }
        [Display(Name = "Material")]
        public int IdMaterialFK { get; set; }
    
        public virtual tblArchivoProyecto tblArchivoProyecto { get; set; }
        public virtual tblMateriales tblMateriales { get; set; }
    }
}
