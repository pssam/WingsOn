using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using WingsOn.Api.BusinessLogic.CommandHandlers;
using WingsOn.Api.BusinessLogic.Factories;
using WingsOn.Api.BusinessLogic.QueryHandlers;
using WingsOn.Api.ExceptionHandling;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            InitilaizeLogger(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add<ExceptionFilter>())
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerDocument();
            RegisterRepositories(services);
            RegisterBusinessLogic(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddSerilog();

            app.UseSwagger();
            app.UseSwaggerUi3();

            app.UseMvc();
        }

        private void RegisterBusinessLogic(IServiceCollection services)
        {
            services.AddTransient<IAddPassengerCommandHandler, AddPassengerCommandHandler>();
            services.AddTransient<IUpdatePersonAddressCommandHandler, UpdatePersonAddressCommandHandler>();

            services.AddTransient<IGetPassengersQueryHandler, GetPassengersQueryHandler>();

            services.AddTransient<IBookingFactory, BookingFactory>();
        }

        private static void InitilaizeLogger(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(configuration)
                         .CreateLogger();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddSingleton<IRepository<Person>, PersonRepository>();
            services.AddSingleton<IRepository<Booking>, BookingRepository>();
            services.AddSingleton<IRepository<Flight>, FlightRepository>();
        }
    }
}