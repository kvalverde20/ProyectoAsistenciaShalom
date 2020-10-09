using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Data.IRepositorio
{
    public interface IPaginaRolRepositorio : IRepositorio<PaginaRol>
    {
        IEnumerable<PaginaRolDto> GetListaPaginaRol(int idRol, int idMenu);
    }
}
