namespace ComicVerse.Core.Entities
{
    public class Edicao : BaseEntity
    {
        public Guid HQId { get; set; }
        public int Numero { get; private set; }
        public string? Titulo { get; private set; }
        public DateTime? DataLancamento { get; set; }
        public string? Imagem { get; private set; }
        public string? Observacoes { get; private set; }
        public bool Lida { get; internal set; }
        public decimal? Nota { get; private set; }

        // Relacionamentos
        public HQ HQ { get; private set; }

        protected Edicao() { }

        public Edicao(
            Guid hqId,
            int numero,
            string? titulo = null,
            DateTime? dataLancamento = null,
            string? imagem = null,
            string? observacoes = null,
            bool lida = false,
            decimal? nota = null)
        {
            HQId = hqId;
            Numero = numero;
            Titulo = titulo;
            DataLancamento = dataLancamento;
            Imagem = imagem;
            Observacoes = observacoes;
            Lida = lida;
            Nota = nota;
        }

        public void Update(
            int numero,
            string? titulo = null,
            DateTime? dataLancamento = null,
            string? imagem = null,
            string? observacoes = null,
            bool lida = false,
            decimal? nota = null)
        {
            Numero = numero;
            Titulo = titulo;
            DataLancamento = dataLancamento;
            Imagem = imagem;
            Observacoes = observacoes;
            Lida = lida;
            Nota = nota;
            UpdateTimestamp();
        }

        // Método público para marcar como lida
        public void MarcarComoLida(bool lida)
        {
            Lida = lida;
            UpdateTimestamp();
        }

        // Método interno para atualização pelo repositório
        public void SetLidaInternal(bool lida)
        {
            Lida = lida;
            UpdateTimestamp();
        }
    }
}