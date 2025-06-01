using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FixTimeBack.Custom
{
    public class DecodeToken : IDecodeToken
    {
        private readonly IConfiguration _configuration;

        public DecodeToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ClaimsPrincipal GetPrincipalFromExpirationToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["jwt:key"]!);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["jwt:issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["jwt:audience"],
                    ValidateLifetime = true, // Importante para validar expiración
                    ClockSkew = TimeSpan.Zero, // Evita tolerancia de 5 min por defecto
                    RoleClaimType = "role" // Para acceder a rol directamente si lo necesitas
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Validación adicional opcional:
                if (validatedToken is not JwtSecurityToken jwtToken ||
                    !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
