using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WotStatApi.Services;

namespace WotStatApi
{
    public class Startup
    {
        private readonly string AllowedOrigins = "_AllowedOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "WotStat API", Version = "v1" });
            });

            services.AddRouting(c =>
            {
                c.LowercaseUrls = true;
            });

            services.AddScoped<IStatService, StatServiceWrapper>();

            services.AddCors(c =>
            {
                c.AddPolicy(AllowedOrigins, builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WotStat API v1");
                // serve the Swagger UI at the app's root
                c.RoutePrefix = string.Empty;
            });

            app.UseCors(AllowedOrigins);

            app.UseMvc();
        }
    }
}
