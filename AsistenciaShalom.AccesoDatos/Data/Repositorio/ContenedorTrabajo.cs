
using AsistenciaShalom.AccesoDatos.Data;
using AsistenciaShalom.AccesoDatos.Data.IRepositorio;
using AsistenciaShalom.AccesoDatos.Data.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Data.Repositorio
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly AsistenciaShalomDbContext _db;

        public ContenedorTrabajo(AsistenciaShalomDbContext db)
        {
            _db = db;
            Persona = new PersonaRepositorio(_db);
            Grupo = new GrupoRepositorio(_db);
            Asignacion = new AsignacionRepositorio(_db);
            Multitabla = new MultitablaRepositorio(_db);
            Ministerio = new MinisterioRepositorio(_db);
            Comunidad  = new ComunidadRepositorio(_db);
            Reunion = new ReunionRepositorio(_db);
            GrupoFase = new GrupoFaseRepositorio(_db);
            Asistencia = new AsistenciaRepositorio(_db);
            Fase = new FaseRepositorio(_db);
            Usuario = new UsuarioRepositorio(_db);
            UsuarioRol = new UsuarioRolRepositorio(_db);
            Rol = new RolRepositorio(_db);
        }

        public IPersonaRepositorio Persona { get; private set; }
        public IGrupoRepositorio Grupo { get; private set; }
        public IAsignacionRepositorio Asignacion { get; private set; }
        public IMultitablaRepositorio Multitabla { get; private set; }
        public IMinisterioRepositorio Ministerio { get; private set; }
        public IComunidadRepositorio Comunidad { get; private set; }
        public IReunionRepositorio Reunion { get; private set; }
        public IGrupoFaseRepositorio GrupoFase { get; private set; }
        public IAsistenciaRepositorio Asistencia { get; private set; }
        public IFaseRepositorio Fase { get; private set; }
        public IUsuarioRepositorio Usuario { get; private set; }
        public IUsuarioRolRepositorio UsuarioRol { get; private set; }
        public IRolRepositorio Rol { get; private set; }
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
