using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AsistenciaShalom.AccesoDatos.Data.IRepositorio;
using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using AsistenciaShalom.Presentacion.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AsistenciaShalom.Presentacion.Controllers
{
    public class UsuarioController : Controller
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
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }


        [HttpPost]
        public IActionResult Agregar(UsuarioRolDto usuarioRolDto)
        {
            try
            {
                CargaCombo();
                if (ModelState.IsValid)
                {
                    using (var transaccion = new TransactionScope())
                    {
                        if (usuarioRolDto.IdUsuarioRol == 0)
                        {
                            var usuarioDto = new UsuarioDto()
                            {
                                IdPersona = usuarioRolDto.IdPersona.GetValueOrDefault(),
                                Username = usuarioRolDto.Username,
                                Contrasena = Cifrado.CifrarDatos(usuarioRolDto.Contrasena),
                                ContrasenaRepetida = usuarioRolDto.ContrasenaRepetida,
                                Estado = true,
                                UsuarioCreacion = "kmvalver",
                                FechaCreacion = DateTime.Now
                            };

                            // Validamos si ya existe el username en la BD
                            var nveces = _contenedorTrabajo.Usuario.GetAll(x => x.Username == usuarioDto.Username).Count();

                            if (nveces != 0)
                            {
                                usuarioRolDto.mensajeError = "El Username ya existe";
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

                if (ModelState.IsValid)
                {
                    if (usuarioRolDto.IdUsuarioRol != 0)
                    {
                        var usuarioDto = new UsuarioDto() {
                            Contrasena = usuarioRolDto.Contrasena
                        };

                        // Actualizar Usuario
                        //var entUsuario = _mapper.Map<Usuario>(usuarioDto);
                        //_contenedorTrabajo.Usuario.Update(entUsuario);
                        //_contenedorTrabajo.Save();

                        // Actualizar Usuario-Rol
                        var entUsuarioRol = _mapper.Map<UsuarioRol>(usuarioRolDto);
                        _contenedorTrabajo.UsuarioRol.Update(entUsuarioRol);
                        _contenedorTrabajo.Save();

                        return Json(new { success = true }); ;
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