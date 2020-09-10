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

        public void Update(Usuario usuario)
        {
            var objDesdeDb = _db.Usuario.FirstOrDefault(s => s.IdUsuario == usuario.IdUsuario);

            objDesdeDb.Contrasena = usuario.Contrasena;
            //-------------------------
            objDesdeDb.UsuarioActualizacion = "kmvalver";
            objDesdeDb.FechaActualizacion = DateTime.Now;
        }
    }
}
