using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Data.IRepositorio
{
    public interface IAsignacionRepositorio : IRepositorio<Asignacion>
    {
        IEnumerable<AsignacionDto> GetAsignaciones();
        IEnumerable<AsignacionDto> ListarAsignacionesOvejasPorGrupo(int idgrupo);
        void UpdateEstadoAsignacionGrupo(int idpersona);
        AsignacionDto GetAsignacionPorPersona(int? idpersona);
        AsignacionDto GetAsignacionPorId(int idAsignacion);
    }
}
