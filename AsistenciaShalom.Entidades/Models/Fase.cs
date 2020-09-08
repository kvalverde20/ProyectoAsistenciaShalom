using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AsistenciaShalom.Entidades.Models
{
    public partial class Fase
    {
        public Fase()
        {
            Grupofase = new HashSet<Grupofase>();
        }

        [Key]
        public int IdFase { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? Duracion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public virtual ICollection<Grupofase> Grupofase { get; set; }
    }
}
