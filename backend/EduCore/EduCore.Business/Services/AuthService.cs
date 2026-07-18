using EduCore.Business.DTOs;
using EduCore.Business.Entities;
using EduCore.Business.Exceptions;
using EduCore.Business.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EduCore.Business.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
        {
            // 1. Buscar usuario
            var usuario = await _repository.ObtenerPorEmailAsync(dto.Email);
            if (usuario == null)
                throw new FunctionalException("CREDENCIALES_INVALIDAS", "Email o contraseña incorrectos");

            // 2. Verificar contraseña
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
                throw new FunctionalException("CREDENCIALES_INVALIDAS", "Email o contraseña incorrectos");

            // 3. Verificar que esté activo
            if (!usuario.Activo)
                throw new FunctionalException("USUARIO_INACTIVO", "El usuario está inactivo");

            // 4. Generar tokens
            var jwt = GenerarJwt(usuario);
            var refreshToken = GenerarRefreshToken(usuario.Id);

            await _repository.GuardarRefreshTokenAsync(refreshToken);

            return new TokenResponseDto
            {
                Token = jwt,
                RefreshToken = refreshToken.Token,
                Expiracion = DateTime.Now.AddMinutes(
                    int.Parse(_configuration["JwtSettings:ExpirationMinutes"]!))
            };
        }

        public async Task<TokenResponseDto> RefreshTokenAsync(string refreshToken)
        {
            // 1. Buscar el refresh token
            var token = await _repository.ObtenerRefreshTokenAsync(refreshToken);
            if (token == null || !token.EsValido())
                throw new FunctionalException("REFRESH_TOKEN_INVALIDO", "El refresh token no es válido");

            // 2. Marcar como usado
            token.Usar();
            await _repository.ActualizarRefreshTokenAsync(token);

            // 3. Generar nuevos tokens
            var nuevoJwt = GenerarJwt(token.Usuario);
            var nuevoRefreshToken = GenerarRefreshToken(token.UsuarioId);
            await _repository.GuardarRefreshTokenAsync(nuevoRefreshToken);

            return new TokenResponseDto
            {
                Token = nuevoJwt,
                RefreshToken = nuevoRefreshToken.Token,
                Expiracion = DateTime.Now.AddMinutes(
                    int.Parse(_configuration["JwtSettings:ExpirationMinutes"]!))
            };
        }

        private string GenerarJwt(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    int.Parse(_configuration["JwtSettings:ExpirationMinutes"]!)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private RefreshToken GenerarRefreshToken(int usuarioId)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            return new RefreshToken(token, usuarioId);
        }
    }
}
