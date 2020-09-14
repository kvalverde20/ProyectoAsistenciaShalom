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
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        private readonly AsistenciaShalomDbContext _db;

        public UsuarioRepositorio(AsistenciaShalomDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<UsuarioDto> GetListaUsuariosTotales()
        {
            var usuario = _db.Usuario;
            var persona = _db.Persona;
            var multitabla = _db.Multitabla;

            var lista = from u in usuario
                        join p in persona on u.IdPersona equals p.IdPersona
                        select new UsuarioDto
                        {
                            IdUsuario = u.IdUsuario,
                            IdPersona = p.IdPersona,
                            Username = u.Username,
                            Contrasena = u.Contrasena,
                            Estado = u.Estado,
                            NombrePersona = p.Nombres,
                            ApellidosPersona = p.Apellidos
                        };

            return lista.AsEnumerable();
        }

        public UsuarioDto GetDatosGeneralesXIdUsuario(int idUsuario)
        {
            var usuario = _db.Usuario;
            var usuarioRol = _db.UsuarioRol;
            var rol = _db.Rol;
            var persona = _db.Persona;
            var grupo   = _db.Grupo;
            var asignacion = _db.Asignacion;

            var consulta =  from u in usuario.Where(x =>x.Estado == true && x.IdUsuario == idUsuario)
                            join p in persona.Where(x => x.Estado == true) on u.IdPersona equals p.IdPersona
                            join a in asignacion.Where(x => x.Estado == true) on p.IdPersona equals a.IdPersona
                            join g in grupo.Where(x => x.Estado == true) on a.IdGrupo equals g.IdGrupo
                            join ur in usuarioRol.Where(x => x.Estado == true) on u.IdUsuario equals ur.IdUsuario
                            join r in rol.Where(x => x.Estado == true) on ur.IdRol equals r.IdRol

                            select new UsuarioDto
                            {
                                IdUsuario = u.IdUsuario,
                                IdPersona = p.IdPersona,
                                IdAsignacion = a.IdAsignacion,
                                IdGrupo = g.IdGrupo,
                                IdUsuarioRol = ur.IdUsuarioRol,
                                IdRol = r.IdRol,
                                Username = u.Username,
                                Contrasena = u.Contrasena,
                                Estado = u.Estado,
                                NombrePersona = p.Nombres,
                                ApellidosPersona = p.Apellidos,
                                NombreCompletoPersona = p.Nombres + ", " +p.Apellidos,
                                NombreGrupo = g.Nombre,
                                NombreRol = r.Nombre
                            };

            return consulta.FirstOrDefault();
        }


        public void Update(Usuario usuario)
        {
            var objDesdeDb = _db.Usuario.FirstOrDefault(s => s.IdUsuario == usuario.IdUsuario);

            objDesdeDb.Username = usuario.Username == null ? objDesdeDb.Username : usuario.Username; 
            objDesdeDb.Contrasena = usuario.Contrasena == null ? objDesdeDb.Contrasena : usuario.Contrasena;
            //-------------------------
            objDesdeDb.UsuarioActualizacion = usuario.UsuarioActualizacion;
            objDesdeDb.FechaActualizacion = DateTime.Now;
        }


    }
}
