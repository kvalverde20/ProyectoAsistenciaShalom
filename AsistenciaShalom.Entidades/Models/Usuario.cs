using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AsistenciaShalom.Entidades.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            UsuarioRol = new HashSet<UsuarioRol>();
        }

        [Key]
        public int IdUsuario { get; set; }
        public int IdPersona { get; set; }
        public string Username { get; set; }
        public string Contrasena { get; set; }
        public bool? Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }


        [ForeignKey("IdPersona")]
        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
    }
}
