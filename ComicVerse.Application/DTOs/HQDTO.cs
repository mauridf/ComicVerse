using System.ComponentModel.DataAnnotations;

namespace ComicVerse.Application.DTOs
{
    public class HQDTO
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string? Imagem { get; set; }
        public string? Descricao { get; set; }
        public DateTime CriadaEm { get; set; }
        public DateTime? AlteradaEm { get; set; }

        // Relacionamentos
        public ICollection<EditoraDTO> Editoras { get; set; } = new List<EditoraDTO>();
        public ICollection<PersonagemDTO> Personagens { get; set; } = new List<PersonagemDTO>();
        public ICollection<EquipeDTO> Equipes { get; set; } = new List<EquipeDTO>();
        public ICollection<EdicaoDTO> Edicoes { get; set; } = new List<EdicaoDTO>();
    }

    public class CreateHQDTO
    {
        [Required]
        public string Titulo { get; set; }
        public string? Imagem { get; set; }
        public string? Descricao { get; set; }
        public ICollection<Guid> EditorasIds { get; set; } = new List<Guid>();
        public ICollection<Guid> PersonagensIds { get; set; } = new List<Guid>();
        public ICollection<Guid> EquipesIds { get; set; } = new List<Guid>();
    }

    public class UpdateHQDTO
    {
        [Required]
        public string Titulo { get; set; }
        public string? Imagem { get; set; }
        public string? Descricao { get; set; }
        public ICollection<Guid> EditorasIds { get; set; } = new List<Guid>();
        public ICollection<Guid> PersonagensIds { get; set; } = new List<Guid>();
        public ICollection<Guid> EquipesIds { get; set; } = new List<Guid>();
    }
}