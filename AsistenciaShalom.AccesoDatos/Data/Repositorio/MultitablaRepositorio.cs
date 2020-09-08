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
    public class MultitablaRepositorio : Repositorio<Multitabla>, IMultitablaRepositorio
    {
        private readonly AsistenciaShalomDbContext _db;

        public MultitablaRepositorio(AsistenciaShalomDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
