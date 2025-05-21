namespace ComicVerse.Core.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; set; }
        public string Role { get; private set; } = "User";

        protected Usuario() { }

        public Usuario(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public void SetPasswordHash(string hash)
        {
            PasswordHash = hash;
        }

        public void SetRole(string role)
        {
            Role = role;
        }
    }
}