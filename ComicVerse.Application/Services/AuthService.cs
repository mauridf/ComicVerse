using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ComicVerse.Application.DTOs;
using ComicVerse.Core.Entities;
using ComicVerse.Core.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ComicVerse.Application.Services
{
    public interface IAuthService
    {
        Task<UsuarioRespostaDto> Registrar(UsuarioRegistroDto registroDto);
        Task<UsuarioRespostaDto> Login(UsuarioLoginDto loginDto);
    }

    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IConfiguration _config;

        public AuthService(
            IUsuarioRepository usuarioRepo,
            IConfiguration config)
        {
            _usuarioRepo = usuarioRepo;
            _config = config;
        }

        public async Task<UsuarioRespostaDto> Registrar(UsuarioRegistroDto registroDto)
        {
            if (await _usuarioRepo.ExisteUsuario(registroDto.Email))
                throw new Exception("Email já está em uso");

            var usuario = new Usuario(registroDto.Nome, registroDto.Email)
            {
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registroDto.Senha)
            };

            await _usuarioRepo.Adicionar(usuario);

            return new UsuarioRespostaDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Token = CriarToken(usuario)
            };
        }

        public async Task<UsuarioRespostaDto> Login(UsuarioLoginDto loginDto)
        {
            var usuario = await _usuarioRepo.ObterPorEmail(loginDto.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginDto.Senha, usuario.PasswordHash))
                throw new Exception("Email ou senha incorretos");

            return new UsuarioRespostaDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Token = CriarToken(usuario)
            };
        }

        private string CriarToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Role, usuario.Role)
            };

            // Sua chave Base64 atual é adequada para SHA256
            var key = new SymmetricSecurityKey(
                Convert.FromBase64String(_config["Jwt:Key"])); // Convertendo de Base64 para bytes

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256); // Algoritmo compatível com 256+ bits

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(Convert.ToDouble(_config["Jwt:ExpireDays"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}