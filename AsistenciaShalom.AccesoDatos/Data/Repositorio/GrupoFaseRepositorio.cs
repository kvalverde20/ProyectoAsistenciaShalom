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
    public class GrupoFaseRepositorio : Repositorio<Grupofase>, IGrupoFaseRepositorio
    {
        private readonly AsistenciaShalomDbContext _db;

        public GrupoFaseRepositorio(AsistenciaShalomDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Grupofase grupofase)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsignacionFase(int idGrupo)
        {
            var objDesdeDb = _db.Grupo.FirstOrDefault(s => s.IdGrupo == idGrupo);
            objDesdeDb.EstadoAsignacionFase = "A";
        }
    }
}
