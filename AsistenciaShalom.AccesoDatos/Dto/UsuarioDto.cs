using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Dto
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public int IdPersona { get; set; }
        public string Username { get; set; }
        public string Contrasena { get; set; }
        public bool? Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }


        public string NombrePersona { get; set; }
        public string ApellidosPersona { get; set; }
        public string ContrasenaRepetida { get; set; }


    }
}
