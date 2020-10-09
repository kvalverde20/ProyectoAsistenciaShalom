using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Data.IRepositorio
{
    public interface IContenedorTrabajo : IDisposable
    {
        IPersonaRepositorio Persona { get; }
        IGrupoRepositorio Grupo { get; }
        IAsignacionRepositorio Asignacion { get; }
        IMultitablaRepositorio Multitabla { get; }
        IMinisterioRepositorio Ministerio { get; }
        IComunidadRepositorio Comunidad { get; }
        IReunionRepositorio Reunion { get; }
        IGrupoFaseRepositorio GrupoFase { get; }
        IAsistenciaRepositorio Asistencia { get; }
        IFaseRepositorio Fase { get; }
        IUsuarioRepositorio Usuario { get; }
        IUsuarioRolRepositorio UsuarioRol { get; }
        IRolRepositorio Rol { get; }
        IPaginaRolRepositorio PaginaRol { get; }
        IMenuRepositorio Menu { get; }
        void Save();
    }
}
