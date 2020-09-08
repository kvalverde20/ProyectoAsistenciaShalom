using AsistenciaShalom.AccesoDatos.Data.IRepositorio;
using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using AsistenciaShalom.AccesoDatos.Data.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Data.Repositorio
{
    public class PersonaRepositorio : Repositorio<Persona>, IPersonaRepositorio
    {
        private readonly AsistenciaShalomDbContext _db;

        public PersonaRepositorio(AsistenciaShalomDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Persona persona)
        {
            var objDesdeDb = _db.Persona.FirstOrDefault(s => s.IdPersona == persona.IdPersona);

            objDesdeDb.Nombres = persona.Nombres;
            objDesdeDb.Apellidos = persona.Apellidos;
            objDesdeDb.PaisOrigen =  string.IsNullOrEmpty(persona.PaisOrigen) ? "" : persona.PaisOrigen;
            objDesdeDb.EstadoCivil = persona.EstadoCivil;
            objDesdeDb.FecNacimiento = persona.FecNacimiento;
            objDesdeDb.Correo = persona.Correo;
            objDesdeDb.Telefono = persona.Telefono;
            objDesdeDb.Direccion = persona.Direccion;
            objDesdeDb.NumeroHijos = persona.NumeroHijos;
            objDesdeDb.NombreCompletoResponsable = persona.NombreCompletoResponsable;
            objDesdeDb.TelefonoResponsable = persona.TelefonoResponsable;
            objDesdeDb.NombreCompletoAcompanador = string.IsNullOrEmpty(persona.NombreCompletoAcompanador) ? "" : persona.NombreCompletoAcompanador;
            objDesdeDb.IdComunidad = persona.IdComunidad;
            objDesdeDb.IdMinisterio = persona.IdMinisterio;
            objDesdeDb.Estado = true;
            //-------------------------
            objDesdeDb.UsuarioActualizacion = "kmvalver";
            objDesdeDb.FechaActualizacion = DateTime.Now;

        }

        public void LogicalDelete(int id)
        {
            var objDesdeDb = _db.Persona.FirstOrDefault(s => s.IdPersona == id) ;
            objDesdeDb.Estado = false;
        }

        public IEnumerable<PersonaDto> GetPersonasActivas()
        {
            var persona     = _db.Persona;
            var comunidad   = _db.Comunidad;
            var ministerio  = _db.Ministerio;
            var multitabla  = _db.Multitabla;

            var lista = from p in persona.Where(x => x.Estado == true)                       
                        join c in comunidad on p.IdComunidad equals c.IdComunidad
                        join mi in ministerio on p.IdMinisterio equals mi.IdMinisterio
                        select new PersonaDto
                        {
                            IdPersona = p.IdPersona,
                            Nombres = p.Nombres,
                            Apellidos = p.Apellidos,
                            PaisOrigen = p.PaisOrigen,
                            FechaNacimientoTexto = p.FecNacimiento.Value.ToShortDateString(),
                            NombreCompletoAcompanador = p.NombreCompletoAcompanador,
                            Correo = p.Correo,
                            Telefono = p.Telefono,
                            MinisterioTexto = mi.Nombre,
                            ComunidadTexto = c.Nombre,
                            Estado = p.Estado.Value,
                            EstadoAsignacionGrupo = p.EstadoAsignacionGrupo
                        };

            return lista.AsEnumerable();
        }

        public PersonaDto GetPersonaPorId(int? id)
        {
            var persona = _db.Persona;
            var comunidad = _db.Comunidad;
            var ministerio = _db.Ministerio;
            var multitabla = _db.Multitabla;

            var query = from p in persona.Where(x => x.IdPersona == id.GetValueOrDefault() && x.Estado == true)
                        join c in comunidad on p.IdComunidad equals c.IdComunidad
                        join mi in ministerio on p.IdMinisterio equals mi.IdMinisterio
                        select new PersonaDto
                        {
                            IdPersona = p.IdPersona,
                            Nombres = p.Nombres,
                            Apellidos = p.Apellidos,
                            PaisOrigen = p.PaisOrigen,
                            FechaNacimientoTexto = p.FecNacimiento.Value.ToShortDateString(),
                            Correo = p.Correo,
                            Telefono = p.Telefono,
                            MinisterioTexto = mi.Nombre,
                            ComunidadTexto = c.Nombre,
                            Estado = p.Estado.Value,
                            EstadoAsignacionGrupo = p.EstadoAsignacionGrupo,
                            NombresCompleto = p.Nombres + ", " + p.Apellidos,
                            FecNacimiento = p.FecNacimiento
                        };

            return query.FirstOrDefault();

        }

    }
}
