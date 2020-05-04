using System;
using System.Text;
using Common.ExceptionHandling;
using Common.GenericRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using XPAND.Captains.API.Models;
using XPAND.Captains.API.Repositories;
using XPAND.Captains.API.Services;

namespace XPAND.Captains.API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration configuration, IConfiguration config)
        {
            Configuration = configuration;
            _config = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddHealthChecks();
            services.AddDbContext<AbacTestCaptainsContext>(options => options.UseSqlServer(_config.GetConnectionString("SQLConnectionString"), builder => {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            }));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            byte[] jwtSecret = Encoding.ASCII.GetBytes(_config["JWTSecret"]);
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
                        ClockSkew = TimeSpan.FromSeconds(15),
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(jwtSecret),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = true,
                        ValidateLifetime = true
                    };
                });


            services.AddTransient<ICaptainRepository, CaptainRepository>();
            services.AddTransient<IShuttleRepository, ShuttleRepository>();
            services.AddTransient<IRobotRepository, RobotRepository>();

            services.AddTransient<IShuttleService, ShuttleService>();
            services.AddTransient<IDataReplicationService, DataReplicationService>();

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

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
