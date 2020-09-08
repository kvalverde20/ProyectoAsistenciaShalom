using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsistenciaShalom.AccesoDatos.Data.IRepositorio;
using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Presentacion.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AsistenciaShalom.Presentacion.Controllers
{
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
            return View();
        }

    }
}