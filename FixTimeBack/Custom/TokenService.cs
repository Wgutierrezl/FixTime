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

        //Este es un token para poder recuperar la contraseña, por lo que le damos un expire de 5 Minutos
        public string GenerateJWtByNewPassword(Usuario model)
        {
            var userclaim = new[]
            {
                new Claim("UsuarioID",model.UsuarioID!.ToString())

                //Vamos a tapar este claim, ya que no es necesario mandarle el rol, no es un token que se va a utilizar en todos los casos, solo para
                //cuando vayamos a recuparar nuestra contraseña

                //new Claim("role",model.TipoUsuario!)
            };

            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]!));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var jwtconfig = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: userclaim,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: credentials

                );

            return new JwtSecurityTokenHandler().WriteToken(jwtconfig);
        }
    }
}
