namespace ComicVerse.Core.Entities
{
    public class HQ : BaseEntity
    {
        public string Titulo { get; private set; }
        public string? Imagem { get; private set; }
        public string? Descricao { get; private set; }

        // Relacionamentos
        public ICollection<Edicao> Edicoes { get; private set; } = new List<Edicao>();
        public ICollection<HQEditora> Editoras { get; private set; } = new List<HQEditora>();
        public ICollection<HQPersonagem> Personagens { get; private set; } = new List<HQPersonagem>();
        public ICollection<HQEquipe> Equipes { get; private set; } = new List<HQEquipe>();

        protected HQ() { }

        public HQ(string titulo, string? imagem = null, string? descricao = null)
        {
            Titulo = titulo;
            Imagem = imagem;
            Descricao = descricao;
        }

        public void Update(string titulo, string? imagem = null, string? descricao = null)
        {
            Titulo = titulo;
            Imagem = imagem;
            Descricao = descricao;
            UpdateTimestamp();
        }
    }
}