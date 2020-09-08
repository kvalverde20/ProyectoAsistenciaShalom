using System;
using AsistenciaShalom.Entidades.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AsistenciaShalom.AccesoDatos.Data
{
    public partial class AsistenciaShalomDbContext : DbContext
    {
        public AsistenciaShalomDbContext()
        {
        }

        public AsistenciaShalomDbContext(DbContextOptions<AsistenciaShalomDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asignacion> Asignacion { get; set; }
        public virtual DbSet<Asistencia> Asistencia { get; set; }
        public virtual DbSet<Comunidad> Comunidad { get; set; }
        public virtual DbSet<Fase> Fase { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<Grupofase> Grupofase { get; set; }
        public virtual DbSet<Ministerio> Ministerio { get; set; }
        public virtual DbSet<Multitabla> Multitabla { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Reunion> Reunion { get; set; }

    }
}
