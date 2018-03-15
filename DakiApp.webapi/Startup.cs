using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DakiApp.domain.Contracts;
using DakiApp.domain.Entities;
using DakiApp.repository;
using DakiApp.repository.Context;
using DakiApp.repository.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace DakiApp.webapi {
    public class Startup {
        public IConfiguration Configuration { get; }

        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public void ConfigureServices (IServiceCollection services) {
            // services.AddDbContext<DakiAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContextPool<DakiAppContext> (options => options.UseSqlServer (Configuration.GetConnectionString ("DefaultConnection")));
             var signingConfigurations = new signingConfigurations();

            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(Configuration.GetSection("TokenConfigurations")).Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);
            
            services.AddAuthentication(authOptions =>{
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(bearerOptions =>{
                    var parametrosValidacao = bearerOptions.TokenValidationParameters;
                    parametrosValidacao.IssuerSigningKey = signingConfigurations.Key;
                    parametrosValidacao.ValidAudience = tokenConfigurations.Audience;
                    parametrosValidacao.ValidIssuer = tokenConfigurations.Issuer;
                    parametrosValidacao.ValidateIssuerSigningKey = true;

                    // Valida a assinatura de um token recebido
                    parametrosValidacao.ValidateIssuerSigningKey = true;

                    // Verifica se um token recebido ainda é válido
                    parametrosValidacao.ValidateLifetime = true;
                });
            
            services.AddAuthorization(auth => {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build());
            });

            services.AddMvc ().AddJsonOptions (option => {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddScoped (typeof (IBaseRepository<>), typeof (BaseRepository<>));
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new Info {
                    Version = "v1",
                        Title = "Api Forum",
                        Description = "Doc",
                        TermsOfService = "None",
                        Contact = new Contact {
                            Name = "FabioF",
                                Email = "f.freller@gmail.com",
                                Url = "www"
                        }
                });
                var caminhoBase = AppContext.BaseDirectory;
                var caminhoxml = Path.Combine (caminhoBase, "DakiApp.xml");
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseSwagger ();
            app.UseSwaggerUI (c => { c.SwaggerEndpoint ("/swagger/v1/swagger.json", "DakiApp"); });
            app.UseMvc ();

            app.Run (async (context) => {
                await context.Response.WriteAsync ("Hello World!");
            });
        }
    }
}