using AsistenciaShalom.AccesoDatos.Data.IRepositorio;
using AsistenciaShalom.Entidades.Models;
using AsistenciaShalom.AccesoDatos.Data.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsistenciaShalom.AccesoDatos.Dto;

namespace AsistenciaShalom.AccesoDatos.Data.Repositorio
{
    public class ReunionRepositorio : Repositorio<Reunion>, IReunionRepositorio
    {
        private readonly AsistenciaShalomDbContext _db;

        public ReunionRepositorio(AsistenciaShalomDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<ReunionDto> GetReunionesPorGrupo(int? idGrupo)
        {
            var reunion   = _db.Reunion;
            var grupo     = _db.Grupo;
            var grupoFase = _db.Grupofase;
            var fase      = _db.Fase;
            var multitabla = _db.Multitabla;

            var lista = from r in reunion
                        join gf in grupoFase on r.IdGrupoFase equals gf.IdGrupoFase
                        join g  in grupo.Where(x => x.IdGrupo == idGrupo) on gf.IdGrupo equals g.IdGrupo
                        join f in fase on gf.IdFase equals f.IdFase
                        join m in multitabla on r.TipoReunion equals m.IdMultitabla

                        select new ReunionDto
                        {
                            IdReunion = r.IdReunion,
                            IdGrupoFase = r.IdGrupoFase,
                            FechaReunion = r.FechaReunion,
                            TipoReunion = r.TipoReunion,
                            TipoReunionTexto = m.MultitablaDescripcion,
                            FechaReunionTexto = r.FechaReunion.Value.ToShortDateString(),
                            TemaFormacion = r.TemaFormacion,
                            RhemaOracion = r.RhemaOracion,
                            Predicador = r.Predicador,
                            NombreFase = f.Nombre,
                            NombreGrupo = g.Nombre,
                            EstadoRegistroAsistencia = r.EstadoRegistroAsistencia
                        };

            return lista.AsEnumerable().OrderByDescending(x=> x.FechaReunion);
        }

        public ReunionDto GetReunionPorId(int idReunion)
        {
            var reunion = _db.Reunion;
            var multitabla = _db.Multitabla;

            var consulta =  from r in reunion.Where(x => x.IdReunion == idReunion)
                            join m in multitabla on r.TipoReunion equals m.IdMultitabla

                            select new ReunionDto
                            {
                                IdReunion = r.IdReunion,
                                IdGrupoFase = r.IdGrupoFase,
                                FechaReunion = r.FechaReunion,
                                TipoReunion = r.TipoReunion,
                                TipoReunionTexto = m.MultitablaDescripcion,
                                FechaReunionTexto = r.FechaReunion.Value.ToShortDateString(),
                                TemaFormacion = r.TemaFormacion,
                                RhemaOracion = r.RhemaOracion,
                                Predicador = r.Predicador,
                                EstadoRegistroAsistencia = r.EstadoRegistroAsistencia
                            };

            return consulta.FirstOrDefault();
        }

        public IEnumerable<ReunionDto> GetReunionesTotales()
        {
            var reunion = _db.Reunion;
            var grupo = _db.Grupo;
            var grupoFase = _db.Grupofase;
            var fase = _db.Fase;
            var multitabla = _db.Multitabla;

            var lista = from r in reunion
                        join gf in grupoFase on r.IdGrupoFase equals gf.IdGrupoFase
                        join g in grupo on gf.IdGrupo equals g.IdGrupo
                        join f in fase on gf.IdFase equals f.IdFase
                        join m in multitabla on r.TipoReunion equals m.IdMultitabla

                        select new ReunionDto
                        {
                            IdReunion = r.IdReunion,
                            IdGrupoFase = r.IdGrupoFase,
                            FechaReunion = r.FechaReunion,
                            TipoReunionTexto = m.MultitablaDescripcion,
                            FechaReunionTexto = r.FechaReunion.Value.ToShortDateString(),
                            TemaFormacion = r.TemaFormacion,
                            RhemaOracion = r.RhemaOracion,
                            Predicador = r.Predicador,
                            NombreFase = f.Nombre,
                            NombreGrupo = g.Nombre,
                            EstadoRegistroAsistencia = r.EstadoRegistroAsistencia
                        };

            return lista.AsEnumerable().OrderByDescending(x => x.FechaReunion); 
        }

        public IEnumerable<AsistenciaDto> ListarOvejasPorGrupo(int idgrupo)
        {
            var asignacion = _db.Asignacion;
            var persona = _db.Persona;
            var grupo = _db.Grupo;

            var lista = from a in asignacion.Where(x => x.Estado == true && x.Cargo == "0301" && x.IdGrupo == idgrupo)
                        join p in persona.Where(x => x.Estado == true) on a.IdPersona equals p.IdPersona
                        //join g in grupo.Where(x => x.Estado == true) on a.IdGrupo equals g.IdGrupo

                        select new AsistenciaDto
                        {
                            IdAsignacion = a.IdAsignacion,
                            IdPersona = a.IdPersona,
                            NombresPersona = p.Nombres,
                            ApellidosPersona = p.Apellidos
                        };

            return lista.AsEnumerable();
        }

        public IEnumerable<AsistenciaDto> ListarAsistenciasPorReunion(int idReunion)
        {
            var asistencia = _db.Asistencia;
            var asignacion = _db.Asignacion;
            var persona = _db.Persona;

            var lista = from a in asistencia.Where(x => x.IdReunion == idReunion)
                        join b in asignacion.Where(x => x.Estado == true) on a.IdAsignacion equals b.IdAsignacion
                        join p in persona.Where(x => x.Estado == true) on b.IdPersona equals p.IdPersona

                        select new AsistenciaDto
                        {
                            IdAsistencia = a.IdAsistencia,
                            IdReunion = a.IdReunion,
                            IdAsignacion = a.IdAsignacion,
                            IdPersona = p.IdPersona,
                            NombresPersona = p.Nombres,
                            ApellidosPersona = p.Apellidos,
                            FlagAsistencia = a.FlagAsistencia,
                            Comentario = a.Comentario
                        };

            return lista.AsEnumerable();
        }

        public IEnumerable<AsistenciaDto> ListarAsistenciasPorReunionHistorico(int idReunion)
        {
            var asistencia = _db.Asistencia;
            var asignacion = _db.Asignacion;
            var persona = _db.Persona;

            var lista = from a in asistencia.Where(x => x.IdReunion == idReunion)
                        join b in asignacion on a.IdAsignacion equals b.IdAsignacion
                        join p in persona.Where(x => x.Estado == true) on b.IdPersona equals p.IdPersona

                        select new AsistenciaDto
                        {
                            IdAsistencia = a.IdAsistencia,
                            IdReunion = a.IdReunion,
                            IdAsignacion = a.IdAsignacion,
                            IdPersona = p.IdPersona,
                            NombresPersona = p.Nombres,
                            ApellidosPersona = p.Apellidos,
                            FlagAsistencia = a.FlagAsistencia,
                            Comentario = a.Comentario
                        };

            return lista.AsEnumerable();
        }

        public void Update(Reunion reunion)
        {
            var objDesdeDb = _db.Reunion.FirstOrDefault(s => s.IdReunion == reunion.IdReunion);

            objDesdeDb.FechaReunion = reunion.FechaReunion;
            objDesdeDb.TipoReunion = reunion.TipoReunion;
            objDesdeDb.TemaFormacion = reunion.TemaFormacion; ;
            objDesdeDb.RhemaOracion = reunion.RhemaOracion;
            objDesdeDb.Predicador = reunion.Predicador;
            //-------------------------
            objDesdeDb.UsuarioActualizacion = reunion.UsuarioActualizacion;
            objDesdeDb.FechaActualizacion = DateTime.Now;
        }

        public void UpdateEstadoAsistencia(Reunion reunion) 
        {
            var objDesdeDb = _db.Reunion.FirstOrDefault(s => s.IdReunion == reunion.IdReunion);

            objDesdeDb.EstadoRegistroAsistencia = true;
            //-------------------------
            objDesdeDb.UsuarioActualizacion = reunion.UsuarioActualizacion;
            objDesdeDb.FechaActualizacion = DateTime.Now;
        }
    }
}
