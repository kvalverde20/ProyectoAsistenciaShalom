using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsistenciaShalom.AccesoDatos.Data.IRepositorio;
using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Presentacion.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AsistenciaShalom.Presentacion.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IContenedorTrabajo contenedorTrabajo, IMapper mapper, ILogger<LoginController> logger)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _mapper = mapper;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public bool ValidarUsuario(string username, string password)
        {
            bool rpta = false;
            try
            {
                //string clavecrifrada = Cifrado.CifrarDatos(password);
                string clavecifrada = GetDecrypter().Encrypt(password);
                var ent = _contenedorTrabajo.Usuario.GetFirstOrDefault(x =>x.Username == username
                                        && x.Contrasena== clavecifrada);
            
                if(ent != null)
                {
                    rpta = true;
                    var usuarioDto = _contenedorTrabajo.Usuario.GetDatosGeneralesXIdUsuario(ent.IdUsuario);
                    var listaMenu = _contenedorTrabajo.Menu.GetListaMenu(usuarioDto.IdRol).ToList();

                    foreach (var item in listaMenu)
                    {
                        item.PaginaRol = _contenedorTrabajo.PaginaRol.GetListaPaginaRol(usuarioDto.IdRol, item.IdMenu).ToList();
                    }

                    Generico.listaMenu = listaMenu;

                    HttpContext.Session.SetString("usuario", ent.IdUsuario.ToString());
                    HttpContext.Session.SetString("username", ent.Username.ToString());
                    HttpContext.Session.SetString("nombreCompletoPersona", usuarioDto.NombreCompletoPersona.ToString());
                    HttpContext.Session.SetString("nombreGrupo", usuarioDto.NombreGrupo.ToString());
                    HttpContext.Session.SetString("nombreRol", usuarioDto.NombreRol.ToString());
                    HttpContext.Session.SetString("idRol", usuarioDto.IdRol.ToString());
                    HttpContext.Session.SetString("idGrupo", usuarioDto.IdGrupo.ToString());
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rpta;
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Remove("usuario");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("nombreCompletoPersona");
            HttpContext.Session.Remove("nombreGrupo");
            HttpContext.Session.Remove("nombreRol");
            HttpContext.Session.Remove("idRol");
            HttpContext.Session.Remove("idGrupo");

            return RedirectToAction(nameof(Index));
        }

    }
}