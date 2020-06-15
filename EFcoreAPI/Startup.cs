using System.Net.Mime;
using System.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EFcoreBL.Interface;
using EFcoreBL.Repository;
using EFcoreDAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Microsoft.AspNetCore.Http.Features;

namespace EFcoreAPI
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;

        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            // services.AddControllers().AddNewtonsoftJson(
            //     options => options.SerializerSettings.ReferenceLoopHandling 
            //     = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            // );
            string _dbConnectionString = Configuration["ConnectionString:DbConnectionString"];
            services.AddTransient<IDbConnection>(p => new SqlConnection(_dbConnectionString));
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(_dbConnectionString,
                x => x.MigrationsAssembly("EFcoreDAL")));
            // services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>(options =>
            // options.UseSqlite(_dbConnectionString,  
            // x => x.MigrationsAssembly("EFcoreDAL")));

            services.AddAuthentication(p =>
            {
                p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(j =>
            {
                j.SaveToken = true;
                j.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwtProp:key"])),
                    ValidIssuer = Configuration["jwtProp:validIssuer"],
                    ValidAudience = Configuration["jwtProp:validAudience"],
                    // ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true


                };
            });
            /* Add Cookie */
            services.AddDistributedMemoryCache();
            services.AddSession(s =>
            {
                s.IdleTimeout = TimeSpan.FromHours(2);
                s.Cookie.HttpOnly = true;
                s.Cookie.IsEssential = true;
            });
            services.Configure<FormOptions>(x =>
            {
                x.MultipartBodyLengthLimit = 1024000000;
            });
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<IRole, RoleRepository>();
            services.AddScoped<IUserWithRole, UserWithRoleRepository>();
            services.AddScoped<IMenu, MenuRepository>();
            services.AddScoped<IUserMenu, UserMenuRepository>();
            services.AddScoped<ICategory, CategoryRepository>();
            services.AddScoped<IPost, PostRepository>();
            services.AddScoped<IPosts, PostsRepository>();
            services.AddScoped<IPostCategory, PostCategoryRepository>();
            services.AddScoped<IMultipleImageData, MultipleImageDataRepository>();
            services.AddScoped<IMedia,MediaRepository>();
            services.AddAutoMapper(typeof(MapperProfile));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseSession();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
