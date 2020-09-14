using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Data.IRepositorio
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        IEnumerable<UsuarioDto> GetListaUsuariosTotales();
        UsuarioDto GetDatosGeneralesXIdUsuario(int idUsuario);
        void Update(Usuario usuario);

    }
}
