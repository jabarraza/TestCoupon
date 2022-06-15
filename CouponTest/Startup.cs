using CouponTest.Middlewares;
using CouponTest.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace CouponTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //Agregamos variable del confi que contiene la ruta de la api a llamar de mercado libre
        private static string urlML = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["URLML"];
    
        public void ConfigureServices(IServiceCollection services)
        {
            //Registramos el servicio con el cual mandaremos abrir IHttpClientFactory desde ServiceApi
            services.AddHttpClient("MercadoLibre", client =>
            {
                client.BaseAddress = new Uri(urlML);
            }  
            );
            //Agregamos para inyenccion de dependencias de la interfaz 
            services.AddSingleton<IServiceApi,ServiceApi>();
            services.AddControllers();
            //Agregamos para generar documentacion de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "COUPON API",
                    Description = "API that obtains the list of items and the list of the 5 most favorites"
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CouponTest v1"));
            }
           //Registramos los errores cutomizados (middleware de errores)
            app.ConfigureCustomExceptionMiddleware();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
