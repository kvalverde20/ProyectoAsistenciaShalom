using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AsistenciaShalom.Entidades.Models
{
    [Table("USUARIO_ROL")]
    public partial class UsuarioRol
    {

        [Key]
        public int IdUsuarioRol { get; set; }
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public bool? Estado { get; set; }


        [ForeignKey("IdRol")]
        public virtual Rol IdRolNavigation { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario IdUsuarioNavigation { get; set; }
        //public virtual ICollection<PaginaRol> PaginaRol { get; set; }
    }
}
