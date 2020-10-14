using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AsistenciaShalom.AccesoDatos.Data.IRepositorio;
using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using AsistenciaShalom.Presentacion.Filters;
using AsistenciaShalom.Presentacion.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AsistenciaShalom.Presentacion.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class UsuarioController : BaseController
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IContenedorTrabajo contenedorTrabajo, IMapper mapper, ILogger<UsuarioController> logger)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _mapper = mapper;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }


        public List<UsuarioRolDto> ListarUsuarios()
        {
            List<UsuarioRolDto> listaUsuarios = new List<UsuarioRolDto>();   
            try
            {
                listaUsuarios = _contenedorTrabajo.UsuarioRol.GetListaUsuariosRoles().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaUsuarios;

        }

        [HttpGet]
        public IActionResult Agregar()
        {
            try
            {
                CargaCombo();
                ViewBag.MostrarError = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }

        [HttpGet(("Usuario/Usuario/ListarPersonaUsuario"))]
        public List<PersonaDto> ListarPersonaUsuario(string nombreCompleto)
        {
            var lista = new List<PersonaDto>();
            try
            {
                if(nombreCompleto == null || nombreCompleto == "")
                {                   
                    lista = _contenedorTrabajo.Persona.GetPersonasActivas().ToList();
                }
                else
                {
                    lista = _contenedorTrabajo.Persona.GetListaPersonasUsuario(nombreCompleto).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lista;
        }

        [HttpPost]
        public IActionResult Agregar(UsuarioRolDto usuarioRolDto)
        {
            try
            {
                int nveces = 0;
                CargaCombo();
                usuarioRolDto.MostrarError = false;
                ViewBag.MostrarError = usuarioRolDto.MostrarError;

                using (var transaccion = new TransactionScope())
                {
                    if (usuarioRolDto.IdUsuarioRol == 0)
                    {
                       
                        if(usuarioRolDto.Username != null)
                        {
                            // Validamos si ya existe el username en la BD
                            nveces = _contenedorTrabajo.Usuario.GetAll(x => x.Username == usuarioRolDto.Username).Count();
                        }

                        if (ModelState.IsValid)
                        {
                            var usuarioDto = new UsuarioDto()
                            {
                                IdPersona = usuarioRolDto.IdPersona.GetValueOrDefault(),
                                Username = usuarioRolDto.Username,
                                //Contrasena = Cifrado.CifrarDatos(usuarioRolDto.Contrasena),
                                Contrasena = GetDecrypter().Encrypt(usuarioRolDto.Contrasena),
                                ContrasenaRepetida = usuarioRolDto.ContrasenaRepetida,
                                Estado = true,
                                UsuarioCreacion = HttpContext.Session.GetString("username"),
                                FechaCreacion = DateTime.Now
                            };

                            if (nveces != 0)
                            {
                                usuarioRolDto.MostrarError = true;
                                ViewBag.MostrarError = usuarioRolDto.MostrarError;
                                return View(usuarioRolDto);
                            }

                            // Guardar entidad Usuario
                            var entUsuario = _mapper.Map<Usuario>(usuarioDto);
                            _contenedorTrabajo.Usuario.Add(entUsuario);
                            _contenedorTrabajo.Save();

                            // Guardar entidad UsuarioRol

                            var idUsuarioGenerado = entUsuario.IdUsuario;
                            usuarioRolDto.IdUsuario = idUsuarioGenerado;
                            usuarioRolDto.Estado = true;
                            var entUsuarioRol = _mapper.Map<UsuarioRol>(usuarioRolDto);
                            _contenedorTrabajo.UsuarioRol.Add(entUsuarioRol);
                            _contenedorTrabajo.Save();
                            transaccion.Complete();

                            return Json(new { success = true });
                        }
                        else
                        {
                            if (nveces != 0)
                            {
                                usuarioRolDto.MostrarError = true;
                                ViewBag.MostrarError = usuarioRolDto.MostrarError;
                                return View(usuarioRolDto);
                            }
                        }                 
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(usuarioRolDto);
        }


        [HttpGet]
        public IActionResult Editar(int id) // idUsuarioRol
        {
            var usuarioRolDto = new UsuarioRolDto();
            try
            {
                CargaCombo();
                ViewBag.MostrarError = false;
                usuarioRolDto = _contenedorTrabajo.UsuarioRol.GetUsuarioRolPorId(id);

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(usuarioRolDto);
        }


        [HttpPost]
        public IActionResult Editar(UsuarioRolDto usuarioRolDto)   // Guardar actualizacion
        {
            try
            {
                CargaCombo();
                var flgAct = false;
                usuarioRolDto.MostrarError = false;
                ViewBag.MostrarError = usuarioRolDto.MostrarError;

                using (var transaccion = new TransactionScope())
                {
                    if (usuarioRolDto.IdUsuarioRol != 0)
                    {
                        if (usuarioRolDto.Username != null)
                        {
                            var oUsuario = _contenedorTrabajo.Usuario.GetAll(x => x.Username.Trim() == usuarioRolDto.Username.Trim()).FirstOrDefault();
                            var usernameDB = _contenedorTrabajo.Usuario.Get(usuarioRolDto.IdUsuario).Username;
                            if (oUsuario != null)  // Si el objeto existe en la BD
                            {
                                if (oUsuario.Username.ToUpper().Trim() != usernameDB.ToUpper().Trim())
                                { //  se ha cambiado el username
                                    flgAct = true;
                                }
                            }
                        }

                        if (ModelState.IsValid)
                        {
                            var usuarioDto = new UsuarioDto()
                            {
                                IdUsuario = usuarioRolDto.IdUsuario,
                                Username = usuarioRolDto.Username,
                                UsuarioActualizacion = HttpContext.Session.GetString("username")
                            };

                            if (flgAct)
                            {
                                usuarioRolDto.MostrarError = true;
                                ViewBag.MostrarError = usuarioRolDto.MostrarError;
                                return View(usuarioRolDto);
                            }

                            // Actualizar Usuario
                            var entUsuario = _mapper.Map<Usuario>(usuarioDto);
                            _contenedorTrabajo.Usuario.Update(entUsuario);
                            _contenedorTrabajo.Save();

                            // Actualizar Usuario-Rol
                            var entUsuarioRol = _mapper.Map<UsuarioRol>(usuarioRolDto);
                            _contenedorTrabajo.UsuarioRol.Update(entUsuarioRol);
                            _contenedorTrabajo.Save();
                            transaccion.Complete();
                            return Json(new { success = true });
                        }
                        else
                        {
                            if (flgAct)
                            {
                                usuarioRolDto.MostrarError = true;
                                ViewBag.MostrarError = usuarioRolDto.MostrarError;
                                return View(usuarioRolDto);
                            }
                        }                      
                    }                    
                   
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(usuarioRolDto);
        }


        [HttpGet]
        public bool Eliminar(int id)  // id: idUsuarioRol
        {
            bool rpta = false;
            try
            {
                using (var transaccion = new TransactionScope())
                {
                    // Elimnar entidad Usuario-rol
                    var usuarioRolDto = _contenedorTrabajo.UsuarioRol.GetUsuarioRolPorId(id);
                    _contenedorTrabajo.UsuarioRol.Remove(id);
                    _contenedorTrabajo.Save();

                    // Eliminar entidad Usuario
                    _contenedorTrabajo.Usuario.Remove(usuarioRolDto.IdUsuario);
                    _contenedorTrabajo.Save();
                    transaccion.Complete();

                    rpta = true;
                }
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rpta;
        }



        void CargaCombo()
        {
            var listaRoles = new Generico(_contenedorTrabajo, _mapper).CargarComboRolesEntidad();
            ViewBag.listaRoles = listaRoles;
        }

    }
}