# 📚🦸‍♂️ ComicVerse API

Um sistema completo para gerenciamento de coleções de HQs usando .NET 9, PostgreSQL e princípios de Clean Architecture

## 📌 Visão Geral

O ComicVerse é uma API RESTful para gerenciamento de coleções de histórias em quadrinhos, com:

- Cadastro completo de HQs, editoras, personagens, equipes e edições
- Relacionamentos complexos (muitos-para-muitos)
- Armazenamento de imagens em sistema de arquivos
- Controle de leitura, notas e observações

## 🛠 Tecnologias e Arquitetura

### Stack Tecnológica

- .NET 9 (C# 12)
- PostgreSQL (Database)
- Entity Framework Core (ORM)
- AutoMapper (Mapeamento DTOs)
- Swagger/OpenAPI (Documentação)
- Clean Architecture + DDD + SOLID

### Estrutura do Projeto

```
ComicVerse/
├── ComicVerse.API/          # Camada de apresentação (Controllers)
├── ComicVerse.Core/         # Domínio (Entities, Interfaces)
├── ComicVerse.Infrastructure/ # Infraestrutura (Repositories, DbContext)
├── ComicVerse.Application/  # Lógica de aplicação (Services, DTOs)
└── ComicVerse.Tests/        # Testes unitários
```

## 🚀 Como Executar

### Pré-requisitos

- .NET 9 SDK
- PostgreSQL 15+
- Docker (opcional para containerização)

### Configuração Inicial

Clone o repositório:

```bash
git clone https://github.com/seu-usuario/ComicVerse.git
```

Configure o banco de dados:

Edite `appsettings.json` com suas credenciais PostgreSQL:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=comicverse;Username=postgres;Password=senha;"
}
```

Execute as migrations:

```bash
dotnet ef database update --startup-project ComicVerse.API
```

### Executando a API

```bash
cd ComicVerse.API
dotnet run
```

Acesse: https://localhost:5001/swagger

## 📚 Documentação da API

### Principais Endpoints

| Recurso              | Métodos HTTP        | Descrição                            |
|----------------------|---------------------|----------------------------------------|
| /api/editoras        | GET, POST, PUT, DELETE | CRUD de editoras (Marvel, DC etc.)     |
| /api/personagens     | GET, POST, PUT, DELETE | Heróis/vilões individuais              |
| /api/equipes         | GET, POST, PUT, DELETE | Grupos (Vingadores, X-Men etc.)        |
| /api/hqs             | GET, POST, PUT, DELETE | Títulos principais                     |
| /api/hqs/{id}/edicoes| GET, POST            | Edições específicas de uma HQ          |

### Exemplo de Request/Response

`GET /api/hqs/1`

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "titulo": "Homem-Aranha: A Última Caçada de Kraven",
  "imagem": "uploads/hqs/spider-man.jpg",
  "editoras": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa7",
      "nome": "Marvel"
    }
  ],
  "personagens": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa8",
      "nome": "Homem-Aranha"
    }
  ]
}
```

## 🧩 Diagrama de Entidades

```
erDiagram
    EDITORA ||--o{ HQ_EDITORA : "1-N"
    HQ ||--o{ HQ_EDITORA : "1-N"
    HQ ||--o{ EDICAO : "1-N"
    PERSONAGEM ||--o{ PERSONAGEM_EQUIPE : "1-N"
    EQUIPE ||--o{ PERSONAGEM_EQUIPE : "1-N"
    HQ ||--o{ HQ_PERSONAGEM : "1-N"
    PERSONAGEM ||--o{ HQ_PERSONAGEM : "1-N"
    HQ ||--o{ HQ_EQUIPE : "1-N"
    EQUIPE ||--o{ HQ_EQUIPE : "1-N"
```

## 🔍 Principais Funcionalidades

### Recursos Avançados

- Gestão Completa de Coleções
  - Marcar edições como lidas/não lidas
  - Adicionar notas (0-10) e observações
  - Filtros por editora, personagem ou equipe

- Relacionamentos Complexos
  - Uma HQ pode ter múltiplas editoras
  - Personagens podem pertencer a várias equipes
  - Edições específicas com metadados detalhados

- Armazenamento de Imagens
  - Upload de capas para sistema de arquivos
  - Gerenciamento de caminhos relativos no banco

## 🤝 Contribuição

1. Faça um fork do projeto
2. Crie uma branch (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

MIT License - Veja `LICENSE.md` para detalhes.

## ✉ Contato

Desenvolvido por Maurício Oliveira - [mauridf@gmail.com]

GitHub

<p align="center"> <img src="https://img.icons8.com/color/96/000000/comics.png" alt="ComicVerse Logo"/> <br> "Colecionar HQs é preservar história!" - Stan Lee </p>