//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
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
