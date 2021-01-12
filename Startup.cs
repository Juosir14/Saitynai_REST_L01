using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saitynai_REST_L01.Data;
using Saitynai_REST_L01.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
using Saitynai_REST_L01.Helpers;
using Saitynai_REST_L01.Services;
//using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using AutoMapper;
using Newtonsoft.Json.Serialization;


namespace Saitynai_REST_L01
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
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme){
            //     .AddJwtBearer(opt =>{
            //         opt.Audience => 
            //     });
            // }
            //--------senas

            services.AddCors();
            services.AddControllers();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();


            // services.AddAuthentication(options => 
            // {
            //     options.DefaultAuthenticateScheme = "JwtBearer";
            //     options.DefaultChallengeScheme = "JwtBearer";
            // })
            // .AddJwtBearer("JwtBearer",jwtOptions => 
            // {
            //     jwtOptions.TokenValidationParameters = new TokenValidationParameters()
            //     {
            //         IssuerSigningKey = TokenController.SIGNING_KEY,
            //         ValidateIssuer = false,
            //         ValidateAudience = false,
            //         ValidateIssuerSigningKey = true,
            //         ValidateLifetime = true,
            //         ClockSkew = TimeSpan.FromMinutes(5)
            //     };
            // });







            services.AddDbContext<CommanderContext>(opt => opt.UseSqlServer
                (Configuration.GetConnectionString("CommanderConnection")));

            services.AddDbContext<PlayerContext>(opt => opt.UseSqlServer
                (Configuration.GetConnectionString("CommanderConnection")));

            services.AddControllers().AddNewtonsoftJson(s => {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            
            services.AddScoped<ICommanderRepo, SqlCommanderRepo>();
            services.AddScoped<IPlayerRepo, SqlPlayerRepo>();

            //senas---------------------------

            //services.AddMvc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

             //app.UseRouting();

            // global cors policy
         //   app.UseCors(x => x
         //       .AllowAnyOrigin()
        //        .AllowAnyMethod()
       //         .AllowAnyHeader());

            //app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseMvc();
        }
    }
}
