﻿using AspNetCore.Classes;
using AspNetCore.DBCoontext;
using AspNetCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AspNetCore
{
    // Ponto incial
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();

            // Aqui preciso dizer a classe Startup que tenho configurações manuais a fazer da classe MinhaConfiguracao
            services.Configure<MinhaConfiguracao>(Configuration.GetSection("MinhaConfiguracao"));

            // Para especificar a quantidade de erros de validação permitida. 
            services.AddMvc(options => options.MaxModelValidationErrors = 50);

            // Adciona como Singleton o Startup.
            services.AddSingleton(Configuration);

            // Adciona como SCOPED o UsuarioService se  o EF core for utlizado COM uso do Extension Method. 
            services.AddScoped<IUsuarioService, UsuarioService>();

            // Adcionar o Singleton no UsuarioService se  o EF core for utlizado  SEM do Extension Method. 
            //Services.Add(new ServiceDescriptor(typeof(IUsuarioService), typeof(UsuarioService), ServiceLifetime.Scoped)); 

            // Adcionar a connectionstring do BD como um serviço ao projeto e referenciar o LibraryContext que é minha classe dDbContext para EF, Code First e Migrations.
            services.AddDbContext<LibraryContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LibraryConnection")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Abaixo faço com que a aplicação ao iniciar apenas mostre a palavra Hello World.
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            //app.UseWelcomePage();
            //// Chamar pelo app.Use primeiro permite gerar uma sequencia de middleware ( httphandlers. ) antes de carregar a view _layout 
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello World From 1st Middleware!");

            //    await next();
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello World From 2nd Middleware!");

            //    await next();
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World From 3rd Middleware");
            //});

            //// Aqui middlaware para quando o usuario tentar na pagina Usuarios
            //app.Map("/Usuario", a => a.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Gotcha");
            //}));

            // Aqui escreve na tela através do middleware caso encontre o navegador do FireFox chamando o Método FirefoxRoute
            app.MapWhen(context => context.Request.Headers["User-Agent"].First().Contains("Firefox"), FirefoxRoute);

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                // New route Usuarios
                routes.MapRoute(
                   name: "usuario",
                   template: "usuario/",
                   defaults: new { controller = "Usuarios", action = "index" });
                // Default Route
                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
            });

        }

        private void FirefoxRoute(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Use este sistema inicialmente no Google Chrome onde foi testado!a");
            });
        }
    }
}
