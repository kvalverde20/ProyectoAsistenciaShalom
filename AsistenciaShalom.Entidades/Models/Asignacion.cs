using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistenciaShalom.Entidades.Models
{
    public partial class Asignacion
    {
        public Asignacion()
        {
            Asistencia = new HashSet<Asistencia>();
        }

        [Key]
        public int IdAsignacion { get; set; }
        public int IdPersona { get; set; }
        public int IdGrupo { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string FormaIngreso { get; set; }
        public string Cargo { get; set; }

        public bool? IsSelected { get; set; }
        public bool? Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        [ForeignKey("IdGrupo")]
        public virtual Grupo IdGrupoNavigation { get; set; }

        [ForeignKey("IdPersona")]
        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual ICollection<Asistencia> Asistencia { get; set; }
    }
}
