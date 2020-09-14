using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AsistenciaShalom.AccesoDatos.Data.IRepositorio;
using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using AsistenciaShalom.Presentacion.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace AsistenciaShalom.Presentacion.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class HomeController : BaseController
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IContenedorTrabajo contenedorTrabajo, IMapper mapper, ILogger<HomeController> logger)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _mapper = mapper;
            _logger = logger;
        }


        public IActionResult Index()
        {
            int idUsuario = int.Parse(HttpContext.Session.GetString("usuario"));
            var objUsuario = _contenedorTrabajo.Usuario.GetDatosGeneralesXIdUsuario(idUsuario);
            ViewBag.Nombres = objUsuario.NombrePersona;
            ViewBag.NombreCompleto = HttpContext.Session.GetString("nombreCompletoPersona");
            ViewBag.NombreGrupo = HttpContext.Session.GetString("nombreGrupo");
            ViewBag.NombreRol = HttpContext.Session.GetString("nombreRol");

            return View();
        }


        public IActionResult Perfil()
        {
            int idUsuario = int.Parse(HttpContext.Session.GetString("usuario"));
            var objUsuario = _contenedorTrabajo.Usuario.GetDatosGeneralesXIdUsuario(idUsuario);

            return View(objUsuario);
        }

        [HttpPost(("Home/Home/ActualizarPassword"))]
        public bool ActualizarPassword(UsuarioDto usuarioDto)
        {
            var rpta = false;
            try
            {
                int idUsuario = int.Parse(HttpContext.Session.GetString("usuario"));
                var passCifrada = GetDecrypter().Encrypt(usuarioDto.Contrasena);

                usuarioDto.IdUsuario = idUsuario;
                usuarioDto.Contrasena = passCifrada;
                var entidad = _mapper.Map<Usuario>(usuarioDto);
                _contenedorTrabajo.Usuario.Update(entidad);
                _contenedorTrabajo.Save();
                rpta = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rpta;
        }


    }
}
