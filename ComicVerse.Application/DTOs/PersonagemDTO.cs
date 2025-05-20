using System.ComponentModel.DataAnnotations;

namespace ComicVerse.Application.DTOs
{
    public class PersonagemDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string? Imagem { get; set; }
        public string? Descricao { get; set; }
        public DateTime CriadaEm { get; set; }
        public DateTime? AlteradaEm { get; set; }

        public ICollection<EquipeDTO> Equipes { get; set; } = new List<EquipeDTO>();
        public ICollection<HQDTO> HQs { get; set; } = new List<HQDTO>();
    }

    public class CreatePersonagemDTO
    {
        [Required]
        public string Nome { get; set; }
        public string? Imagem { get; set; }
        public string? Descricao { get; set; }
        public ICollection<Guid> EquipesIds { get; set; } = new List<Guid>();
    }

    public class UpdatePersonagemDTO
    {
        [Required]
        public string Nome { get; set; }
        public string? Imagem { get; set; }
        public string? Descricao { get; set; }
        public ICollection<Guid> EquipesIds { get; set; } = new List<Guid>();
    }
}