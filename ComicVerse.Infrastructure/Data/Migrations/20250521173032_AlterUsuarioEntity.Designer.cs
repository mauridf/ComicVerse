﻿// <auto-generated />
using System;
using ComicVerse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ComicVerse.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ComicVerseDbContext))]
    [Migration("20250521173032_AlterUsuarioEntity")]
    partial class AlterUsuarioEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ComicVerse.Core.Entities.Edicao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AlteradaEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CriadaEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DataLancamento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("HQId")
                        .HasColumnType("uuid");

                    b.Property<string>("Imagem")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("Lida")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("Nota")
                        .HasColumnType("decimal(3,1)");

                    b.Property<int>("Numero")
                        .HasColumnType("integer");

                    b.Property<string>("Observacoes")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("Titulo")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("Id");

                    b.HasIndex("HQId", "Numero")
                        .IsUnique();

                    b.ToTable("Edicoes");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.Editora", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AlteradaEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CriadaEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Imagem")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Editoras");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.Equipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AlteradaEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CriadaEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Imagem")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Equipes");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.HQ", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AlteradaEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CriadaEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Imagem")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("Id");

                    b.ToTable("HQs");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.HQEditora", b =>
                {
                    b.Property<Guid>("HQId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EditoraId")
                        .HasColumnType("uuid");

                    b.HasKey("HQId", "EditoraId");

                    b.HasIndex("EditoraId");

                    b.ToTable("HQEditoras");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.HQEquipe", b =>
                {
                    b.Property<Guid>("HQId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EquipeId")
                        .HasColumnType("uuid");

                    b.HasKey("HQId", "EquipeId");

                    b.HasIndex("EquipeId");

                    b.ToTable("HQEquipes");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.HQPersonagem", b =>
                {
                    b.Property<Guid>("HQId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PersonagemId")
                        .HasColumnType("uuid");

                    b.HasKey("HQId", "PersonagemId");

                    b.HasIndex("PersonagemId");

                    b.ToTable("HQPersonagens");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.Personagem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AlteradaEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CriadaEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Imagem")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Personagens");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.PersonagemEquipe", b =>
                {
                    b.Property<Guid>("PersonagemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EquipeId")
                        .HasColumnType("uuid");

                    b.HasKey("PersonagemId", "EquipeId");

                    b.HasIndex("EquipeId");

                    b.ToTable("PersonagemEquipes");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AlteradaEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CriadaEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasAnnotation("RegularExpression", "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasDefaultValue("User");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.Edicao", b =>
                {
                    b.HasOne("ComicVerse.Core.Entities.HQ", "HQ")
                        .WithMany("Edicoes")
                        .HasForeignKey("HQId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HQ");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.HQEditora", b =>
                {
                    b.HasOne("ComicVerse.Core.Entities.Editora", "Editora")
                        .WithMany("HQEditoras")
                        .HasForeignKey("EditoraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComicVerse.Core.Entities.HQ", "HQ")
                        .WithMany("Editoras")
                        .HasForeignKey("HQId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Editora");

                    b.Navigation("HQ");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.HQEquipe", b =>
                {
                    b.HasOne("ComicVerse.Core.Entities.Equipe", "Equipe")
                        .WithMany("HQs")
                        .HasForeignKey("EquipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComicVerse.Core.Entities.HQ", "HQ")
                        .WithMany("Equipes")
                        .HasForeignKey("HQId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipe");

                    b.Navigation("HQ");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.HQPersonagem", b =>
                {
                    b.HasOne("ComicVerse.Core.Entities.HQ", "HQ")
                        .WithMany("Personagens")
                        .HasForeignKey("HQId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComicVerse.Core.Entities.Personagem", "Personagem")
                        .WithMany("HQs")
                        .HasForeignKey("PersonagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HQ");

                    b.Navigation("Personagem");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.PersonagemEquipe", b =>
                {
                    b.HasOne("ComicVerse.Core.Entities.Equipe", "Equipe")
                        .WithMany("Personagens")
                        .HasForeignKey("EquipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComicVerse.Core.Entities.Personagem", "Personagem")
                        .WithMany("Equipes")
                        .HasForeignKey("PersonagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipe");

                    b.Navigation("Personagem");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.Editora", b =>
                {
                    b.Navigation("HQEditoras");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.Equipe", b =>
                {
                    b.Navigation("HQs");

                    b.Navigation("Personagens");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.HQ", b =>
                {
                    b.Navigation("Edicoes");

                    b.Navigation("Editoras");

                    b.Navigation("Equipes");

                    b.Navigation("Personagens");
                });

            modelBuilder.Entity("ComicVerse.Core.Entities.Personagem", b =>
                {
                    b.Navigation("Equipes");

                    b.Navigation("HQs");
                });
#pragma warning restore 612, 618
        }
    }
}
