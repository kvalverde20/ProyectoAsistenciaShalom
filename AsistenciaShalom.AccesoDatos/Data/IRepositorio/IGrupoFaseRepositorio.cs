using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Data.IRepositorio
{
    public interface IGrupoFaseRepositorio : IRepositorio<Grupofase>
    {

        void Update(Grupofase grupofase);
        void UpdateAsignacionFase(int idGrupo);

    }
}
