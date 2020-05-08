using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EFcoreBL.Interface;
using EFcoreBL.Repository;
using EFcoreDAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EFcoreAPI
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
            string _dbConnectionString = Configuration["ConnectionString:DbConnectionString"];
            services.AddTransient<IDbConnection>(p => new SqlConnection(_dbConnectionString));
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(_dbConnectionString,
                x => x.MigrationsAssembly("EFcoreDAL")));
                // services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>(options =>
                // options.UseSqlite(_dbConnectionString,  
                // x => x.MigrationsAssembly("EFcoreDAL")));
            services.AddScoped<IUser, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
