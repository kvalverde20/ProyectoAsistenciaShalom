using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Data.IRepositorio
{
    public interface IUsuarioRolRepositorio : IRepositorio<UsuarioRol>
    {
        IEnumerable<UsuarioRolDto> GetListaUsuariosRoles();

        UsuarioRolDto GetUsuarioRolPorId(int idUsuarioRol);
        void Update(UsuarioRol usuarioRol);

    }
}
