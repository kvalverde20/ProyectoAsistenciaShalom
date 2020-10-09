using AsistenciaShalom.AccesoDatos.Data.IRepositorio;
using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsistenciaShalom.AccesoDatos.Data.Repositorio
{
    public class MenuRepositorio : Repositorio<Menu>, IMenuRepositorio
    {
        private readonly AsistenciaShalomDbContext _db;

        public MenuRepositorio(AsistenciaShalomDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<MenuDto> GetListaMenu(int idRol)
        {
            var paginaRol = _db.PaginaRol;
            var pagina = _db.Pagina;
            var menu = _db.Menu;

            var lista = (from pr in paginaRol.Where(x => x.Estado == true && x.IdRol == idRol)
                         join p in pagina.Where(x => x.Estado == true) on pr.IdPagina equals p.IdPagina
                         join m in menu.Where(x => x.Estado == true) on p.IdMenu equals m.IdMenu
                         select new MenuDto
                         {
                             IdMenu = m.IdMenu,
                             Nombre = m.Nombre,
                             Titulo = m.Titulo
                         }).Distinct();

            return lista.AsEnumerable();
        }
    }
}
