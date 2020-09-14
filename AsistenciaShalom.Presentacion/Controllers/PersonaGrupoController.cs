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
    public class PersonaGrupoController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<AsignacionController> _logger;

        public PersonaGrupoController(IContenedorTrabajo contenedorTrabajo, IMapper mapper, ILogger<AsignacionController> logger)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //ECL
        public List<AsignacionDto> ListarPersonaGrupoTotal()
        {
            List<AsignacionDto> lista = new List<AsignacionDto>();
            try
            {
                lista = _contenedorTrabajo.Asignacion.GetPersonaGrupoActivos().ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lista;
        }

        public bool Eliminar(int id)
        {
            bool rpta = false;
            try
            {
                _contenedorTrabajo.Asignacion.LogicalDelete(id);
                _contenedorTrabajo.Save();
                rpta = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rpta;
        }

        void CargaCombo()
        {
            //Tipo = TipoGrupo
            var listaGruposDto = new Generico(_contenedorTrabajo, _mapper).CargarComboGrupoEntidad();
            ViewBag.ListaGrupos = listaGruposDto;

            // Tipo: Cargo
            var tipoCargo = "03";
            var listaCargos = new Generico(_contenedorTrabajo, _mapper).CargarComboxGrupo(tipoCargo);
            ViewBag.listaCargos = listaCargos;
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            var asignacionDto = new AsignacionDto();
            try
            {
                CargaCombo();

                if (id != null)
                {
                    var asignacionModel = _contenedorTrabajo.Asignacion.GetAsignacionPersonaGrupo(id.GetValueOrDefault());
                    asignacionDto = _mapper.Map<AsignacionDto>(asignacionModel);
                }
                else if (id == null)
                { throw new Exception("ID de Asignaciòn nulo"); }
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return View(asignacionDto);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Editar(AsignacionDto asignacionDto)   // Guardar actualizacion
        {
            try
            {
                CargaCombo();

                if (ModelState.IsValid)
                {
                    if (asignacionDto.IdAsignacion != 0)
                    {
                        var entidad = _mapper.Map<Asignacion>(asignacionDto);
                        _contenedorTrabajo.Asignacion.Update(entidad);
                        _contenedorTrabajo.Save();

                        return Json(new { success = true }); ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(asignacionDto);
        }



        //ECL



    }
}
