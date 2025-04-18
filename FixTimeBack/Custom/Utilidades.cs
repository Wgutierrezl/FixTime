using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TixTimeModels.Modelos;

namespace FixTimeBack.Custom
{
    public class Utilidades
    {
        private readonly IConfiguration _configuration;

        public Utilidades(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        //Este metodo nos sirve para poder encriptar las contraseñas en la
        //Base de datos
        public string EncryptSHA256(string text)
        {
            using(SHA256 sha256=SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder builder = new StringBuilder();
                for(int i=0;i<bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();

            }

        }

        //Con este metodo generamos el token, para que el usuario pueda realizar
        //las diferentes transacciones en la aplicacion
        public string GenerateJWT(Usuario modelo)
        {
            var userclaim = new[]
            {
                new Claim("UsuarioID",modelo.UsuarioID!.ToString()),
                new Claim("Nombre",modelo.NombreCompleto!),
                new Claim("role",modelo.TipoUsuario!)
            };

            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]!));
            var credentials=new SigningCredentials(securitykey,SecurityAlgorithms.HmacSha256);

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
