using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebSwagger.Data;
using WebSwagger.Models;

namespace WebSwagger.Validation
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, Customer user);
    }

    public class TokenService : ITokenService
    {
        private TimeSpan ExpiryDuration = new TimeSpan(0, 30, 0);
        public string BuildToken(string key, string issuer, Customer user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.name),
                new Claim(ClaimTypes.NameIdentifier,
                Guid.NewGuid().ToString())
             };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }

    public class CustomerValidator:AbstractValidator<Customer>
    {
        public CustomerValidator(CustomerRepository data)
        {
            RuleFor(x => x.name).NotEmpty().MinimumLength(2);
        }
    }
}
