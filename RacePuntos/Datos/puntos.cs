//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RacePuntos.Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class puntos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public puntos()
        {
            this.detalle_puntos = new HashSet<detalle_puntos>();
        }
    
        public int id_puntos_actuales { get; set; }
        public string id_usuario_puntos { get; set; }
        public string id_usuario_plataforma { get; set; }
        public Nullable<int> puntos_acumulados { get; set; }
    
        public virtual personas personas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_puntos> detalle_puntos { get; set; }
    }
}
