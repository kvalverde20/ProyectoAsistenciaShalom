using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Dto
{
    public class PaginaRolDto
    {
        public int IdPaginaRol { get; set; }
        public int IdPagina { get; set; }
        public int IdRol { get; set; }
        public bool? Estado { get; set; }


        public PaginaDto Pagina { get; set; }
        public RolDto Rol { get; set; }
        public MenuDto Menu { get; set; }

        public int IdMenu { get; set; }
        public string NombreMenu { get; set; }
        public string TituloMenu { get; set; }
    }
}
