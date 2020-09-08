using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AsistenciaShalom.Entidades.Models
{
    public partial class Comunidad
    {
        public Comunidad()
        {
            Persona = new HashSet<Persona>();
        }

        [Key]
        public int IdComunidad { get; set; }
        public string Nombre { get; set; }
        public string Sigla { get; set; }
        public string Descripcion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public virtual ICollection<Persona> Persona { get; set; }
    }
}
