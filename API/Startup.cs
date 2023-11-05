using API.Helpers;
using Domain.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using Hangfire;
using API.Data;
using Serilog;
using System.Text.Json.Serialization;

namespace API
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

            //Dependency Injection
            services.RegisterServices();
            services.AddAutoMapper(typeof(Startup));

            
            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddCors();
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            // Sql Server
            services.AddDbContext<ApplicationContext>(option =>
            {
                 option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())

            using (var context = scope.ServiceProvider.GetService<ApplicationContext>())
            {
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            // Middleware that will add Serilog exception log
            loggerFactory.AddSerilog();
            app.UseMiddleware<SerilogMiddleware>();

            // Middleware that will issue HTTP response codes redirecting from http to https
            app.UseHttpsRedirection();

            // call frontend
            app.Use(async (context, next) =>
            {
                await next().ConfigureAwait(true);
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/index.html";
                    await next().ConfigureAwait(true);
                }
            });
            app.UseStaticFiles();
            app.UseDefaultFiles();
            //

            // Middleware that matches request to an endpoint.
            app.UseRouting();
            // cors
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("content-disposition"));

            // Middleware that adding authorization middleware to application
            app.UseAuthorization();

            
            
            // Middleware that execute the matched endpoint.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Seed data
            DBIntalizer.seed(app);

            // Hangfire
            //app.UseHangfireDashboard();
            //app.UseHangfireServer();

        }
    }
}
