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
        void EliminarEstadoAsignacionGrupo(int idpersona);
        AsignacionDto GetAsignacionPorPersona(int? idpersona);
        AsignacionDto GetAsignacionPorId(int idAsignacion);
        IEnumerable<AsignacionDto> GetPersonaGrupoActivos();
        void LogicalDelete(int id);

        AsignacionDto GetAsignacionPersonaGrupo(int idAsignacion);

        void Update(Asignacion asignacion);

    }
}
