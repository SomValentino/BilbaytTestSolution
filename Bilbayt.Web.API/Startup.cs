using Bilbayt.Data.Context;
using Bilbayt.Data.Interfaces;
using Bilbayt.Data.Repositories;
using Bilbayt.Web.API.Services;
using Bilbayt.Web.API.Services.Interfaces;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Bilbayt.Web.API.BackgroundJob.Config;
using Bilbayt.Web.API.BackgroundJob.Jobs;
using Bilbayt.Web.API.BackgroundJob;

namespace Bilbayt.Web.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bilbayt.Web.API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var path = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(path);
            });

            services.AddScoped<IDataContextSetting, DataContextSetting>(options =>
            {
                return new DataContextSetting
                {
                    ConnectionString = Configuration.GetValue<string>("connectionString"),
                    DatabaseName = Configuration.GetValue<string>("databaseName")
                };
            });

            var issuer = Configuration.GetValue<string>("baseUrl");
            var key = Configuration.GetValue<string>("jwtSecret");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = true,
                          ValidateAudience = true,
                          ValidateIssuerSigningKey = true,
                          ValidIssuer = issuer,
                          ValidAudience = issuer,
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                      };

                      options.Events = new JwtBearerEvents
                      {
                          OnAuthenticationFailed = context =>
                          {
                              if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                              {
                                  context.Response.Headers.Add("Token-Expired", "true");
                              }
                              return Task.CompletedTask;
                          }
                      };
                  });
            services.AddCors(options => options.AddPolicy("ApplicantPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailQueueRepository, EmailQueueRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();

            services.AddBackgroundJob<EmailQueueJob>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bilbayt.Web.API v1"));

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exception = context.Features.Get<IExceptionHandlerFeature>();

                    logger.LogError(exception.Error.Message, exception.Error);
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync(JsonConvert
                        .SerializeObject(new { errors = exception.Error.Message }));

                });
            });

            //app.UseHttpsRedirection();
            app.UseCors("ApplicantPolicy");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
