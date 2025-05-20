using AutoMapper;
using ComicVerse.Application.Mappings;
using ComicVerse.Application.Services;
using ComicVerse.Core.Interfaces.Repositories;
using ComicVerse.Infrastructure.Data;
using ComicVerse.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ComicVerseDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("ComicVerse.Infrastructure")));

// Registro de repositórios
builder.Services.AddScoped<IEditoraRepository, EditoraRepository>();
builder.Services.AddScoped<IPersonagemRepository, PersonagemRepository>();
builder.Services.AddScoped<IEquipeRepository, EquipeRepository>();
builder.Services.AddScoped<IHQRepository, HQRepository>();
builder.Services.AddScoped<IEdicaoRepository, EdicaoRepository>();

// Registro de serviços
builder.Services.AddScoped<IEditoraService, EditoraService>();
builder.Services.AddScoped<IPersonagemService, PersonagemService>();
builder.Services.AddScoped<IEquipeService, EquipeService>();
builder.Services.AddScoped<IHQService, HQService>();
builder.Services.AddScoped<IEdicaoService, EdicaoService>();
// Registrar outros serviços...

// AutoMapper
builder.Services.AddAutoMapper(typeof(EditoraProfile), typeof(PersonagemProfile),
    typeof(EquipeProfile), typeof(HQProfile), typeof(EdicaoProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();