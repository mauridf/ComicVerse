namespace ComicVerse.Core.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; private set; } = "User";

        protected Usuario() { }

        public Usuario(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public void SetPasswordHash(byte[] hash, byte[] salt)
        {
            PasswordHash = hash;
            PasswordSalt = salt;
        }

        public void SetRole(string role)
        {
            Role = role;
        }
    }
}