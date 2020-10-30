using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Data.IRepositorio
{
    public interface IPersonaRepositorio : IRepositorio<Persona>
    {
        void Update(Persona persona);
        void LogicalDelete(int id);
        IEnumerable<PersonaDto> GetPersonasActivas();
        IEnumerable<PersonaDto> GetPersonasNoAsignadas();
        PersonaDto GetPersonaPorId(int? id);
        IEnumerable<PersonaDto> GetListaPersonasUsuario(string nombrecompleto);

    }
}
