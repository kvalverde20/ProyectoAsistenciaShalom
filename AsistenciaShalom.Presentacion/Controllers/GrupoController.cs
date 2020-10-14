using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class GrupoController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonaController> _logger;

        public GrupoController(IContenedorTrabajo contenedorTrabajo, IMapper mapper, ILogger<PersonaController> logger)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _mapper = mapper;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public List<GrupoDto> ListarGrupos()
        {
            var listaGruposDto = _contenedorTrabajo.Grupo.GetGruposActivos().ToList();

            return listaGruposDto;
        }

        void CargaCombo()
        {
            //Tipo = TipoGrupo
            var tipoGrupo = "02";
            var listaTipoGrupo = new Generico(_contenedorTrabajo, _mapper).CargarComboxGrupo(tipoGrupo);
            ViewBag.listaTipoGrupo = listaTipoGrupo;
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

        [HttpPost]
        public IActionResult Agregar(GrupoDto grupoDto)
        {
            try
            {
                int cont = 0;
                CargaCombo();
                grupoDto.MostrarError = false;
                ViewBag.MostrarError = grupoDto.MostrarError;

                if (grupoDto.IdGrupo == 0)
                {
                    if (grupoDto.Nombre != null)
                    {
                        cont = _contenedorTrabajo.Grupo.GetGrupoPorNombre(grupoDto.Nombre).Count();
                    }

                    if (ModelState.IsValid)
                    {
                        grupoDto.Estado = true;
                        grupoDto.UsuarioCreacion = HttpContext.Session.GetString("username");
                        grupoDto.FechaCreacion = DateTime.Now;
                        grupoDto.EstadoAsignacionFase = "N";

                        if (cont != 0)
                        {
                            grupoDto.MostrarError = true;
                            ViewBag.MostrarError = grupoDto.MostrarError;
                            return View(grupoDto);
                        }

                        var entidad = _mapper.Map<Grupo>(grupoDto);
                        _contenedorTrabajo.Grupo.Add(entidad);
                        _contenedorTrabajo.Save();

                        return Json(new { success = true });
                    }
                    else
                    {
                        if (cont != 0)
                        {
                            grupoDto.MostrarError = true;
                            ViewBag.MostrarError = grupoDto.MostrarError;
                            return View(grupoDto);
                        }
                    }                      
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(grupoDto);
        }

        public bool Eliminar(int id)
        {
            bool rpta = false;
            try
            {
                _contenedorTrabajo.Grupo.LogicalDelete(id);
                _contenedorTrabajo.Save();
                rpta = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rpta;
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            var grupoDto = new GrupoDto();
            try
            {
                CargaCombo();
                ViewBag.MostrarError = false;

                if (id != null)
                {
                    var grupoModel = _contenedorTrabajo.Grupo.Get(id.GetValueOrDefault());
                    grupoDto = _mapper.Map<GrupoDto>(grupoModel);
                }
                else if (id == null)
                { throw new Exception("ID de Grupo nulo"); }
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(grupoDto);
        }

        [HttpPost]
        public IActionResult Editar(GrupoDto grupoDto)   // Guardar actualizacion
        {
            try
            {
                CargaCombo();
                var oGrupo = new GrupoDto();
                var flgAct = false;
                grupoDto.MostrarError = false;
                ViewBag.MostrarError = grupoDto.MostrarError;
     
                if (grupoDto.IdGrupo != 0)
                {
                    if (grupoDto.Nombre != null)
                    {
                        oGrupo = _contenedorTrabajo.Grupo.GetGrupoPorNombre(grupoDto.Nombre).FirstOrDefault();
                        var nombreBD = _contenedorTrabajo.Grupo.GetGrupoPorId(grupoDto.IdGrupo).Nombre;
                        if(oGrupo != null)  // Si el nombre existe en la BD
                        {
                            if (oGrupo.Nombre.ToUpper().Trim() != nombreBD.ToUpper().Trim())
                            { //  se ha cambiado el nombre
                                flgAct = true;
                            }
                        }               
                    }

                    if (ModelState.IsValid)
                    {
                        if (flgAct)
                        {
                            grupoDto.MostrarError = true;
                            ViewBag.MostrarError = grupoDto.MostrarError;
                            return View(grupoDto);
                        }
                        grupoDto.UsuarioActualizacion = HttpContext.Session.GetString("username");
                        var entidad = _mapper.Map<Grupo>(grupoDto);
                        _contenedorTrabajo.Grupo.Update(entidad);
                        _contenedorTrabajo.Save();

                        return Json(new { success = true });
                    }
                    else
                    {
                        if (flgAct)
                        {
                            grupoDto.MostrarError = true;
                            ViewBag.MostrarError = grupoDto.MostrarError;
                            return View(grupoDto);
                        }
                    }            
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(grupoDto);
        }

    }
}