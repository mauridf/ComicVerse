using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ComicVerse.Application.DTOs
{
    public class EdicaoDTO
    {
        public Guid Id { get; set; }
        public Guid HQId { get; set; }
        public int Numero { get; set; }
        public string? Titulo { get; set; }
        public DateTime? DataLancamento { get; set; }
        public string? Imagem { get; set; }
        public string? Observacoes { get; set; }
        public bool Lida { get; set; }
        public decimal? Nota { get; set; }
        public DateTime CriadaEm { get; set; }
        public DateTime? AlteradaEm { get; set; }

        public HQDTO HQ { get; set; }
    }

    public class CreateEdicaoDTO
    {
        [Required]
        public int Numero { get; set; }
        public string? Titulo { get; set; }

        private DateTime? _dataLancamento;

        [JsonPropertyName("dataLancamento")]
        public DateTime? DataLancamento
        {
            get => _dataLancamento;
            set => _dataLancamento = value.HasValue ?
                DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) :
                null;
        }
        public string? Imagem { get; set; }
        public string? Observacoes { get; set; }
        public bool Lida { get; set; } = false;
        [Range(0.0, 10.0)]
        public decimal? Nota { get; set; }
    }

    public class UpdateEdicaoDTO
    {
        [Required]
        public int Numero { get; set; }
        public string? Titulo { get; set; }

        private DateTime? _dataLancamento;

        [JsonPropertyName("dataLancamento")]
        public DateTime? DataLancamento
        {
            get => _dataLancamento;
            set => _dataLancamento = value.HasValue ?
                DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) :
                null;
        }
        public string? Imagem { get; set; }
        public string? Observacoes { get; set; }
        public bool Lida { get; set; }
        [Range(0.0, 10.0)]
        public decimal? Nota { get; set; }
    }
}