using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using WebSwagger.Data;
using WebSwagger.Models;
using WebSwagger.Validation;

namespace WebSwagger.Helpers
{
    public static class WebApiSetupHelper
    {
        private static WebApplicationBuilder? _builder;
        public static string JWT_ISSUER { get => _builder is null ? string.Empty : _builder.Configuration["Jwt:Issuer"]; }
        public static string JWT_KEY { get => _builder is null ? string.Empty : _builder.Configuration["Jwt:Key"]; }
        public static string SQLCONN { get => _builder is null ? string.Empty : _builder.Configuration["SQL_CONN"]; }

        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WebSwagger",
                    Version = "v1"
                });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                x.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(securitySchema, new[] { "Bearer" });
                x.AddSecurityRequirement(securityRequirement);
            });

            builder.Services.AddSingleton<ITokenService, TokenService>();
            builder.Services.AddSingleton<WeatherData>();
            builder.Services.AddDbContext<CustomerRepository>(o => {
                o.UseSqlServer(SQLCONN);
            });

            builder.Services.AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<Customer>());
            //builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();            

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = JWT_ISSUER,
                        ValidAudience = JWT_ISSUER,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_KEY))
                    };
                });

            builder.Services.AddAuthorization(options =>
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build()
            );

            return builder;
        }

        public static WebApplication BuildApp(this WebApplicationBuilder builder)
        {
            var app = builder.Build();
            SetBuilder(builder);

            return app;
        }

        public static WebApplicationBuilder? GetBuilder()
        {
            return _builder;
        }

        public static void SetBuilder(WebApplicationBuilder builder)
        {
            _builder = builder;
        }
    }
}
