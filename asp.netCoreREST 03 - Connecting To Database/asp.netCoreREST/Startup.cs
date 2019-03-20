using asp.netCoreREST.Business;
using asp.netCoreREST.Business.Implementation;
using asp.netCoreREST.Model.Context;
using asp.netCoreREST.Repository;
using asp.netCoreREST.Repository.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace asp.netCoreREST {
    public class Startup {

        private readonly ILogger _logger;
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _enviroment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _enviroment = environment;
            _logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

            if (_enviroment.IsDevelopment()) {
                try {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                    var evolve = new Evolve.Evolve(evolveConnection, msg => _logger.LogInformation(msg)) {
                        Locations = new List<string> { "db/migrations" },
                        IsEraseDisabled = true
                    };
                    evolve.Migrate();
                }
                catch (Exception ex) {
                    _logger.LogCritical("Database migration failed.", ex);
                    throw;
                }
            }

            //Add Framework services
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddApiVersioning();
            
            //Dependency Injection
            services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
            services.AddScoped<IPersonRepository, PersonRepositoryImpl>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
