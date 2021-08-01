using AutoMapper;
using ecommerce_api._Repositories.Interface;
using ecommerce_api._Repositories.Repositories;
using ecommerce_api._Services.Interface;
using ecommerce_api._Services.Services;
using ecommerce_api.Data;
using ecommerce_api.Helpers.AutoMapper;
using ecommerce_api.SchedulerHelper;
using ecommerce_api.SchedulerHelper.Jobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace ecommerce_api.Helpers.Extensions
{
    public static class IServiceCollectionExtensions
    {

        public static IServiceCollection AddDatabaseExtention(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with Scoped lifetime 
            var appsettings = configuration.GetSection("Appsettings").Get<Appsettings>();
            services.AddSingleton(appsettings);
            services.AddDbContextPool<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<IoTContext>(options => options.UseMySQL(configuration.GetConnectionString("IoTConnection")));

            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
          
            return services;
        }

        public static IServiceCollection AddRepositoriesExtention(this IServiceCollection services)
        {

            services.AddScoped(typeof(IECRepository<>), typeof(ECRepository<>));
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            return services;
        }

        public static IServiceCollection AddServicesExtention(this IServiceCollection services)
        {
            
            // services.AddScoped<IUserService, UserService>();
            
            //extension
            services.AddScoped<IMailExtension, MailExtension>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IJWTService, JWTService>();
        


            return services;
        }

        public static IServiceCollection AddShedulerExtention(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddQuartz(async q =>
            {
                q.SchedulerId = "ecommerce_api";

                // Thuc thi luc 6:00, lap lai 1 tieng 1 lan
                var option = new ReloadTodoJob();
                await new SchedulerBase<ReloadTodoJob>(configuration).Start(1, IntervalUnit.Hour, 6, 00);

                // Thuc thi luc 6:00 đên 21 gio la ngung lap lai 30 phut 1 lan
                var startAt = TimeSpan.FromHours(6);
                var endAt = TimeSpan.FromHours(21);
                var repeatMins = 30;
                await new SchedulerBase<ReloadDispatchJob>(configuration).Start(repeatMins, startAt, endAt);
            });

            // ASP.NET Core hosting
            services.AddQuartzServer(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });
            return services;
        }


        public static IServiceCollection AddHttpClientExtention(this IServiceCollection services, IConfiguration configuration)
        {
            var appsettings = configuration.GetSection("Appsettings").Get<Appsettings>();
            services.AddHttpClient("default", client =>
            {
                client.BaseAddress = new Uri(appsettings.API_AUTH_URL);

                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            return services;
        }

        public static IServiceCollection AddAuthenticationWithSwaggerExtention(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // which means that the authentication requires HTTPS for the metadata address or authority.
                options.SaveToken = true; // which saves the JWT access token in the current HttpContext
                                          // we can retrieve it using the method await HttpContext.GetTokenAsync(“Bearer”, “access_token”)
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false

                    // ClockSkew = TimeSpan.FromMinutes(1) //  which gives an allowance time for the token expiration validation
                };
                //options.Events = new JwtBearerEvents
                //{
                //    OnMessageReceived = context =>
                //    {
                //        var accessToken = context.Request.Query["access_token"];
                //        var token = context.Request.Headers.FirstOrDefault(x => x.Key == "Authorization");
                //        // If the request is for our hub...
                //        var path = context.HttpContext.Request.Path;
                //        if (!string.IsNullOrEmpty(accessToken) &&
                //            (path.StartsWithSegments("/ec-hub")))
                //        {
                //            // Read the token out of the query string
                //            context.Token = accessToken;
                //        }
                //        return Task.CompletedTask;
                //    }
                //};
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Digital mixing room system", Version = "3.0.0" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                     {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                    }
                });

            });

            return services;
        }
        public static IServiceCollection AddAutoMapperExtention(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IMapper>(sp =>
            {
                return new Mapper(AutoMapperConfig.RegisterMappings());
            });
            services.AddSingleton(AutoMapperConfig.RegisterMappings());
            return services;
        }
    }
}
