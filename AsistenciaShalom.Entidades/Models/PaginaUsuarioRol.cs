using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AsistenciaShalom.Entidades.Models
{
    public partial class PaginaUsuarioRol
    {
        [Key]
        public int IdPaginaUsuarioRol { get; set; }
        public int IdPagina { get; set; }
        public int IdUsuarioRol { get; set; }
        public bool? Estado { get; set; }

        [ForeignKey("IdPagina")]
        public virtual Pagina IdPaginaNavigation { get; set; }

        [ForeignKey("IdUsuarioRol")]
        public virtual UsuarioRol IdUsuarioRolNavigation { get; set; }
    }
}
