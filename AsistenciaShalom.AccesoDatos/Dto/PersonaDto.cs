using AsistenciaShalom.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace AsistenciaShalom.AccesoDatos.Dto
{

    public class PersonaDto
    {
        public int IdPersona { get; set; }

        //[Required(ErrorMessage = "El Código es obligatorio")]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Los Apellidos son obligatorio")]
        public string Apellidos { get; set; }

        //[Required(ErrorMessage = "El País de origen es obligatorio")]
        [Display(Name = "País origen")]
        public string PaisOrigen { get; set; }

        //[Required(ErrorMessage = "El Estado civil es obligatorio")]
        [Display(Name = "Estado civil")]
        public string EstadoCivil { get; set; }

        //[Required(ErrorMessage = "La dirección es obligatorio")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }


        //[Required(ErrorMessage = "El Fecha de nacimiento es obligatorio")]
        [DataType(DataType.Date, ErrorMessage = "El formato de fecha no es correcto")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Nac.")]
        public DateTime? FecNacimiento { get; set; }


        //[Required(ErrorMessage = "El Correo es obligatorio")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Dirección de Correo electrónico incorrecta")]
        public string Correo { get; set; }

        //[Required(ErrorMessage = "El Teléfono es obligatorio")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }


        [Display(Name = "Nombre del responsable")]
        public string NombreCompletoResponsable { get; set; }

        [Display(Name = "Teléfono del responsable")]
        public string TelefonoResponsable { get; set; }

        [Display(Name = "Número de hijos")]
        public int? NumeroHijos { get; set; }

        [Display(Name = "Nombre del acompañador")]
        public string NombreCompletoAcompanador { get; set; }

        [Display(Name = "Comunidad")]
        [Required(ErrorMessage = "La Comunidad es obligatorio")]
        public int? IdComunidad { get; set; }

        [Display(Name = "Ministerio")]
        [Required(ErrorMessage = "El Ministerio es obligatorio")]
        public int? IdMinisterio { get; set; }

        public bool Estado { get; set; }
        public string EstadoAsignacionGrupo { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }


        [Display(Name = "Fecha Nac.")]
        public string FechaNacimientoTexto { get; set; }

        [Display(Name = "Ministerio")]
        public string MinisterioTexto { get; set; }

        [Display(Name = "Comunidad")]
        public string ComunidadTexto { get; set; }


        public virtual ICollection<AsignacionDto> Asignacion { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombresCompleto { get; set; }

        [Display(Name = "Edad")]
        public int Edad { get; set; }


    }
}
