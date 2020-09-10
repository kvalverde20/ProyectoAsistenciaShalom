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
    public class UsuarioRolRepositorio : Repositorio<UsuarioRol>, IUsuarioRolRepositorio
    {
        private readonly AsistenciaShalomDbContext _db;

        public UsuarioRolRepositorio(AsistenciaShalomDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<UsuarioRolDto> GetListaUsuariosRoles()
        {
            var usuariRol = _db.UsuarioRol;
            var usuario = _db.Usuario;
            var persona = _db.Persona;
            var rol = _db.Rol;

            var lista = from ur in usuariRol.Where(x => x.Estado == true)
                        join u in usuario.Where(x => x.Estado == true) on ur.IdUsuario equals u.IdUsuario
                        join r in rol on ur.IdRol equals r.IdRol
                        join p in persona.Where(x => x.Estado == true) on u.IdPersona equals p.IdPersona
                        select new UsuarioRolDto
                        {
                            IdUsuarioRol = ur.IdUsuarioRol,
                            IdUsuario = u.IdUsuario,    
                            IdRol = r.IdRol,
                            NombreRol = r.Nombre,
                            Username = u.Username,                           
                            Contrasena = u.Contrasena,
                            Estado = u.Estado,
                            NombrePersona = p.Nombres,
                            ApellidosPersona = p.Apellidos
                        };

            return lista.AsEnumerable();
        }

        public UsuarioRolDto GetUsuarioRolPorId(int idUsuarioRol)
        {
            var usuariRol = _db.UsuarioRol;
            var usuario = _db.Usuario;
            var persona = _db.Persona;
            var rol = _db.Rol;

            var lista = from ur in usuariRol.Where(x => x.Estado == true && x.IdUsuarioRol == idUsuarioRol)
                        join u in usuario.Where(x => x.Estado == true) on ur.IdUsuario equals u.IdUsuario
                        join r in rol on ur.IdRol equals r.IdRol
                        join p in persona.Where(x => x.Estado == true) on u.IdPersona equals p.IdPersona
                        select new UsuarioRolDto
                        {
                            IdUsuarioRol = ur.IdUsuarioRol,
                            IdUsuario = ur.IdUsuario,
                            IdRol = ur.IdRol,
                            NombreRol = r.Nombre,
                            Username = u.Username,
                            Contrasena = u.Contrasena,
                            Estado = u.Estado,
                            NombrePersona = p.Nombres,
                            IdPersona = p.IdPersona,
                            ApellidosPersona = p.Apellidos,
                            NombreCompleto = p.Nombres + ", "+ p.Apellidos
                        };

            return lista.FirstOrDefault();
        }

        public void Update(UsuarioRol usuarioRol)
        {
            var objDesdeDb = _db.UsuarioRol.FirstOrDefault(s => s.IdUsuarioRol == usuarioRol.IdUsuarioRol);

            objDesdeDb.IdRol = usuarioRol.IdRol;
            //-------------------------
            //objDesdeDb.UsuarioActualizacion = "kmvalver";
            //objDesdeDb.FechaActualizacion = DateTime.Now;
        }
    }
}
