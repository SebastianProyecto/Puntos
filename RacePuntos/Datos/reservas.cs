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
    
    public partial class reservas
    {
        public string id_reserva { get; set; }
        public Nullable<int> id_estacion { get; set; }
        public string id_usuario { get; set; }
        public Nullable<System.DateTime> fecha_reserva { get; set; }
        public Nullable<int> puntos_redimidos { get; set; }
    
        public virtual estaciones_de_servicio estaciones_de_servicio { get; set; }
        public virtual personas personas { get; set; }
    }
}
