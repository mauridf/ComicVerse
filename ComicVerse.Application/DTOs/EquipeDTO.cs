using System.ComponentModel.DataAnnotations;

namespace ComicVerse.Application.DTOs
{
    public class EquipeDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string? Imagem { get; set; }
        public string? Descricao { get; set; }
        public DateTime CriadaEm { get; set; }
        public DateTime? AlteradaEm { get; set; }

        public ICollection<PersonagemDTO> Personagens { get; set; } = new List<PersonagemDTO>();
        public ICollection<HQDTO> HQs { get; set; } = new List<HQDTO>();
    }

    public class CreateEquipeDTO
    {
        [Required]
        public string Nome { get; set; }
        public string? Imagem { get; set; }
        public string? Descricao { get; set; }
        public ICollection<Guid> PersonagensIds { get; set; } = new List<Guid>();
    }

    public class UpdateEquipeDTO
    {
        [Required]
        public string Nome { get; set; }
        public string? Imagem { get; set; }
        public string? Descricao { get; set; }
        public ICollection<Guid> PersonagensIds { get; set; } = new List<Guid>();
    }
}