namespace ComicVerse.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CriadaEm { get; protected set; } = DateTime.UtcNow;
        public DateTime? AlteradaEm { get; protected set; }

        // Método interno para atualização do timestamp
        internal void UpdateTimestampInternal()
        {
            AlteradaEm = DateTime.UtcNow;
        }

        // Mantém o método protegido para uso dentro da hierarquia
        protected void UpdateTimestamp()
        {
            UpdateTimestampInternal();
        }
    }
}
