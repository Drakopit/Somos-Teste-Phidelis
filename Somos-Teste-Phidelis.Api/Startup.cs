using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Somos_Teste_Phidelis.Api.HostedService;
using Somos_Teste_Phidelis.Domain.Config;
using Somos_Teste_Phidelis.Handler;
using Somos_Teste_Phidelis.Repository;
using Somos_Teste_Phidelis.Repository.Interfaces;
using Somos_Teste_Phidelis.Repository.Repositories;

namespace Somos_Teste_Phidelis.Api
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

            var timeSetSection = Configuration.GetSection("TimeSet");
            services.Configure<TimeSet>(timeSetSection);  


            services.AddDbContext<PhidelisContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("Connection"));
            });
            
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<ITimerRefreshHandler, TimerRefreshHandler>();
            
            services.AddHostedService<TimerHostedService>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Somos_Teste_Phidelis.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Somos_Teste_Phidelis.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
