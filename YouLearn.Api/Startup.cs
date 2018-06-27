using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using YouLearn.Api.Security;
using YouLearn.Domain.Interfaces.Repositories;
using YouLearn.Domain.Interfaces.Services;
using YouLearn.Domain.Services;
using YouLearn.Infra.Persistance.EF;
using YouLearn.Infra.Persistance.Repositories;
using YouLearn.Infra.Transaction;

namespace YouLearn.Api
{
    public class Startup
    {
        private const string ISSUER = "c1f51f42";
        private const string AUDIENCE = "c6bbbb645024";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Adiciona a injeção de dependencia
            services.AddScoped<YouLearnContext, YouLearnContext>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //Services
            services.AddTransient<IServiceCanal, ServiceCanal>();
            services.AddTransient<IServicePlayList, ServicePlayList>();
            services.AddTransient<IServiceVideo, ServiceVideo>();
            services.AddTransient<IServiceUsuario, ServiceUsuario>();
            //Repositories
            services.AddTransient<IRepositoryCanal, RepositoryCanal>();
            services.AddTransient<IRepositoryPlayList, RepositoryPlayList>();
            services.AddTransient<IRepositoryVideo, RepositoryVideo>();
            services.AddTransient<IRepositoryUsuario, RepositoryUsuario>();

            //Configuracao Token Autenticacao Bearer
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations
            {
                Audience = AUDIENCE,
                Issuer = ISSUER,
                // tempo que o token vai expirar
                Seconds = int.Parse(TimeSpan.FromDays(1).TotalSeconds.ToString())

            };
            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.SigningCredentials.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            //Para todas as requisições serem necessaria o token, para um endpoint não exisgir o token
            //deve colocar o [AllowAnonymous]
            //Caso remova essa linha, para todas as requisições que precisar de token, deve colocar
            //o atributo [Authorize("Bearer")]
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            });

            //Para todas as requisições serem necessaria o token, para um endpoint não exisgir o token
            //deve colocar o [AllowAnonymous]
            //Caso remova essa linha, para todas as requisições que precisar de token, deve colocar
            //o atributo [Authorize("Bearer")]
            //services.AddMvc(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
            //        .RequireAuthenticatedUser().Build();

            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});

            //services.AddCors();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));



            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "DocumentoAPI", Version = "1" });
            });

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseCors(x =>
            //{
            //    x.AllowAnyHeader();
            //    x.AllowAnyMethod();
            //    x.AllowAnyOrigin();
            //});

            app.UseCors("MyPolicy");

            app.UseDeveloperExceptionPage();

            app.UseDatabaseErrorPage();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("http://18.231.180.155/YouLearn/swagger/v1/swagger.json", "DocumentoAPI - v1");
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "DocumentoAPI - v1");
            });
            //app.UseSwagger(c =>
            //{
            //    //Change the path of the end point , should also update UI middle ware for this change                
            //    c.RouteTemplate = "api-docs/{documentName}/swagger.json";
            //});
            //app.UseSwaggerUI(c =>
            //{
            //    //Include virtual directory if site is configured so
            //    c.RoutePrefix = "api-docs";
            //    c.SwaggerEndpoint("v1/swagger.json", "Api v1");
            //});

        }
    }
}
