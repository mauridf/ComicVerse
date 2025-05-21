﻿namespace ComicVerse.Application.DTOs
{
    public class UsuarioLoginDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class UsuarioRegistroDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class UsuarioRespostaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
