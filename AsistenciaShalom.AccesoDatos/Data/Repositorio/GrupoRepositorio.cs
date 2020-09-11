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
    public class GrupoRepositorio : Repositorio<Grupo>, IGrupoRepositorio
    {
        private readonly AsistenciaShalomDbContext _db;

        public GrupoRepositorio(AsistenciaShalomDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<GrupoDto> GetGruposActivos()
        {
            var grupo = _db.Grupo;
            var multitabla = _db.Multitabla;

            var lista = from g in grupo.Where(x => x.Estado == true)
                        join mi in multitabla on g.TipoGrupo equals mi.IdMultitabla
                        select new GrupoDto
                        {
                            IdGrupo = g.IdGrupo,
                            Nombre = g.Nombre,
                            FechaInicio = g.FechaFin,
                            FechaFin = g.FechaFin,
                            DiaReunion = g.DiaReunion,
                            Horario = g.Horario,
                            TipoGrupoTexto = mi.MultitablaDescripcion,
                            FechaInicioTexto = g.FechaInicio.Value.ToShortDateString(),
                            FechaFinTexto = g.FechaFin.Value.ToShortDateString(),
                            EstadoAsignacionFase = g.EstadoAsignacionFase
                            
                        };

            return lista.AsEnumerable();

        }

        public GrupoDto GetGrupoPorId(int idGrupo)
        {
            var grupo = _db.Grupo;
            var multitabla = _db.Multitabla;

            var consulta = from g in grupo.Where(x => x.Estado == true && x.IdGrupo == idGrupo)
                        join mi in multitabla on g.TipoGrupo equals mi.IdMultitabla
                        select new GrupoDto
                        {
                            IdGrupo = g.IdGrupo,
                            Nombre = g.Nombre,
                            FechaInicio = g.FechaFin,
                            FechaFin = g.FechaFin,
                            DiaReunion = g.DiaReunion,
                            Horario = g.Horario,
                            TipoGrupoTexto = mi.MultitablaDescripcion,
                            FechaInicioTexto = g.FechaInicio.Value.ToShortDateString(),
                            FechaFinTexto = g.FechaFin.Value.ToShortDateString(),
                            EstadoAsignacionFase = g.EstadoAsignacionFase

                        };

            return consulta.FirstOrDefault();
        }

        public void Update(Grupo grupo)
        {
            var objDesdeDb = _db.Grupo.FirstOrDefault(s => s.IdGrupo == grupo.IdGrupo);
            objDesdeDb.Nombre = grupo.Nombre;
            objDesdeDb.Descripcion = grupo.Descripcion;
            objDesdeDb.FechaInicio = grupo.FechaInicio;
            objDesdeDb.FechaFin = grupo.FechaFin;
            objDesdeDb.DiaReunion = grupo.DiaReunion;
            objDesdeDb.TipoGrupo = grupo.TipoGrupo;
            objDesdeDb.Horario = grupo.Horario;
            objDesdeDb.Estado = true;
            objDesdeDb.UsuarioActualizacion = "kmvalver";
            objDesdeDb.FechaActualizacion = DateTime.Now;
        }

        public void LogicalDelete(int id)
        {
            var objDesdeDb = _db.Grupo.FirstOrDefault(s => s.IdGrupo == id);
            objDesdeDb.Estado = false;
        }

    }
}
