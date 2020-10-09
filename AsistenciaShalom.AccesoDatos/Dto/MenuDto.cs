using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Dto
{
    public class MenuDto
    {
        public int IdMenu { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public bool? Estado { get; set; }

        public List<PaginaRolDto> PaginaRol { get; set; }
    }
}
