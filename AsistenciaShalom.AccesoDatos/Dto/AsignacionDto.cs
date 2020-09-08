using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Dto
{
    public class AsignacionDto
    {
        public int IdAsignacion { get; set; }
        public int? IdPersona { get; set; }

        [Required(ErrorMessage = "El Grupo de oración es obligatorio")]
        [Display(Name = "Grupo de oración")]
        public int? IdGrupo { get; set; }

        //[Required(ErrorMessage = "El Fecha de ingreso es obligatorio")]
        [DataType(DataType.Date, ErrorMessage = "El formato de fecha no es correcto")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Ingreso")]
        public DateTime? FechaIngreso { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Salida")]
        public DateTime? FechaSalida { get; set; }

        [Display(Name = "Forma de ingreso")]
        public string FormaIngreso { get; set; }

        [Required(ErrorMessage = "El Cargo es obligatorio")]
        public string Cargo { get; set; }

        public bool? IsSelected { get; set; }
        public bool? Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }


        public string NombresPersona { get; set; }
        public string ApellidosPersona { get; set; }     
        public string NombreGrupo { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombresCompleto { get; set; }
        public int Edad { get; set; }

        [Display(Name = "Ministerio")]
        public string MinisterioTexto { get; set; }

        [Display(Name = "Comunidad")]
        public string ComunidadTexto { get; set; }

        public string NombreFase { get; set; }

        public int? IdGrupoFase { get; set; }


        //public GrupoDto Grupo{ get; set; }
        //public PersonaDto Persona { get; set; }
    }
}
