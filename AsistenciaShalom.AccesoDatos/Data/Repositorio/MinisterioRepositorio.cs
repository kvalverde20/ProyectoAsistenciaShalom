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
    public class MinisterioRepositorio : Repositorio<Ministerio>, IMinisterioRepositorio
    {
        private readonly AsistenciaShalomDbContext _db;

        public MinisterioRepositorio(AsistenciaShalomDbContext db) : base(db)
        {
            _db = db;
        }


    }
}
