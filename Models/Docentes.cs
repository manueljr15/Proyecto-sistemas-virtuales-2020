//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemasVirtuales.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Docentes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Docentes()
        {
            this.Tutores_Alumnos = new HashSet<Tutores_Alumnos>();
        }
    
        public int id_Docente { get; set; }
        public string nombre { get; set; }
        public string ap_pat { get; set; }
        public string ap_mat { get; set; }
        public Nullable<int> status { get; set; }
        public string correo { get; set; }
        public string pwd { get; set; }
        public Nullable<int> rol { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tutores_Alumnos> Tutores_Alumnos { get; set; }
    }
}
