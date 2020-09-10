using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AsistenciaShalom.Entidades.Models
{
    public partial class Menu
    {
        public Menu()
        {
            Pagina = new HashSet<Pagina>();
        }

        [Key]
        public int IdMenu { get; set; }
        public string Titulo { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Pagina> Pagina { get; set; }
    }
}
