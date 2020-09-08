using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AsistenciaShalom.Presentacion.Controllers
{
    public class ErrorController : Controller
    {
        readonly ILogger _logger;
        public ErrorController(ILogger<ErrorController> logger) => _logger = logger;

        [HttpGet("/Error")]
        public IActionResult Index()
        {
            var status = Response.StatusCode;
            //var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"Status Code: {status}");
            _logger.LogError($"Ruta del ERROR: {exception.Path} ");
            _logger.LogError($"Mensaje de ERROR: {exception.Error}");
            _logger.LogError($"Traza del ERROR: {exception.Error.StackTrace}");

            return View("Error");
        }

        [HttpGet("/Error/NotFound/{statusCode}")]
        public IActionResult NotFound(int statusCode)
        {
            var exception = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            _logger.LogError($"Status Code: {statusCode}");
            _logger.LogError($"Error --> Pagina no encontrada: {exception.OriginalPath}");
            return View("PageNotFound", exception.OriginalPath);
        }
    }
}