using ComicVerse.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComicVerse.Infrastructure.Data
{
    public class ComicVerseDbContext : DbContext
    {
        public ComicVerseDbContext(DbContextOptions<ComicVerseDbContext> options) : base(options)
        {
        }

        public DbSet<Editora> Editoras { get; set; }
        public DbSet<Personagem> Personagens { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<HQ> HQs { get; set; }
        public DbSet<Edicao> Edicoes { get; set; }
        public DbSet<PersonagemEquipe> PersonagemEquipes { get; set; }
        public DbSet<HQEditora> HQEditoras { get; set; }
        public DbSet<HQPersonagem> HQPersonagens { get; set; }
        public DbSet<HQEquipe> HQEquipes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações das entidades principais
            modelBuilder.Entity<Editora>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Imagem).HasMaxLength(255);
                entity.Property(e => e.Descricao).HasMaxLength(500);
            });

            modelBuilder.Entity<Personagem>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nome).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Imagem).HasMaxLength(255);
                entity.Property(p => p.Descricao).HasMaxLength(500);
            });

            modelBuilder.Entity<Equipe>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Imagem).HasMaxLength(255);
                entity.Property(e => e.Descricao).HasMaxLength(500);
            });

            modelBuilder.Entity<HQ>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.Titulo).IsRequired().HasMaxLength(150);
                entity.Property(h => h.Imagem).HasMaxLength(255);
                entity.Property(h => h.Descricao).HasMaxLength(1000);
            });

            modelBuilder.Entity<Edicao>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).HasMaxLength(150);
                entity.Property(e => e.Imagem).HasMaxLength(255);
                entity.Property(e => e.Observacoes).HasMaxLength(2000);
                entity.Property(e => e.Nota).HasColumnType("decimal(3,1)");

                entity.HasOne(e => e.HQ)
                    .WithMany(h => h.Edicoes)
                    .HasForeignKey(e => e.HQId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configurações das tabelas de relacionamento N:N
            modelBuilder.Entity<PersonagemEquipe>(entity =>
            {
                entity.HasKey(pe => new { pe.PersonagemId, pe.EquipeId });

                entity.HasOne(pe => pe.Personagem)
                    .WithMany(p => p.Equipes)
                    .HasForeignKey(pe => pe.PersonagemId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pe => pe.Equipe)
                    .WithMany(e => e.Personagens)
                    .HasForeignKey(pe => pe.EquipeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<HQEditora>(entity =>
            {
                entity.HasKey(he => new { he.HQId, he.EditoraId });

                entity.HasOne(he => he.HQ)
                    .WithMany(h => h.Editoras)
                    .HasForeignKey(he => he.HQId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(he => he.Editora)
                    .WithMany(e => e.HQEditoras)
                    .HasForeignKey(he => he.EditoraId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<HQPersonagem>(entity =>
            {
                entity.HasKey(hp => new { hp.HQId, hp.PersonagemId });

                entity.HasOne(hp => hp.HQ)
                    .WithMany(h => h.Personagens)
                    .HasForeignKey(hp => hp.HQId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(hp => hp.Personagem)
                    .WithMany(p => p.HQs)
                    .HasForeignKey(hp => hp.PersonagemId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<HQEquipe>(entity =>
            {
                entity.HasKey(he => new { he.HQId, he.EquipeId });

                entity.HasOne(he => he.HQ)
                    .WithMany(h => h.Equipes)
                    .HasForeignKey(he => he.HQId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(he => he.Equipe)
                    .WithMany(e => e.HQs)
                    .HasForeignKey(he => he.EquipeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.PasswordHash)
                    .IsRequired();

                entity.Property(u => u.PasswordSalt)
                    .IsRequired();

                entity.Property(u => u.Role)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValue("User");

                // Índice único para o email
                entity.HasIndex(u => u.Email)
                    .IsUnique();

                // Validação de formato de email
                entity.Property(u => u.Email)
                    .HasAnnotation("RegularExpression", @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

                // Configuração do tipo de coluna para os campos binários (específico para PostgreSQL)
                entity.Property(u => u.PasswordHash)
                    .HasColumnType("bytea");

                entity.Property(u => u.PasswordSalt)
                    .HasColumnType("bytea");
            });

            // Configurações adicionais
            modelBuilder.Entity<Edicao>()
                .HasIndex(e => new { e.HQId, e.Numero })
                .IsUnique();
        }
    }
}