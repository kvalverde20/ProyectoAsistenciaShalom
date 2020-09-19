using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Data.IRepositorio
{
    public interface IReunionRepositorio : IRepositorio<Reunion>
    {
        IEnumerable<ReunionDto> GetReunionesTotales();
        IEnumerable<ReunionDto> GetReunionesPorGrupo(int? idGrupo);
        IEnumerable<AsistenciaDto> ListarOvejasPorGrupo(int idgrupo);
        IEnumerable<AsistenciaDto> ListarAsistenciasPorReunion(int idReunion);
        void Update(Reunion reunion);
        void UpdateEstadoAsistencia(Reunion reunion);

    }
}
