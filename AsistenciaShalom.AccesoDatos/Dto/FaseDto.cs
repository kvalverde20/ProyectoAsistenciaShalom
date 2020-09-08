using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Dto
{
    public class FaseDto
    {

        [Required(ErrorMessage = "La Fase es obligatorio")]
        public int IdFase { get; set; }
        public string Nombre { get; set; }


    }
}
