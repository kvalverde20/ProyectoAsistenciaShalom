using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Dto
{
    public  class PaginaDto
    {
        public int IdPagina { get; set; }
        public int IdMenu { get; set; }
        public string Titulo { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public bool? Estado { get; set; }
    }
}
