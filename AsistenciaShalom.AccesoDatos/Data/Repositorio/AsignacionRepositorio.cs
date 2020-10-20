﻿using AsistenciaShalom.AccesoDatos.Data.IRepositorio;
using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Data.Repositorio
{
    public class AsignacionRepositorio : Repositorio<Asignacion>, IAsignacionRepositorio
    {
        private readonly AsistenciaShalomDbContext _db;

        public AsignacionRepositorio(AsistenciaShalomDbContext db) : base(db)
        {
            _db = db;
        }


        public IEnumerable<AsignacionDto> GetAsignaciones()
        {
            var asignacion  = _db.Asignacion;
            var persona     = _db.Persona;
            var grupo       = _db.Grupo;
            var multitabla  = _db.Multitabla;


            var lista = from a in asignacion.Where(x => x.Estado == true)
                        join p in persona.Where(x => x.Estado == true) on  a.IdPersona equals p.IdPersona
                        join g in grupo.Where(x => x.Estado == true) on a.IdGrupo equals g.IdGrupo
                        select new AsignacionDto
                        {
                            IdAsignacion = a.IdAsignacion,
                            NombresPersona = p.Nombres,
                            ApellidosPersona = p.Apellidos,
                            NombreGrupo = g.Nombre,
                            FechaIngreso = a.FechaIngreso
                        };

            return lista.AsEnumerable();
        }

        public AsignacionDto GetAsignacionPorPersona(int? idpersona)
        {
            var asignacion = _db.Asignacion;
            var persona = _db.Persona;
            var grupo = _db.Grupo;
            var grupofase = _db.Grupofase;
            var fase    = _db.Fase;

            var consulta = from a in asignacion.Where(x => x.Estado == true)
                          join p in persona.Where(x => x.Estado == true && x.IdPersona == idpersona.GetValueOrDefault()) on a.IdPersona equals p.IdPersona
                          join g in grupo.Where(x => x.Estado == true) on a.IdGrupo equals g.IdGrupo
                          join gf in grupofase.Where(x => x.FechaFin == null) on g.IdGrupo equals gf.IdGrupo
                          join f in fase on gf.IdFase equals f.IdFase

                      select new AsignacionDto
                      {
                          IdAsignacion = a.IdAsignacion,
                          NombresPersona = p.Nombres,
                          ApellidosPersona = p.Apellidos,
                          NombreGrupo = g.Nombre,
                          NombreFase = f.Nombre,
                          IdGrupoFase = gf.IdGrupoFase
                      };

            return consulta.FirstOrDefault();

        }

        public IEnumerable<AsignacionDto> ListarAsignacionesOvejasPorGrupo(int idgrupo)
        {
            var asignacion = _db.Asignacion;
            var persona = _db.Persona;
            var grupo = _db.Grupo;
            var multitabla = _db.Multitabla;

            var lista = from a in asignacion.Where(x => x.Estado == true && x.Cargo == "0301" && x.IdGrupo == idgrupo)
                           join p in persona.Where(x => x.Estado == true) on a.IdPersona equals p.IdPersona
                           join g in grupo.Where(x => x.Estado == true) on a.IdGrupo equals g.IdGrupo
                           join mi in multitabla on a.Cargo equals mi.IdMultitabla

                        select new AsignacionDto
                           {
                               IdAsignacion = a.IdAsignacion,
                               IdPersona = a.IdPersona,
                               NombresPersona = p.Nombres,
                               ApellidosPersona = p.Apellidos,
                               NombresCompleto = p.Nombres + " " + p.Apellidos,
                               NombreGrupo = g.Nombre,
                               FechaIngresoTexto = a.FechaIngreso == null ? "" : a.FechaIngreso.Value.ToShortDateString(),
                               FechaSalidaTexto = a.FechaSalida == null ? "" : a.FechaSalida.Value.ToShortDateString(),
                               FormaIngreso = a.FormaIngreso,
                               CargoTexto = mi.MultitablaDescripcion
                        };

            return lista.AsEnumerable();
        }

        public void UpdateEstadoAsignacionGrupo(int idpersona)
        {
            var objDesdeDb = _db.Persona.FirstOrDefault(s => s.IdPersona == idpersona);
            objDesdeDb.EstadoAsignacionGrupo = "A";
        }

        public void EliminarEstadoAsignacionGrupo(int idpersona)
        {
            var objDesdeDb = _db.Persona.FirstOrDefault(s => s.IdPersona == idpersona);
            objDesdeDb.EstadoAsignacionGrupo = "N";
        }

        public AsignacionDto GetAsignacionPorId(int idAsignacion)
        {
            var asignacion = _db.Asignacion;
            var persona = _db.Persona;

            var consulta = from a in asignacion.Where(x => x.Estado == true && x.IdAsignacion == idAsignacion)
                           join p in persona.Where(x => x.Estado == true) on a.IdPersona equals p.IdPersona

                           select new AsignacionDto
                           {
                               IdAsignacion = a.IdAsignacion,
                               NombresPersona = p.Nombres,
                               ApellidosPersona = p.Apellidos
                           };

            return consulta.FirstOrDefault();
        }
        //ECL
        public IEnumerable<AsignacionDto> GetPersonaGrupoActivos()
        {
            var asigancion = _db.Asignacion;
            var persona = _db.Persona;
            var grupo = _db.Grupo;
            var multitabla = _db.Multitabla;

            var lista = from a in asigancion.Where(x => x.Estado == true)
                        join p in persona on a.IdPersona equals p.IdPersona
                        join g in grupo on a.IdGrupo equals g.IdGrupo
                        join mi in multitabla on a.Cargo equals mi.IdMultitabla
                        select new AsignacionDto
                        {
                            IdAsignacion = a.IdAsignacion,
                            NombresCompleto = p.Nombres + " " + p.Apellidos,
                            NombreGrupo = g.Nombre,
                            FechaIngresoTexto = a.FechaIngreso == null ? "" : a.FechaIngreso.Value.ToShortDateString(),
                            FechaSalidaTexto = a.FechaSalida == null ? "" : a.FechaSalida.Value.ToShortDateString(),
                            FormaIngreso = a.FormaIngreso,
                            CargoTexto = mi.MultitablaDescripcion
                        };

            return lista.AsEnumerable();

        }

        public void LogicalDelete(int id)
        {
            var objDesdeDb = _db.Asignacion.FirstOrDefault(s => s.IdAsignacion == id);
            objDesdeDb.Estado = false;
            objDesdeDb.FechaSalida = DateTime.Now;
        }

        public AsignacionDto GetAsignacionPersonaGrupo(int idAsignacion)
        {
            var asignacion = _db.Asignacion;
            var persona = _db.Persona;

            var query = from a in asignacion.Where(x => x.IdAsignacion == idAsignacion  && x.Estado == true)
                        join p in persona.Where(x => x.Estado == true) on a.IdPersona equals p.IdPersona
                        select new AsignacionDto
                        {
                            IdAsignacion = a.IdAsignacion,
                            IdPersona = p.IdPersona,
                            NombresCompleto = p.Nombres + " " + p.Apellidos,
                            IdGrupo = a.IdGrupo,
                            FechaIngreso = a.FechaIngreso,
                            FechaSalida = a.FechaSalida,
                            FormaIngreso = a.FormaIngreso,
                            Cargo = a.Cargo
                        };

            return query.FirstOrDefault();

        }

 
        public void Update(Asignacion asignacion)
        {
            var objDesdeDb = _db.Asignacion.FirstOrDefault(s => s.IdAsignacion == asignacion.IdAsignacion);
            objDesdeDb.IdGrupo = asignacion.IdGrupo;
            objDesdeDb.FechaIngreso = asignacion.FechaIngreso;
            objDesdeDb.FechaSalida = asignacion.FechaSalida;
            objDesdeDb.FormaIngreso = asignacion.FormaIngreso;
            objDesdeDb.Cargo = asignacion.Cargo;
            objDesdeDb.UsuarioActualizacion = asignacion.UsuarioActualizacion;
            objDesdeDb.FechaActualizacion = DateTime.Now;
        }

        //ECL
    }
}
