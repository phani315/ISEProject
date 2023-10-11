using Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Kanini.InvestmentSearchEngine.LoginWatchList.Services
{
    /// <summary>
    /// This class provides services for generating authentication tokens.
    /// It implements the <see cref="ITokenGenerate"/> interface.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TokenService : ITokenGenerate
    {
        #region Properties
        private readonly SymmetricSecurityKey _key;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration object used to retrieve the token key.</param>
        public TokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }

        #endregion

        #region GenerateToken Method

        /// <summary>
        /// Generates a JSON Web Token (JWT) for a user.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <returns>The generated JWT token.</returns>
        public string GenerateToken(UserLoginDTO user)
        {
            var emailClaim = user.EmailId != null ? new Claim(JwtRegisteredClaimNames.NameId, user.EmailId) : null;

            // User identity claims
            var claims = new List<Claim>();
            if (emailClaim != null)
            {
                claims.Add(emailClaim);
            }

            // Signature algorithm
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            // Assembling the token details
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = cred
            };

            // Using the handler to generate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var myToken = tokenHandler.CreateToken(tokenDescription);
            string token = tokenHandler.WriteToken(myToken);

            return token;
        }

        #endregion
    }
}
