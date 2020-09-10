using AsistenciaShalom.AccesoDatos.Dto;
using AsistenciaShalom.Entidades.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsistenciaShalom.Presentacion.Mapper
{
    public class AsistenciaShalomMapper : Profile
    {
        public AsistenciaShalomMapper()
        {
            CreateMap<Persona, PersonaDto>().ReverseMap();
            CreateMap<Grupo, GrupoDto>().ReverseMap();
            CreateMap<Asignacion, AsignacionDto>().ReverseMap();
            CreateMap<Multitabla, MultitablaDto>().ReverseMap();
            CreateMap<Comunidad, ComunidadDto>().ReverseMap();
            CreateMap<Ministerio, MinisterioDto>().ReverseMap();
            CreateMap<Reunion, ReunionDto>().ReverseMap();
            CreateMap<Grupofase, GrupoFaseDto>().ReverseMap();
            CreateMap<Asistencia, AsistenciaDto>().ReverseMap();
            CreateMap<Fase, FaseDto>().ReverseMap();

            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<UsuarioRol, UsuarioRolDto>().ReverseMap();
            CreateMap<Rol, RolDto>().ReverseMap();
        }
    }
}
