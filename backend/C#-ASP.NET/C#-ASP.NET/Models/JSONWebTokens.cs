namespace C__ASP.NET.models
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Json;
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.IdentityModel.Tokens;
    public class JSONWebTokens(string username)
    {
        public string? Token { get; set; } = GenerateToken(username);

        private static string GenerateToken(string username)
        {
            string? secretId = Environment.GetEnvironmentVariable("SECRET_ID");
            string? secretValue = Environment.GetEnvironmentVariable("SECRET_VALUE");
            string? clientId = Environment.GetEnvironmentVariable("CLIENT_ID");

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secretValue);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim("sub",username)
                ,new Claim("aud","tableau")
                ,new Claim("jti",DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fff tt"))
                ,new Claim("iss",clientId)
                ,new Claim("scp","tableau:views:embed")
                ,new Claim("scp","tableau:views:embed_authoring")
                ,new Claim("scp","tableau:metrics:embed")
                ,new Claim("scp"," ")      //if you really just need one scope, you can still add a dummy one just to force it to create the List Type, as expected by Tableau        
			}),

                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            token.Header.Add("iss", clientId);
            token.Header.Add("kid", secretId);

            return tokenHandler.WriteToken(token);
        }
    }
}
