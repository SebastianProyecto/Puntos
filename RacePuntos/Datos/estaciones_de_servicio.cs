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
    
    public partial class estaciones_de_servicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public estaciones_de_servicio()
        {
            this.reservas = new HashSet<reservas>();
            this.detalle_puntos = new HashSet<detalle_puntos>();
        }
    
        public int id_estacion { get; set; }
        public string empresa_estacion { get; set; }
        public string sede_estacion { get; set; }
        public string direccion_estacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reservas> reservas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_puntos> detalle_puntos { get; set; }
    }
}
