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
    public class ReunionController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonaController> _logger;

        public ReunionController(IContenedorTrabajo contenedorTrabajo, IMapper mapper, ILogger<PersonaController> logger)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult Index()
        {
            CargaComboGrupos();
            return View();
        }

        public List<ReunionDto> listarReuniones()
        {
            var idUsuario = int.Parse(HttpContext.Session.GetString("usuario")); // viene el IdUsuario
            var objUsuario = _contenedorTrabajo.Usuario.GetDatosGeneralesXIdUsuario(idUsuario);
            var idGrupo = objUsuario.IdGrupo;
            List<ReunionDto> lista = new List<ReunionDto>();
            try
            {
                if (idGrupo == 0)
                {
                    lista = _contenedorTrabajo.Reunion.GetReunionesTotales().ToList();
                }
                else
                {
                    lista = _contenedorTrabajo.Reunion.GetReunionesPorGrupo(idGrupo).ToList();
                }

                    
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lista;
        }

        public void CargaComboGrupos()
        {
            var listaGrupo = new Generico(_contenedorTrabajo, _mapper).CargarComboGrupoEntidad();
            ViewBag.listaGrupo = listaGrupo;
            //------------------------------------------------------------//
            //Tipo = Tipo Reunion
            var tipoReunion = "04";
            var listaTipoReunion = new Generico(_contenedorTrabajo, _mapper).CargarComboxGrupo(tipoReunion);
            ViewBag.listaTipoReunion = listaTipoReunion;

        }


        [HttpGet]
        public IActionResult Agregar()
        {
            CargaComboGrupos();
            return View();
        }


        [HttpPost]
        public IActionResult Agregar(ReunionDto reunionDto)
        {
            try
            {
                CargaComboGrupos();
                if (ModelState.IsValid)
                {
                    using (var transaccion = new TransactionScope())
                    {
                        if (reunionDto.IdReunion == 0)
                        {
                            //var idPersona = 267; // simulando
                            var idUsuario = int.Parse(HttpContext.Session.GetString("usuario")); // viene el IdUsuario
                            var objUsuario = _contenedorTrabajo.Usuario.GetDatosGeneralesXIdUsuario(idUsuario);
                            var idPersona = objUsuario.IdPersona;
                            var idGrupoFase = _contenedorTrabajo.Asignacion.GetAsignacionPorPersona(idPersona).IdGrupoFase.GetValueOrDefault();

                            reunionDto.IdGrupoFase = idGrupoFase;
                            //reunionDto.TipoReunion = reunionDto.TipoReunion == null ? "" : reunionDto.TipoReunion;
                            reunionDto.TemaFormacion = reunionDto.TemaFormacion == null ? "" : reunionDto.TemaFormacion;
                            reunionDto.RhemaOracion = reunionDto.RhemaOracion == null ? "" : reunionDto.RhemaOracion;
                            reunionDto.Predicador = reunionDto.Predicador == null ? "" : reunionDto.Predicador;
                            reunionDto.EstadoRegistroAsistencia = false;
                            reunionDto.UsuarioCreacion = HttpContext.Session.GetString("username");
                            reunionDto.FechaCreacion = DateTime.Now;

                            var entidad = _mapper.Map<Reunion>(reunionDto);
                            _contenedorTrabajo.Reunion.Add(entidad);
                            _contenedorTrabajo.Save();

                            // Crear las asistencias por asignaciones

                            //var newIdreunion = _contenedorTrabajo.Reunion.GetAll(null, q => q.OrderByDescending(s => s.IdReunion), null).FirstOrDefault().IdReunion;
                            var newIdreunion = entidad.IdReunion;
                            var grupofase = _contenedorTrabajo.GrupoFase.Get(idGrupoFase);
                            var idGrupo = grupofase.IdGrupo;
                            var ListaAsignaciones = _contenedorTrabajo.Asignacion.ListarAsignacionesOvejasPorGrupo(idGrupo).ToList();

                            foreach (var asig in ListaAsignaciones)
                            {
                                AsistenciaDto asistenciaDto = new AsistenciaDto()
                                {
                                    IdReunion = newIdreunion,
                                    IdAsignacion = asig.IdAsignacion,
                                    FlagAsistencia = false,
                                    Comentario = "",
                                    UsuarioCreacion = HttpContext.Session.GetString("username"),
                                    FechaCreacion = DateTime.Now
                                };
                                var entidadAsistencia = _mapper.Map<Asistencia>(asistenciaDto);
                                _contenedorTrabajo.Asistencia.Add(entidadAsistencia);
                                _contenedorTrabajo.Save();
                            }

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

            return View(reunionDto);
        }


        [HttpGet]
        public IActionResult Asistencia(int id)  
        {
            List<AsistenciaDto> listAsistenciasDto = new List<AsistenciaDto>();

            try
            {
                listAsistenciasDto = _contenedorTrabajo.Reunion.ListarAsistenciasPorReunion(id).ToList();  // id = idReunion

                var reunion = _contenedorTrabajo.Reunion.Get(id);
              
                ViewBag.IdReunion = reunion.IdReunion;
                ViewBag.FechaReunion = reunion.FechaReunion.Value.ToShortDateString();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(listAsistenciasDto);
        }

        [HttpPost]
        public IActionResult GuardarAsistencia(IFormCollection form)
        {
            try
            {
                using (var transaccion = new TransactionScope())
                {
                    var reunion = new Reunion() {
                        IdReunion = int.Parse(form["hdnIdReunion"]),
                        UsuarioActualizacion = HttpContext.Session.GetString("username")
                    };

                    //var idreunion = int.Parse(form["hdnIdReunion"]);
                    var listaAsistencia = _contenedorTrabajo.Reunion.ListarAsistenciasPorReunion(reunion.IdReunion).ToList();
                    var listaElementos = new List<AsistenciaDto>();

                    foreach (var asistenciaDto in listaAsistencia)
                    {
                        var id = asistenciaDto.IdAsistencia;
                        //var nameIdAsistencia = "hdnIdAsistencia-" + id.ToString();
                        var nameComentario = "Comentario-" + id.ToString();
                        var nameFlagAsistencia = "FlagAsistencia-" + id.ToString();

                        foreach (var key in form.Keys)
                        {
                            if (key.ToString() == nameComentario)
                            {
                                var value = form[key.ToString()];
                                asistenciaDto.Comentario = value.ToString();
                            }
                            if (key.ToString() == nameFlagAsistencia)
                            {
                                var value = form[key.ToString()];
                                asistenciaDto.FlagAsistencia = bool.Parse(value);
                            }
                        }
                        asistenciaDto.UsuarioActualizacion = HttpContext.Session.GetString("username");
                        _contenedorTrabajo.Asistencia.Update(asistenciaDto);
                        _contenedorTrabajo.Save();
                    }

                    _contenedorTrabajo.Reunion.UpdateEstadoAsistencia(reunion);
                    _contenedorTrabajo.Save();
                    transaccion.Complete();

                    //string[] ids = form["IdAsistencia"].ToString().Split(new char[] { ',' });   //Toma los idAsistencia seleccionados
                    //foreach (var id in ids)
                    //{
                    //    var idAsis= int.Parse(id);
                    //    //_contenedorTrabajo.Asistencia.UpdateFlagAsistencia(idAsis);
                    //    //_contenedorTrabajo.Save();
                    //}
                    //_contenedorTrabajo.Reunion.UpdateEstadoAsistencia(idreunion);
                    //_contenedorTrabajo.Save();

                    //transaccion.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Ver(int id)  // Detalle de la Reunion con asistencias
        {
            ReunionDto reunionDto = new ReunionDto();

            try
            {
                //var reunion= _contenedorTrabajo.Reunion.Get(id);  // id = idReunion
                //reunionDto = _mapper.Map<ReunionDto>(reunion);
                //reunionDto.FechaReunionTexto = reunion.FechaReunion.Value.ToShortDateString();
                reunionDto = _contenedorTrabajo.Reunion.GetReunionPorId(id);   // id = idReunion

                var listAsistenciasDto = _contenedorTrabajo.Reunion.ListarAsistenciasPorReunionHistorico(id).ToList(); 
                reunionDto.ListaAsistencias = listAsistenciasDto;

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return View(reunionDto);
        }

        [HttpGet]
        public IActionResult EditarReunionAsistencia(int id)   // id : IdReunion
        {
            ReunionDto reunionDto = new ReunionDto();
            CargaComboGrupos();
            try
            {
                var reunion = _contenedorTrabajo.Reunion.Get(id);
                reunionDto = _mapper.Map<ReunionDto>(reunion);
                var lista = _contenedorTrabajo.Reunion.ListarAsistenciasPorReunionHistorico(id).ToList();
                reunionDto.ListaAsistencias = lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(reunionDto);
        }


        [HttpPost]
        public IActionResult EditarReunionAsistencia(IFormCollection form)
        {
            try
            {
                CargaComboGrupos();
                //--------- Reunion --------------
                var idreunion = int.Parse(form["hdnIdReunion"]);
                var reunionActualizar = new Reunion
                {
                    IdReunion = idreunion,
                    FechaReunion = DateTime.Parse(form["txtFechaReunion"]),
                    //TipoReunion = form["txtTipoReunion"].ToString(),
                    TipoReunion = form["TipoReunion"].ToString(),
                    TemaFormacion = form["txtTemaFormacion"].ToString(),
                    Predicador = form["txtPredicador"].ToString(),
                    RhemaOracion = form["txtRhemaOracion"].ToString(),
                    UsuarioActualizacion = HttpContext.Session.GetString("username")
                };
                _contenedorTrabajo.Reunion.Update(reunionActualizar);
                _contenedorTrabajo.Save();

                //--------- Asistencias --------------
                using (var transaccion = new TransactionScope())
                {
                    var listaAsistencia = _contenedorTrabajo.Reunion.ListarAsistenciasPorReunionHistorico(idreunion).ToList();

                    foreach (var asistenciaDto in listaAsistencia)
                    {
                        var id = asistenciaDto.IdAsistencia;
                        var nameComentario = "Comentario-" + id.ToString();
                        var nameFlagAsistencia = "FlagAsistencia-" + id.ToString();
                        asistenciaDto.FlagAsistencia = false; // Reseteamos las asistencias guardadas anteriormente
                        asistenciaDto.Comentario = "";
                        asistenciaDto.UsuarioActualizacion = HttpContext.Session.GetString("username");

                        foreach (var key in form.Keys)
                        {
                            if (key.ToString() == nameComentario)
                            {
                                var value = form[key.ToString()];
                                asistenciaDto.Comentario = value.ToString();
                            }
                            
                            if (key.ToString() == nameFlagAsistencia)
                            {
                                var value = form[key.ToString()];
                                asistenciaDto.FlagAsistencia = bool.Parse(value);
                            }
                        }
                        _contenedorTrabajo.Asistencia.Update(asistenciaDto);
                        _contenedorTrabajo.Save();
                    }
                    transaccion.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction(nameof(Index));
        }

    }
}