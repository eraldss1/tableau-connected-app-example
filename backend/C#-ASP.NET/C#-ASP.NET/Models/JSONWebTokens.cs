namespace C__ASP.NET.models
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Json;
    using Microsoft.IdentityModel.Tokens;
    public class JSONWebTokens(string username)
    {
        public string? Token { get; set; } = GenerateToken(username);

        private static string GenerateToken(string username)
        {
            #region Variables
            string? secretId = Environment.GetEnvironmentVariable("SECRET_ID");
            string? secretValue = Environment.GetEnvironmentVariable("SECRET_VALUE");
            string? clientId = Environment.GetEnvironmentVariable("CLIENT_ID");

            double tokenExpiryInMinutes = 5; // Max of 10 minutes.

            string[] scopes = ["tableau:views:embed", "tableau:views:embed_authoring"];
            #endregion

            #region JWT generation
            string kid = secretId;
            string iss = clientId;
            string sub = username;
            string aud = "tableau";
            DateTime exp = DateTime.UtcNow.AddMinutes(tokenExpiryInMinutes);
            string jti = Guid.NewGuid().ToString();
            string scp = JsonSerializer.Serialize(scopes);

            Dictionary<string, object> headerClaims = new() { { "iss", iss } };

            byte[] key = Encoding.ASCII.GetBytes(secretValue);

            SigningCredentials signingCredentials = new(
                new SymmetricSecurityKey(key) { KeyId = kid },
                SecurityAlgorithms.HmacSha256Signature);

            List<Claim> claims = new(
                new[]
                {
                    new Claim("sub", sub),
                    new Claim("jti", jti),
                    new Claim("scp", scp, JsonClaimValueTypes.JsonArray),
                });

            ClaimsIdentity subject = new(claims);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Audience = aud,
                Subject = subject,
                AdditionalInnerHeaderClaims = headerClaims,
                SigningCredentials = signingCredentials,
                Expires = exp
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string jwt = tokenHandler.WriteToken(token);

            #endregion

            return jwt;
        }
    }
}
