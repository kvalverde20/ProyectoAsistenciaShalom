using AsistenciaShalom.AccesoDatos.Data.IRepositorio;
using AsistenciaShalom.AccesoDatos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsistenciaShalom.Presentacion.Filters
{
    public class Seguridad : IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var idusuario = int.Parse(context.HttpContext.Session.GetString("usuario"));
            //Console.WriteLine("");
       
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var usu = context.HttpContext.Session.GetString("usuario");
            if (usu == null)
            {
                context.Result = new RedirectResult("Login");
            }
        }
    }
}
