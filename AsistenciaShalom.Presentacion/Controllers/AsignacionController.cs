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
    public class AsignacionController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<AsignacionController> _logger;

        public AsignacionController (IContenedorTrabajo contenedorTrabajo, IMapper mapper, ILogger<AsignacionController> logger)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public List<PersonaDto> ListarPersonasTotales()     
        {
            List<PersonaDto> lista = new List<PersonaDto>();
            try
            {
                 lista = _contenedorTrabajo.Persona.GetPersonasActivas().ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            return lista;
        }

        [HttpGet]
        public IActionResult Asignar(int? id)
        {
            var AsignacionDto = new AsignacionDto();
            var personaDto = new PersonaDto();
            try
            {
                CargaCombo();               

                if (id != null)
                {
                    personaDto = _contenedorTrabajo.Persona.GetPersonaPorId(id.GetValueOrDefault());

                    int edad = DateTime.Today.Year - personaDto.FecNacimiento.Value.Year;
                    if (DateTime.Today.Month < personaDto.FecNacimiento.Value.Month)
                    {
                        edad--;
                    }
                    else if (DateTime.Today.Month == personaDto.FecNacimiento.Value.Month &&
                                        DateTime.Today.Day < personaDto.FecNacimiento.Value.Day)
                    {
                        edad--;
                    }

                    AsignacionDto = new AsignacionDto
                    {
                        IdPersona = personaDto.IdPersona,
                        NombresCompleto = personaDto.NombresCompleto,
                        Edad = edad,
                        MinisterioTexto = personaDto.MinisterioTexto,
                        ComunidadTexto = personaDto.ComunidadTexto
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
            return View(AsignacionDto);
        }

        public void CargaCombo()
        {
            var listaGruposDto = new Generico(_contenedorTrabajo, _mapper).CargarComboGrupoEntidad();
            ViewBag.ListaGrupos = listaGruposDto;

            // Tipo: Cargo
            var tipoCargo = "03";
            var listaCargos = new Generico(_contenedorTrabajo, _mapper).CargarComboxGrupo(tipoCargo);
            ViewBag.listaCargos = listaCargos;
        }

        [HttpPost]
        public IActionResult Asignar(AsignacionDto asignacion)    // Para Guardar la asignación
        {           
            try
            {
                CargaCombo();
                using (var transaccion = new TransactionScope())
                {
                    if (ModelState.IsValid)
                    {
                        if (asignacion.IdAsignacion == 0)
                        {
                            asignacion.FechaIngreso = asignacion.FechaIngreso == null ? DateTime.Now : asignacion.FechaIngreso;
                            asignacion.FormaIngreso = asignacion.FormaIngreso == null ? "" : asignacion.FormaIngreso;
                            asignacion.Estado = true;
                            asignacion.UsuarioCreacion = "kmvalver";
                            asignacion.FechaCreacion = DateTime.Now;

                            var entidad = _mapper.Map<Asignacion>(asignacion);
                            _contenedorTrabajo.Asignacion.Add(entidad);
                            _contenedorTrabajo.Save();

                            _contenedorTrabajo.Asignacion.UpdateEstadoAsignacionGrupo(asignacion.IdPersona.GetValueOrDefault());
                            _contenedorTrabajo.Save();
                            transaccion.Complete();

                            return Json(new { success = true }); ;
                        }
                    }
                }          
            }
            catch (Exception ex)
            {
                //Console.WriteLine(e.InnerException.Message);
                throw ex;
            }

            return View(asignacion);
        }

   

    }
}