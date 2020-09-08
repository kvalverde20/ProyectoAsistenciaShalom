using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsistenciaShalom.AccesoDatos.Data.IRepositorio;
using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using AsistenciaShalom.Presentacion.Generic;
using AsistenciaShalom.Utilitarios;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace AsistenciaShalom.Presentacion.Controllers
{
    public class PersonaController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonaController> _logger;

        public PersonaController(IContenedorTrabajo contenedorTrabajo, IMapper mapper, ILogger<PersonaController> logger)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Thread.Sleep(3000);
            return View();
        }

        [HttpGet]
        public IActionResult Agregar()
        {
            try
            {              
                CargaCombos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return View();
        }

        
        [HttpPost]
        public IActionResult Agregar(PersonaDto personaDto)
        {
            try
            {
                CargaCombos();
                if (ModelState.IsValid)
                {
                    if (personaDto.IdPersona == 0)
                    {
                        personaDto.PaisOrigen = personaDto.PaisOrigen == null ? "" : personaDto.PaisOrigen;
                        personaDto.NombreCompletoAcompanador = personaDto.NombreCompletoAcompanador == null ? "" : personaDto.NombreCompletoAcompanador;
                        personaDto.Estado = true;
                        personaDto.EstadoAsignacionGrupo = "N";
                        personaDto.UsuarioCreacion = "kmvalver";
                        personaDto.FechaCreacion = DateTime.Now;

                        var entidad = _mapper.Map<Persona>(personaDto);
                        _contenedorTrabajo.Persona.Add(entidad);
                        _contenedorTrabajo.Save();

                        return Json(new { success = true }); ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return  View(personaDto); 
        }


        [HttpGet]
        public IActionResult Editar(int? id)
        {
            var personaDto = new PersonaDto();
            try
            {
                CargaCombos();            

                if (id != null)
                {
                    var personaModel = _contenedorTrabajo.Persona.Get(id.GetValueOrDefault());
                    personaDto = _mapper.Map<PersonaDto>(personaModel);
                }
                else if (id == null)
                    { throw new Exception("ID de Persona nulo"); }
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
           
            return View(personaDto);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Editar(PersonaDto personaDto)   // Guardar actualizacion
        {
            try
            {
                CargaCombos();

                if (ModelState.IsValid)
                {
                    if (personaDto.IdPersona != 0)
                    {
                        var entidad = _mapper.Map<Persona>(personaDto);
                        _contenedorTrabajo.Persona.Update(entidad);
                        _contenedorTrabajo.Save();

                        return Json(new { success = true }); ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex; 
            }
           
            return View(personaDto);
        }


        #region CargarCombos
        public void CargaCombos()
        {
            var listaMinisterios = new Generico(_contenedorTrabajo, _mapper).CargarComboMinisterioEntidad();
            ViewBag.listaMinisterios = listaMinisterios;
            //------------------------------------------------------------//
            var listaComunidades = new Generico(_contenedorTrabajo, _mapper).CargarComboComunidadEntidad();
            ViewBag.listaComunidades = listaComunidades;
            //------------------------------------------------------------//
            //Tipo = Estado civil
            var estcivil = "01";
            var listaEstCivil = new Generico(_contenedorTrabajo, _mapper).CargarComboxGrupo(estcivil);
            ViewBag.listaEstCivil = listaEstCivil;
        }

        #endregion

        [HttpGet]
        public bool Eliminar(int id)
        {
            bool rpta = false;
            try
            {
                _contenedorTrabajo.Persona.LogicalDelete(id);
                _contenedorTrabajo.Save();
                rpta = true;
            }
            catch (Exception ex)
            {
                throw ex;             
            }
            return rpta;          
        }

        public List<PersonaDto> listarPersonas()
        {
            List<PersonaDto> listaPersonasDto = new List<PersonaDto>();
            try
            {
                 listaPersonasDto = _contenedorTrabajo.Persona.GetPersonasActivas().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return listaPersonasDto;
            //var personasList = _contenedorTrabajo.Persona.GetAll(x =>  x.Estado == true).ToList();         
            //var listaPersonasDto = _mapper.Map<List<PersonaDto>>(personasList);

        }

    }
}