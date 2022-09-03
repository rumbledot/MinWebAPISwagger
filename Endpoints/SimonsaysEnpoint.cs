using FluentValidation;
using WebSwagger.Data;
using WebSwagger.Models;
using WebSwagger.Validation;

namespace WebSwagger.Endpoints
{
    public static class SimonsaysEnpoint
    {
        public static WebApplication SetSimonsaysEndpoint(this WebApplication app)
        {
            app.MapGet("/simon", () =>
            {
                return "Hi, I am Simon.";
            })
                .RequireAuthorization()
                .WithName("SimonHello")
                .WithTags("Simon");

            app.MapPost("/say", (SimonSay say) =>
            {
                return Results.Ok($"Simon say {say.CommandTo} and comply and he will {say.DoAction}");
            })
                .RequireAuthorization()
                .WithName("SimonSays")
                .WithTags("Simon");

            return app;
        }
    }
}
