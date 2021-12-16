using ImportApi.Models;
using ImportApi.Repositories.Interface;
using ImportApi.Repositories.Repository;
using ImportApi.Services.Interface;
using ImportApi.Services.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ImportApi
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
            // Registrando DbContext
#if DEBUG
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
#else
            services.AddDbContext<AppDbContext>(options =>options
                .UseSqlServer($"Server={Environment.MachineName}{Configuration.GetConnectionString("DataBase")}; " +
                              $"Database={Configuration.GetConnectionString("Database")}; Trusted_Connection=False; MultipleActiveResultSets=False; " +
                              $"User ID={Configuration.GetConnectionString("User")};" +
                              $"Password={Configuration.GetConnectionString("Password")}; " +
                              $"Encrypt=True; TrustServerCertificate=True;"));
#endif

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Import API",
                    Version = "v1.0",
                    Description = "",
                });
            });

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            }).AddJsonOptions(options => {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.DictionaryKeyPolicy = null;

            });

            services.AddScoped<IImportService, ImportService>();
            services.AddScoped<IArquivoRepository, ArquivoRepository>();
            services.AddScoped<IRegistroRepository, RegistroRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod().AllowAnyOrigin());
                    //.SetIsOriginAllowed((origin) => true));
                    //.AllowCredentials()
                    //.SetIsOriginAllowed((origin) => true));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Import API");
                options.DefaultModelsExpandDepth(-1);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors("CorsPolicy");
            });
        }
    }
}
