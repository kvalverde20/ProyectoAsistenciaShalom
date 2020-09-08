using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsistenciaShalom.Entidades.Models
{
    public partial class Grupofase
    {
        [Key]
        public int IdGrupoFase { get; set; }
        public int IdFase { get; set; }
        public int IdGrupo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        [ForeignKey("IdFase")]
        public virtual Fase IdFaseNavigation { get; set; }
        [ForeignKey("IdGrupo")]
        public virtual Grupo IdGrupoNavigation { get; set; }
    }
}
