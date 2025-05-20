namespace ComicVerse.Core.Entities
{
    public class Personagem : BaseEntity
    {
        public string Nome { get; private set; }
        public string? Imagem { get; private set; }
        public string? Descricao { get; private set; }

        // Relacionamentos
        public ICollection<PersonagemEquipe> Equipes { get; private set; } = new List<PersonagemEquipe>();
        public ICollection<HQPersonagem> HQs { get; private set; } = new List<HQPersonagem>();

        protected Personagem() { }

        public Personagem(string nome, string? imagem = null, string? descricao = null)
        {
            Nome = nome;
            Imagem = imagem;
            Descricao = descricao;
        }

        public void Update(string nome, string? imagem = null, string? descricao = null)
        {
            Nome = nome;
            Imagem = imagem;
            Descricao = descricao;
            UpdateTimestamp();
        }
    }
}