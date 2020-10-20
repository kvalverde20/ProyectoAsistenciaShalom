using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsistenciaShalom.AccesoDatos.Data;
using AsistenciaShalom.AccesoDatos.Data.IRepositorio;
using AsistenciaShalom.AccesoDatos.Data.Repositorio;
using AsistenciaShalom.Presentacion.Filters;
using AsistenciaShalom.Presentacion.Mapper;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FirstAppNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AsistenciaShalomDbContext>(options => options.UseSqlServer(
                                        Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IContenedorTrabajo, ContenedorTrabajo>();
            services.AddAutoMapper(typeof(AsistenciaShalomMapper));

            //services.AddControllersWithViews();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddHttpContextAccessor();
            services.AddRazorPages();
            services.AddSession( options => { options.IdleTimeout = TimeSpan.FromMinutes(10); } );
            services.AddScoped<Seguridad>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("E:/OTROS/Shalom/log-{Date}.txt");

            if (env.IsDevelopment())
            {
                //app.UseExceptionHandler("/Error"); // Added to handle error
                app.UseStatusCodePagesWithReExecute("/Error/NotFound/{0}");  // Added to handle Page Not Found
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error"); // Added to handle error
                app.UseStatusCodePagesWithReExecute("/Error/NotFound/{0}");  // Added to handle Page Not Found
                
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
