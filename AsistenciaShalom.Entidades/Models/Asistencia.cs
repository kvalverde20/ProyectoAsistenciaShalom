using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistenciaShalom.Entidades.Models
{
    public partial class Asistencia
    {
        [Key]
        public int IdAsistencia { get; set; }
        public int IdReunion { get; set; }
        public int IdAsignacion { get; set; }
        public bool FlagAsistencia { get; set; }
        public string Comentario { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        [ForeignKey("IdAsignacion")]
        public virtual Asignacion IdAsignacionNavigation { get; set; }

        [ForeignKey("IdReunion")]
        public virtual Reunion IdReunionNavigation { get; set; }
    }
}
