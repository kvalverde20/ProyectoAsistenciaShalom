using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AsistenciaShalom.Entidades.Models
{
    public partial class Rol
    {
        public Rol()
        {
            UsuarioRol = new HashSet<UsuarioRol>();
        }

        [Key]
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
        public virtual ICollection<PaginaRol> PaginaRol { get; set; }
    }
}
