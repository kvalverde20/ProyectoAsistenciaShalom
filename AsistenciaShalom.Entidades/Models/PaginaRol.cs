using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AsistenciaShalom.Entidades.Models
{
    [Table("PAGINA_ROL")]
    public partial class PaginaRol
    {
        [Key]
        public int IdPaginaRol { get; set; }
        public int IdPagina { get; set; }
        public int IdRol { get; set; }
        public bool? Estado { get; set; }

        [ForeignKey("IdPagina")]
        public virtual Pagina IdPaginaNavigation { get; set; }

        [ForeignKey("IdRol")]
        public virtual Rol IdRolNavigation { get; set; }

    }
}
