using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System;
using TestAPI5.Models;

namespace TestAPI5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private const string AllowAllOrigins = "AllowAllOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowAllOrigins,
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            }); // Added

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestAPI5", Version = "v1" });
            });

            // In Memory DB
            //services.AddDbContext<TodoContext>(opt =>
            //                                   opt.UseInMemoryDatabase("TestAPI5"));

            // Database connection with PostgreSQL
            services.AddDbContext<TodoContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));



            services.AddLogging();
            //services.AddHttpLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                { 
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestAPI5 v1");
                    c.RoutePrefix = "api/docs";
                });
            }

            app.UseCors(AllowAllOrigins);

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
