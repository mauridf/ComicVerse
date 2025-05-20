namespace ComicVerse.Core.Entities
{
    public class Equipe : BaseEntity
    {
        public string Nome { get; private set; }
        public string? Imagem { get; private set; }
        public string? Descricao { get; private set; }

        // Relacionamentos
        public ICollection<PersonagemEquipe> Personagens { get; private set; } = new List<PersonagemEquipe>();
        public ICollection<HQEquipe> HQs { get; private set; } = new List<HQEquipe>();

        protected Equipe() { }

        public Equipe(string nome, string? imagem = null, string? descricao = null)
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