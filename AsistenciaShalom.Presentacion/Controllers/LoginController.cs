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

                    HttpContext.Session.SetString("usuario", ent.IdUsuario.ToString());
                    HttpContext.Session.SetString("username", ent.Username.ToString());
                    HttpContext.Session.SetString("nombreCompletoPersona", usuarioDto.NombreCompletoPersona.ToString());
                    HttpContext.Session.SetString("nombreGrupo", usuarioDto.NombreGrupo.ToString());
                    HttpContext.Session.SetString("nombreRol", usuarioDto.NombreRol.ToString());
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

            return RedirectToAction(nameof(Index));
        }

    }
}