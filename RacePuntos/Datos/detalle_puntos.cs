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
    
    public partial class detalle_puntos
    {
        public Nullable<int> id_estacion { get; set; }
        public Nullable<int> id_puntos_servicio { get; set; }
        public Nullable<int> id_puntos_actuales { get; set; }
        public Nullable<int> puntos_compra { get; set; }
        public long id_detalle_puntos { get; set; }
    
        public virtual estaciones_de_servicio estaciones_de_servicio { get; set; }
        public virtual puntos puntos { get; set; }
    }
}
