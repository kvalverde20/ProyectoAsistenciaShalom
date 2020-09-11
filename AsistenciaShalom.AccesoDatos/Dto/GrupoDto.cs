using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Dto
{
    public class GrupoDto
    {


        public int IdGrupo { get; set; }

        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [DataType(DataType.Date, ErrorMessage = "El formato de fecha no es correcto")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Inicio")]
        public DateTime? FechaInicio { get; set; }

        [DataType(DataType.Date, ErrorMessage = "El formato de fecha no es correcto")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Fin")]
        public DateTime? FechaFin { get; set; }

        [Display(Name = "Día Reunión")]
        public string DiaReunion { get; set; }
        public string Horario { get; set; }

        [Display(Name = "Tipo Grupo")]
        public string TipoGrupo { get; set; }
        public bool? Estado { get; set; }
        public string EstadoAsignacionFase { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        //----------------------
        [Display(Name = "Tipo Grupo")]
        public string TipoGrupoTexto { get; set; }

        [Display(Name = "Fecha Inicio")]
        public string FechaInicioTexto { get; set; }

        [Display(Name = "Fecha Fin")]
        public string FechaFinTexto { get; set; }


    }
}
