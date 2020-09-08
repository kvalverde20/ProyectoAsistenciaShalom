using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Data.IRepositorio
{
    public interface IAsistenciaRepositorio : IRepositorio<Asistencia>
    {
        void Update(AsistenciaDto asistencia);
        void UpdateFlagAsistencia(int idAsistencia);
    }
}
