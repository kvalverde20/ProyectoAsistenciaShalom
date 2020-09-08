using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Dto
{
    public class AsistenciaDto
    {
        public int IdAsistencia { get; set; }
        public int IdReunion { get; set; }
        public int IdAsignacion { get; set; }
        public bool FlagAsistencia { get; set; }
        public string Comentario { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }


        public AsignacionDto Asignacion { get; set; }

        // Reunion
        [Display(Name = "Fecha Reunión")]
        public string FechaReunionTexto { get; set; }

        public int IdPersona { get; set; }

        [Display(Name = "Nombres")]
        public string NombresPersona { get; set; }

        [Display(Name = "Apellidos")]
        public string ApellidosPersona { get; set; }

    }
}
