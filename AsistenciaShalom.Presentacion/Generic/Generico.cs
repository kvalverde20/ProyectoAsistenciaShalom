﻿using AsistenciaShalom.AccesoDatos.Data.IRepositorio;
using AsistenciaShalom.AccesoDatos.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsistenciaShalom.Presentacion.Generic
{
    public  class Generico
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IMapper _mapper;

        public Generico(IContenedorTrabajo contenedorTrabajo, IMapper mapper)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _mapper = mapper;
        }

        public List<SelectListItem> CargarComboxGrupo (string idTipo)
        {
            var lista = new List<SelectListItem>();

            var listEnt = _contenedorTrabajo.Multitabla.GetAll(x => x.IdTipo == idTipo).ToList();
            var listDtos = _mapper.Map<List<MultitablaDto>>(listEnt);

            lista = (from m in listDtos
                                select new SelectListItem
                                {
                                    Text = m.MultitablaDescripcion,
                                    Value = m.IdMultitabla
                                }).ToList();

            lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            return lista;
        }

        public List<SelectListItem> CargarComboGrupoEntidad()
        {
            var lista = new List<SelectListItem>();

            var listEnt = _contenedorTrabajo.Grupo.GetAll(x => x.Estado == true).ToList();
            var listDtos = _mapper.Map<List<GrupoDto>>(listEnt);

            lista = (from m in listDtos
                     select new SelectListItem
                     {
                         Text = m.Nombre,
                         Value = m.IdGrupo.ToString()
                     }).ToList();

            lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            return lista;
        }

        public List<SelectListItem> CargarComboComunidadEntidad()
        {
            var lista = new List<SelectListItem>();

            var listEnt = _contenedorTrabajo.Comunidad.GetAll().ToList();
            var listDtos = _mapper.Map<List<ComunidadDto>>(listEnt);

            lista = (from m in listDtos
                     select new SelectListItem
                     {
                         Text = m.Nombre,
                         Value = m.IdComunidad.ToString()
                     }).ToList();

            lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            return lista;
        }

        public List<SelectListItem> CargarComboMinisterioEntidad()
        {
            var lista = new List<SelectListItem>();

            var listEnt = _contenedorTrabajo.Ministerio.GetAll().ToList();
            var listDtos = _mapper.Map<List<MinisterioDto>>(listEnt);

            lista = (from m in listDtos
                     select new SelectListItem
                     {
                         Text = m.Nombre,
                         Value = m.IdMinisterio.ToString()
                     }).ToList();

            lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            return lista;
        }

        public List<SelectListItem> CargarComboFaseEntidad()
        {
            var lista = new List<SelectListItem>();

            var listEnt = _contenedorTrabajo.Fase.GetAll().ToList();
            var listDtos = _mapper.Map<List<FaseDto>>(listEnt);

            lista = (from m in listDtos
                     select new SelectListItem
                     {
                         Text = m.Nombre,
                         Value = m.IdFase.ToString()
                     }).ToList();

            lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            return lista;
        }

    }
}
