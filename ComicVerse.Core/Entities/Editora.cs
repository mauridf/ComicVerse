namespace ComicVerse.Core.Entities
{
    public class Editora : BaseEntity
    {
        public string Nome { get; private set; }
        public string? Imagem { get; private set; }
        public string? Descricao { get; private set; }

        // Relacionamentos
        public ICollection<HQEditora> HQEditoras { get; private set; } = new List<HQEditora>();

        protected Editora() { }

        public Editora(string nome, string? imagem = null, string? descricao = null)
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