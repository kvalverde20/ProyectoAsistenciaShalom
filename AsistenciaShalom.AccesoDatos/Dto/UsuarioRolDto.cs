using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Dto
{
    public class UsuarioRolDto
    {
        public int IdUsuarioRol { get; set; }
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El Rol es obligatorio")]
        [Display(Name = "Rol")]
        public int? IdRol { get; set; }
        public bool? Estado { get; set; }

        [Required(ErrorMessage = "La Persona es obligatorio")]
        [Display(Name = "Persona")]
        public int? IdPersona { get; set; }

        [Required(ErrorMessage = "El Username es obligatorio")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La Contraseña es obligatoria")]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }


        [DataType(DataType.Password)]
        [Compare("Contrasena", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
        //[Required(ErrorMessage = "La Contraseña es obligatoria")]
        [Display(Name = "Confirmar Contraseña")]
        public string ContrasenaRepetida { get; set; }


        public string NombrePersona { get; set; }
        public string ApellidosPersona { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto{ get; set; }
        public string NombreRol { get; set; }
        public string mensajeError { get; set; }
        
    }
}
