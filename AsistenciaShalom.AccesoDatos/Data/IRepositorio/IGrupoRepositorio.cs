using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Data.IRepositorio
{
    public interface IGrupoRepositorio : IRepositorio<Grupo>
    {
        IEnumerable<GrupoDto> GetGruposActivos();
        GrupoDto GetGrupoPorId(int idGrupo);
        void Update(Grupo grupo);

    }
}
