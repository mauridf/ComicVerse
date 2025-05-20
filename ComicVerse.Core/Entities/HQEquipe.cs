namespace ComicVerse.Core.Entities
{
    public class HQEquipe
    {
        public Guid HQId { get; private set; }
        public Guid EquipeId { get; private set; }

        // Relacionamentos
        public HQ HQ { get; private set; }
        public Equipe Equipe { get; private set; }

        protected HQEquipe() { }

        public HQEquipe(Guid hqId, Guid equipeId)
        {
            HQId = hqId;
            EquipeId = equipeId;
        }
    }
}