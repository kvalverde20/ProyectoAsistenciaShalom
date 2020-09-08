using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Dto
{
    public class GrupoFaseDto
    {
        public int IdGrupoFase { get; set; }


        [Required(ErrorMessage = "La Fase es obligatorio")]
        public int? IdFase { get; set; }

        public int IdGrupo { get; set; }

        [DataType(DataType.Date, ErrorMessage = "El formato de fecha no es correcto")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public GrupoDto Grupo { get; set; }
    }
}
