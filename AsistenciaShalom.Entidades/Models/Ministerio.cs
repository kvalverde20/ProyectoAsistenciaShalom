using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AsistenciaShalom.Entidades.Models
{
    public partial class Ministerio
    {
        
        public Ministerio()
        {
            Persona = new HashSet<Persona>();
        }

        [Key]
        public int IdMinisterio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public virtual ICollection<Persona> Persona { get; set; }
    }
}
