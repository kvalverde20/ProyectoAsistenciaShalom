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
                CargaCombo();
                if (ModelState.IsValid)
                {
                    if (grupoDto.IdGrupo == 0)
                    {
                        //personaDto.PaisOrigen = personaDto.PaisOrigen == null ? "" : personaDto.PaisOrigen;
                        //personaDto.NombreCompletoAcompanador = personaDto.NombreCompletoAcompanador == null ? "" : personaDto.NombreCompletoAcompanador;
                        grupoDto.Estado = true;
                        grupoDto.UsuarioCreacion = "kmvalver";
                        grupoDto.FechaCreacion = DateTime.Now;
                        grupoDto.EstadoAsignacionFase = "N";

                        var entidad = _mapper.Map<Grupo>(grupoDto);
                        _contenedorTrabajo.Grupo.Add(entidad);
                        _contenedorTrabajo.Save();

                        return Json(new { success = true }); ;
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
        //[ValidateAntiForgeryToken]
        public IActionResult Editar(GrupoDto grupoDto)   // Guardar actualizacion
        {
            try
            {
                CargaCombo();

                if (ModelState.IsValid)
                {
                    if (grupoDto.IdGrupo != 0)
                    {
                        var entidad = _mapper.Map<Grupo>(grupoDto);
                        _contenedorTrabajo.Grupo.Update(entidad);
                        _contenedorTrabajo.Save();

                        return Json(new { success = true }); ;
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