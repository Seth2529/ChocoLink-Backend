using ChocoLink.Infra.ServiceToken;
using ChocoLink.Infra.ServiceToken.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChocoLink.Infra.ServiceToken
{
    public class TokenService : ITokenService
    {
        private readonly Token _token;

        public TokenService(IOptions<Token> token)
        {
            _token = token.Value;
        }
        public async Task<string> GerarTokenJWT(TokenRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.user))
            {
                throw new ArgumentNullException(nameof(request), "Request or usuario cannot be null");
            }

            byte[] secret = Encoding.ASCII.GetBytes(_token.Secret);
            DateTime expirationDateTime = DateTime.UtcNow.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Issuer = _token.Issuer,
                Audience = _token.Audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user", request.user),
                }),
                Expires = expirationDateTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = handler.CreateToken(descriptor);
            string tokenString = handler.WriteToken(token);

            // Log para verificar o token gerado
            Console.WriteLine($"Token Gerado: {tokenString}");

            return tokenString;
        }

        public async Task<JwtSecurityToken> DecodificarTokenJWT(string protectedText)
        {
            if (String.IsNullOrEmpty(protectedText))
            {
                throw new ArgumentNullException(nameof(protectedText), "Token cannot be null or empty");
            }

            // Limpar o token de possíveis prefixos
            protectedText = protectedText.Replace("Bearer ", "").Replace("bearer ", "");

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _token.Issuer,
                ValidAudience = _token.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_token.Secret))
            };

            var handler = new JwtSecurityTokenHandler();

            try
            {
                var principal = handler.ValidateToken(protectedText, validationParameters, out var validatedToken);
                var jwtToken = validatedToken as JwtSecurityToken;

                // Log para verificar o token decodificado
                Console.WriteLine($"Token Decodificado: {jwtToken}");

                return jwtToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao decodificar o token: {ex.Message}");
                throw new SecurityTokenException("Invalid token", ex);
            }
        }

    }
}