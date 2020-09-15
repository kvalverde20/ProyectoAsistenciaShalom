using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AsistenciaShalom.Entidades.Models
{
    public partial class Pagina
    {
        public Pagina()
        {
            PaginaRol = new HashSet<PaginaRol>();
        }

        [Key]
        public int IdPagina { get; set; }
        public int IdMenu { get; set; }
        public string Titulo { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public bool? Estado { get; set; }

        [ForeignKey("IdMenu")]
        public virtual Menu IdMenuNavigation { get; set; }
        public virtual ICollection<PaginaRol> PaginaRol { get; set; }
    }
}
