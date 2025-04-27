using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TixTimeModels.Modelos;

namespace FixTimeBack.Custom
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJWT(Usuario modelo)
        {
            var userclaim = new[]
            {
                new Claim("UsuarioID",modelo.UsuarioID!.ToString()),
                new Claim("Nombre",modelo.NombreCompleto!),
                new Claim("role",modelo.TipoUsuario!)
            };

            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]!));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var jwtconfig = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: userclaim,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials

                );

            return new JwtSecurityTokenHandler().WriteToken(jwtconfig);
        }
    }
}
