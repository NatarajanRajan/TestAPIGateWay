using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using CoPass_WF.AuthServer.API.Model;

namespace CoPass_WF.AuthServer.Helper
{
    public interface IJWTAuthenticationManager
    {
        Task<AuthResponse> AuthenticateAsync(string username, string password, ISqlServerConnectionProvider provider);
    }

    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
         private readonly string tokenKey;

        public JWTAuthenticationManager(string tokenKey)
        {
            this.tokenKey = tokenKey;
        }

        public async Task<AuthResponse> AuthenticateAsync(string username, string password, ISqlServerConnectionProvider provider)
        {
            AuthResponse response = new AuthResponse();
            try
            {
                if (username == "super" && password == "super")
                {
                    int roleId = 0;

                    var claims = new List<Claim>();
                    claims.Add(new Claim("tenantId", "1"));
                    claims.Add(new Claim("subTenantId", "1"));
                    claims.Add(new Claim("userId", "1"));
                    claims.Add(new Claim(ClaimTypes.Role, roleId.ToString()));

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(tokenKey);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims.ToArray()),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    response.status = true;
                    response.message = "Valid user";
                    response.token = tokenHandler.WriteToken(token);
                }
            }catch(Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
                response.token = "";
            }
            return response;
        }

    
    }
}
