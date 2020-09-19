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
    public class AsistenciaRepositorio : Repositorio<Asistencia>, IAsistenciaRepositorio
    {
        private readonly AsistenciaShalomDbContext _db;

        public AsistenciaRepositorio(AsistenciaShalomDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(AsistenciaDto asistencia)
        {
            var objDesdeDb = _db.Asistencia.FirstOrDefault(s => s.IdAsistencia == asistencia.IdAsistencia);

            objDesdeDb.Comentario = asistencia.Comentario;
            objDesdeDb.FlagAsistencia = asistencia.FlagAsistencia;
            //-------------------------
            objDesdeDb.UsuarioActualizacion = asistencia.UsuarioActualizacion;
            objDesdeDb.FechaActualizacion = DateTime.Now;
        }

        public void UpdateFlagAsistencia(int idAsistencia)
        {
            var objDesdeDb = _db.Asistencia.FirstOrDefault(s => s.IdAsistencia == idAsistencia);

            objDesdeDb.FlagAsistencia = true;
            //-------------------------
            objDesdeDb.UsuarioActualizacion = "kmvalver";
            objDesdeDb.FechaActualizacion = DateTime.Now;
        }
    }
}
