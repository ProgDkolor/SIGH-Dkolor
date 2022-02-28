using API.PONCHADOR.Servicios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using API.PONCHADOR.Modelo.DTOs;
using API.PONCHADOR.Modelo;
using API.PONCHADOR.Servicios.Empleados;

namespace API.PONCHADOR
{
    public class Startup
    {
        private readonly string cors = "apiCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.CreateMap<AccionesAplicablesEnLaRutaDTO, PermisoPorRol>().ReverseMap();
                config.CreateMap<UsuarioDTO, Usuarios>().ReverseMap();
            });
            services.AddScoped<ISql, Sql>();
            services.AddScoped<ICRUDEmpleado, SCRUDEmpleado>();
            services.AddScoped<IControlDeRutas, SControlDeRutas>();
            services.AddScoped<IAutenticacion, SAutenticacion>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API ACCESO DATOS PONCHADOR", Version = "v1" });
            });
            services.AddCors(opciones => {
                opciones.AddPolicy(name: cors, builder => {
                    //builder.SetIsOriginAllowed(origen => new Uri(origen).Host == "127.0.0.1")
                    builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => 
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["JWTkey"])),
                ClockSkew = TimeSpan.Zero
            });
        }

        private void JwtBeaterDefaults(AuthenticationOptions obj)
        {
            throw new NotImplementedException();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API ACCESO DATOS CITAS INTRANT");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(cors);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
