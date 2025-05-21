using AutoMapper;
using ComicVerse.Application.DTOs.Validators;
using ComicVerse.Application.Mappings;
using ComicVerse.Application.Services;
using ComicVerse.Core.Interfaces.Repositories;
using ComicVerse.Infrastructure.Data;
using ComicVerse.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ComicVerse.API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Registro de serviços
builder.Services.AddScoped<IEditoraService, EditoraService>();
builder.Services.AddScoped<IPersonagemService, PersonagemService>();
builder.Services.AddScoped<IEquipeService, EquipeService>();
builder.Services.AddScoped<IHQService, HQService>();
builder.Services.AddScoped<IEdicaoService, EdicaoService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();

// AutoMapper
builder.Services.AddAutoMapper(typeof(EditoraProfile), typeof(PersonagemProfile),
    typeof(EquipeProfile), typeof(HQProfile), typeof(EdicaoProfile));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateEditoraDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateEdicaoDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateEquipeDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateHQDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePersonagemDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateEditoraDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateEdicaoDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateEquipeDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateHQDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdatePersonagemDtoValidator>();

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
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();