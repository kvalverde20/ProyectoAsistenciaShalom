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
    public class AsignacionGrupoFaseController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<AsignacionGrupoFaseController> _logger;

        public AsignacionGrupoFaseController(IContenedorTrabajo contenedorTrabajo, IMapper mapper, ILogger<AsignacionGrupoFaseController> logger)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _mapper = mapper;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public List<GrupoDto> ListarGruposActivos()
        {
            List<GrupoDto> lista = new List<GrupoDto>();
            try
            {
                var idrol = int.Parse(HttpContext.Session.GetString("idRol"));
                var idgrupo = int.Parse(HttpContext.Session.GetString("idGrupo"));

                if (idrol == 1)  // id=1 (Administrador)
                {
                    lista = _contenedorTrabajo.Grupo.GetGruposActivos().ToList();
                }
                else // Roles Pastor y núcleo
                {
                    var obj = _contenedorTrabajo.Grupo.GetGrupoPorId(idgrupo);
                    lista.Add(obj);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lista;
        }

        [HttpGet]
        public IActionResult Asignar(int? id)  // id : idGrupo
        {
            var grupofaseDto = new GrupoFaseDto();
            try
            {
                CargaCombo();

                if (id != null)
                {
                    var grupoDto = new GrupoDto();
                    grupoDto = _contenedorTrabajo.Grupo.GetGrupoPorId(id.GetValueOrDefault());
                    grupofaseDto.IdGrupo = id.GetValueOrDefault();
                    grupofaseDto.Grupo = grupoDto;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return View(grupofaseDto);
        }

        public void CargaCombo()
        {
            var listaFases = new Generico(_contenedorTrabajo, _mapper).CargarComboFaseEntidad();
            ViewBag.listaFases = listaFases;
        }

        [HttpPost]
        public IActionResult Asignar(GrupoFaseDto grupoFaseDto)    // Para Guardar la asignación Grupo-Fase
        {
            try
            {
                CargaCombo();
                using (var transaccion = new TransactionScope())
                {
                    if (ModelState.IsValid)
                    {
                        if (grupoFaseDto.IdGrupoFase == 0)
                        {
                            grupoFaseDto.FechaInicio = grupoFaseDto.FechaInicio == null ? DateTime.Now : grupoFaseDto.FechaInicio;
                            grupoFaseDto.UsuarioCreacion = "kmvalver";
                            grupoFaseDto.FechaCreacion = DateTime.Now;

                            var entidad = _mapper.Map<Grupofase>(grupoFaseDto);
                            _contenedorTrabajo.GrupoFase.Add(entidad);
                            _contenedorTrabajo.Save();

                            _contenedorTrabajo.GrupoFase.UpdateAsignacionFase(grupoFaseDto.IdGrupo);
                            _contenedorTrabajo.Save();
                            transaccion.Complete();

                            return Json(new { success = true }); ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(grupoFaseDto);
        }


    }
}