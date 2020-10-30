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
            CargaCombo();
            return View();
        }


        public List<PersonaDto> ListarPersonasTotales()     
        {
            List<PersonaDto> lista = new List<PersonaDto>();
            try
            {
                 lista = _contenedorTrabajo.Persona.GetPersonasNoAsignadas().ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            return lista;
        }

        [HttpGet(("Asignacion/Asignacion/ListarPersonasToAsignar"))]
        public List<PersonaDto> ListarPersonasToAsignar()
        {
            List<PersonaDto> list = new List<PersonaDto>();
            try
            {
                list = Generico.listaPersonasSeleccionadas;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return list;
        }
        


        [HttpPost]
        public bool AbrirFormularioAsignacion(int[] idPersonas)
        {
            bool rpta = false;
            List<PersonaDto> lista = new List<PersonaDto>();
            Generico.listaPersonasSeleccionadas = null;
            try
            {
                CargaCombo();
                foreach (var id in idPersonas)
                {
                    var p = _contenedorTrabajo.Persona.GetPersonaPorId(id);
                    lista.Add(p);
                }
                Generico.listaPersonasSeleccionadas = lista;
                rpta = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rpta;
        }



        [HttpPost]
        public bool GuardarAsignacionMasiva(int[] idPersonas, AsignacionDto asignacion)
        {
            bool rpta = false;
            List<AsignacionDto> lista = new List<AsignacionDto>();
            try
            {
                using (var transaccion = new TransactionScope())
                {
                    if (ModelState.IsValid)
                    {
                        foreach (var id in idPersonas)
                        {
                            asignacion.IdPersona = id;
                            asignacion.Estado = true;
                            asignacion.FechaIngreso = asignacion.FechaIngreso == null ? DateTime.Now : asignacion.FechaIngreso;
                            asignacion.FormaIngreso = asignacion.FormaIngreso == null ? "" : asignacion.FormaIngreso;
                            asignacion.UsuarioCreacion = HttpContext.Session.GetString("username");
                            asignacion.FechaCreacion = DateTime.Now;
                            
                            var entidad = _mapper.Map<Asignacion>(asignacion);
                            _contenedorTrabajo.Asignacion.Add(entidad);
                            _contenedorTrabajo.Save();

                            //***Actualizamos el estadoAsignacion a 'A' en la Tabla Persona
                            _contenedorTrabajo.Asignacion.UpdateEstadoAsignacionGrupo(id);
                            _contenedorTrabajo.Save();
                            
                        }
                        transaccion.Complete();
                        rpta = true;
                    }
                }
                                                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rpta;
        }



        [HttpPost]
        public List<int> SeleccionarTodo(string valor)
        {
            List<int> rpta = new List<int>();
            List<PersonaDto> listaPersonas = new List<PersonaDto>();

            try
            {
                if (valor == "true")
                {
                    listaPersonas = _contenedorTrabajo.Persona.GetPersonasNoAsignadas().ToList();
                    foreach (var p in listaPersonas)
                    {
                        var elem = p.IdPersona;
                        rpta.Add(elem);
                    }
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rpta;
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
                            asignacion.UsuarioCreacion = HttpContext.Session.GetString("username");
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