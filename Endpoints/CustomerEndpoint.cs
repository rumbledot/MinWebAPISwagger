using FluentValidation;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using WebSwagger.Data;
using WebSwagger.Helpers;
using WebSwagger.Models;
using WebSwagger.Validation;

namespace WebSwagger.Endpoints
{
    public static class CustomerEndpoint
    {
        public static WebApplication SetCustomerEndpoint(this WebApplication app)
        {
            app.MapPost("/login", async (HttpContext http, ITokenService tokenService, CustomerRepository repo, Customer customer) =>
            {
                if (customer is null)
                {
                    http.Response.StatusCode = 401;
                    return;
                }

                var user = repo.Customers.AsEnumerable()
                    .Where(c => c.name.Equals(customer.name) && c.password.Equals(customer.password))
                    .DefaultIfEmpty(null)
                    .FirstOrDefault();

                if (user is null)
                {
                    http.Response.StatusCode = 401;
                    return;
                }

                var token = tokenService.BuildToken(WebApiSetupHelper.JWT_KEY, WebApiSetupHelper.JWT_ISSUER, customer);
                await http.Response.WriteAsJsonAsync(new { token = token });

                return;
            })
                .AllowAnonymous()
                .WithMetadata(new SwaggerOperationAttribute("Login", "Login to get api token"))
                .WithName("Login")
                .WithTags("Authorization");

            app.MapPost("/customers", (CustomerRepository repo) =>
            {
                return Results.Ok(repo.Customers.ToList());
            })
                .RequireAuthorization()
                .WithMetadata(new SwaggerOperationAttribute("List all customers", "View current list of customers"))
                .WithName("List all customers")
                .WithTags("Customer");

            app.MapPost("/addcustomer", (CustomerRepository repo, IValidator<Customer> validator, Customer customer) =>
            {
                var validationResult = validator.Validate(customer);

                if (!validationResult.IsValid)
                {
                    var error = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
                    return Results.BadRequest(error);
                }

                repo.Customers.Add(customer);
                repo.SaveChanges();

                return Results.Created($"Customer {customer.name} added", customer);
            })
                .RequireAuthorization()
                .WithMetadata(new SwaggerOperationAttribute("Add new customers", "Add new customer to list"))
                .WithName("AddCustomer")
                .WithTags("Customer");

            app.MapPost("/deletecustomer", (CustomerRepository repo, IValidator<Customer> validator, Customer customer) =>
            {
                repo.Customers.Remove(customer);
                repo.SaveChanges();
            })
                .RequireAuthorization()
                .WithMetadata(new SwaggerOperationAttribute("Delete customers", "Remove a customer from list"))
                .WithName("DeleteCustomer")
                .WithTags("Customer");

            return app;
        }
    }
}
