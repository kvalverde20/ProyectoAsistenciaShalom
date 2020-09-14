using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsistenciaShalom.AccesoDatos.Data.IRepositorio;
using AsistenciaShalom.Presentacion.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AsistenciaShalom.Presentacion.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class GrupoFaseController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<GrupoFaseController> _logger;

        public GrupoFaseController(IContenedorTrabajo contenedorTrabajo, IMapper mapper, ILogger<GrupoFaseController> logger)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}