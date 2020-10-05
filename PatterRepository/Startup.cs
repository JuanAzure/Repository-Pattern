using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PatterRepository.Extensions;
using PatterRepository.Utility;

namespace PatterRepository
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            ///LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Validacion de entidades por EFCore
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.ConfigureLoggerService();
            services.ConfigureSqlServerContext(Configuration);
            services.ConfigureRepositoryWrapper();
            //services.ConfigureCors();

            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson()
              .AddXmlDataContractSerializerFormatters()
            .AddCustomCSVFormatter();

            services.AddScoped<TemplateGenerator>();
            services.AddAutoMapper(typeof(Startup));
    

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.ConfigureCor();

            //app.ConfigureExceptionHandler(logger);
            app.ConfigureCustomExceptionMiddleware();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
