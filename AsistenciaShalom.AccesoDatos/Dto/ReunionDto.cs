using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Dto
{
    public class ReunionDto
    {
        public int IdReunion { get; set; }
        public int IdGrupoFase { get; set; }


        [Required(ErrorMessage = "La Fecha de reunión es obligatoria")]
        [DataType(DataType.Date, ErrorMessage = "El formato de fecha no es correcto")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Reunión")]
        public DateTime? FechaReunion { get; set; }


        [Display(Name = "Tipo Reunión")]
        public string TipoReunion { get; set; }

        [Display(Name = "Tema Formación")]
        public string TemaFormacion { get; set; }

        [Display(Name = "Rhema Oración")]
        public string RhemaOracion { get; set; }
        public string Predicador { get; set; }

        public bool EstadoRegistroAsistencia { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }


        public List<AsistenciaDto> ListaAsistencias { get; set; }

        [Display(Name = "Fase")]
        public string NombreFase { get; set; }

        [Display(Name = "Grupo")]
        public string NombreGrupo { get; set; }

        [Display(Name = "Fecha Reunión")]
        public string FechaReunionTexto { get; set; }

    }
}
