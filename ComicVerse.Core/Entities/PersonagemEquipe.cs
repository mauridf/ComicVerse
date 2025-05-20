namespace ComicVerse.Core.Entities
{
    public class PersonagemEquipe
    {
        public Guid PersonagemId { get; private set; }
        public Guid EquipeId { get; private set; }

        // Relacionamentos
        public Personagem Personagem { get; private set; }
        public Equipe Equipe { get; private set; }

        protected PersonagemEquipe() { }

        public PersonagemEquipe(Guid personagemId, Guid equipeId)
        {
            PersonagemId = personagemId;
            EquipeId = equipeId;
        }
    }
}