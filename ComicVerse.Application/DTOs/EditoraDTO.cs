using System.ComponentModel.DataAnnotations;

namespace ComicVerse.Application.DTOs
{
    public class EditoraDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string? Imagem { get; set; }
        public string? Descricao { get; set; }
        public DateTime CriadaEm { get; set; }
        public DateTime? AlteradaEm { get; set; }
    }

    public class CreateEditoraDTO
    {
        [Required]
        public string Nome { get; set; }
        public string? Imagem { get; set; }
        public string? Descricao { get; set; }
    }

    public class UpdateEditoraDTO
    {
        [Required]
        public string Nome { get; set; }
        public string? Imagem { get; set; }
        public string? Descricao { get; set; }
    }
}