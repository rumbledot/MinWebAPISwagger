using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Runtime.CompilerServices;

namespace WebSwagger.Services
{
    public static class SwaggerService
    {
        public static WebApplication ConfigureApp(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.MapSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
                                    $"{app.Environment.ApplicationName} v1"));
            }

            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
