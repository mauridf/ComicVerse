namespace ComicVerse.Core.Entities
{
    public class HQEditora
    {
        public Guid HQId { get; private set; }
        public Guid EditoraId { get; private set; }

        // Relacionamentos
        public HQ HQ { get; private set; }
        public Editora Editora { get; private set; }

        protected HQEditora() { }

        public HQEditora(Guid hqId, Guid editoraId)
        {
            HQId = hqId;
            EditoraId = editoraId;
        }
    }
}