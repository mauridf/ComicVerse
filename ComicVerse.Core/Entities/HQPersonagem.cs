namespace ComicVerse.Core.Entities
{
    public class HQPersonagem
    {
        public Guid HQId { get; private set; }
        public Guid PersonagemId { get; private set; }

        // Relacionamentos
        public HQ HQ { get; private set; }
        public Personagem Personagem { get; private set; }

        protected HQPersonagem() { }

        public HQPersonagem(Guid hqId, Guid personagemId)
        {
            HQId = hqId;
            PersonagemId = personagemId;
        }
    }
}