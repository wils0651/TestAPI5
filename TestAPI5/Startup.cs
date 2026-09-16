using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using TestAPI5.Contracts.Repositories;
using TestAPI5.Contracts.Services;
using TestAPI5.Models;
using TestAPI5.Repositories;
using TestAPI5.Serialization;
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

            // Every DateTime returned by this API is a UTC instant -- see UtcDateTimeConverter
            // for why Npgsql/System.Text.Json need help saying so explicitly.
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestAPI5", Version = "v1" });
            });

            // Database connection with PostgreSQL
            services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddLogging();

            // Services
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUnclassifiedMessageService, UnclassifiedMessageService>();
            services.AddScoped<IComputerTaskService, ComputerTaskService>();
            services.AddScoped<IComputerService, ComputerService>();
            services.AddScoped<IProbeDataService, ProbeDataService>();
            services.AddScoped<IProbeService, ProbeService>();
            services.AddScoped<ITemperatureStatisticService, TemperatureStatisticService>();
            services.AddScoped<IGarageDistanceService, GarageDistanceService>();
            services.AddScoped<IGarageEventLogService, GarageEventLogService>();

            // Repositories
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IComputerRepository, ComputerRepository>();
            services.AddScoped<IUnclassifiedMessageRepository, UnclassifiedMessageRepository>();
            services.AddScoped<IComputerTaskRepository, ComputerTaskRepository>();
            services.AddScoped<IProbeDataRepository, ProbeDataRepository>();
            services.AddScoped<IProbeRepository, ProbeRepository>();
            services.AddScoped<ITemperatureStatisticRepository, TemperatureStatisticRepository>();
            services.AddScoped<IGarageDistanceRepository, GarageDistanceRepository>();
            services.AddScoped<IGarageEventLogRepository, GarageEventLogRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Must run before other middleware so anything relying on the client's real
            // scheme/IP (behind the planned nginx reverse proxy) sees the forwarded values
            // instead of the proxy's own.
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

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
