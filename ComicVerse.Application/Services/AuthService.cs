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

            CriarPasswordHash(registroDto.Senha, out byte[] passwordHash, out byte[] passwordSalt);

            var usuario = new Usuario(registroDto.Nome, registroDto.Email)
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
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

            if (usuario == null || !VerificarPasswordHash(loginDto.Senha, usuario.PasswordHash, usuario.PasswordSalt))
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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("Jwt:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private void CriarPasswordHash(string senha, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        private bool VerificarPasswordHash(string senha, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}