using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TestAPI5.Contracts;
using TestAPI5.Models;
using TestAPI5.Repositories;
using TestAPI5.Services;

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
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestAPI5", Version = "v1" });
            });

            // Database connection with PostgreSQL
            services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddLogging();

            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUnclassifiedMessageService, UnclassifiedMessageService>();
            services.AddScoped<IComputerTaskService, ComputerTaskService>();
            services.AddScoped<IComputerService, ComputerService>();
            services.AddScoped<IProbeDataService, ProbeDataService>();
            services.AddScoped<IProbeService, ProbeService>();

            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IComputerRepository, ComputerRepository>();
            services.AddScoped<IUnclassifiedMessageRepository, UnclassifiedMessageRepository>();
            services.AddScoped<IComputerTaskRepository, ComputerTaskRepository>();
            services.AddScoped<IProbeDataRepository, ProbeDataRepository>();
            services.AddScoped<IProbeRepository, ProbeRepository>();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestAPI5 v1");
            });

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
