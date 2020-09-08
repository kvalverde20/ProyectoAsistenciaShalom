using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AsistenciaShalom.Entidades.Models
{
    public partial class Multitabla
    {
        [Key]
        public string IdMultitabla { get; set; }
        public string MultitablaDescripcion { get; set; }
        public string IdTipo { get; set; }
        public string TipoDescripcion { get; set; }
    }
}
