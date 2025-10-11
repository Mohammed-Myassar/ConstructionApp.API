using Buisnes_Logic.Interface;
using Domain.Account_Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Buisnes_Logic.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration configuration;

        public LoginService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Login(string username, string password)
        {
            // Validate username and password
            if (username == configuration["LoginInfo:userName"]
                || password == configuration["LoginInfo:password"])
            {
                // Create user
                var user = CreateAccount("Mohammed", "Myassar");

                // Returns Token
                return CreateToken(user);
            }
            else
                throw new InvalidOperationException("Usaer name or password error");
        }

        private Account CreateAccount(string fistName, string LastName)
        {
            return new Account(1, fistName, LastName);
        }

        private string CreateToken(Account account)
        {
            // Create securety key by Encoding Secret Key
            var securetyKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(configuration["Authentication:SecretKey"]));

            // Creadintioal signing using algorithm HS256
            var signingCreadintioal = new SigningCredentials(securetyKey,
                SecurityAlgorithms.HmacSha256
                );

            // Assemble and create the complete token
            var secuerityToken = new JwtSecurityToken(
                configuration["Authentication:Issuer"],
                configuration["Authentication:Audience"],
                new List<Claim>()
                {
                        new Claim(ClaimTypes.GivenName, account.FirstName),
                        new Claim(ClaimTypes.Surname, account.LastName),
                        new Claim(ClaimTypes.NameIdentifier, account.Id.ToString())
                },
                DateTime.UtcNow,
                DateTime.UtcNow.AddMonths(1),
                signingCreadintioal
                );

            // Write Token
            var sirializedToken = new JwtSecurityTokenHandler().WriteToken(secuerityToken);

            return sirializedToken;
        }
    }
}
