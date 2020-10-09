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
    public class PaginaRolRepositorio : Repositorio<PaginaRol>, IPaginaRolRepositorio
    {
        private readonly AsistenciaShalomDbContext _db;

        public PaginaRolRepositorio(AsistenciaShalomDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<PaginaRolDto> GetListaPaginaRol(int idRol, int idMenu)
        {
            var paginaRol = _db.PaginaRol;
            var pagina = _db.Pagina;
            var rol = _db.Rol;
            var menu = _db.Menu;

            var lista = from pr in paginaRol.Where(x => x.Estado == true && x.IdRol == idRol)
                        join p in pagina.Where(x => x.Estado == true) on pr.IdPagina equals p.IdPagina
                        join r in rol.Where(x => x.Estado == true) on pr.IdRol equals r.IdRol
                        join m in menu.Where(x => x.Estado == true && x.IdMenu == idMenu) on p.IdMenu equals m.IdMenu
                        select new PaginaRolDto
                        {
                            IdPaginaRol = pr.IdPaginaRol,
                            IdPagina = pr.IdPagina,    
                            IdRol = pr.IdRol,
                            Estado = pr.Estado,
                            Pagina = new PaginaDto()
                            {
                                Titulo = p.Titulo,
                                Controlador = p.Controlador,
                                Accion = p.Accion
                            },
                            Rol = new RolDto()
                            {
                                Nombre = r.Nombre
                            },
                            Menu = new MenuDto()
                            {
                                IdMenu = m.IdMenu,
                                Nombre = m.Nombre,
                                Titulo = m.Titulo
                            }

                        };

            return lista.AsEnumerable();
        }


    }
}
