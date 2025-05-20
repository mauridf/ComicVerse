using AutoMapper;
using ComicVerse.Application.Mappings;
using ComicVerse.Application.Services;
using ComicVerse.Core.Entities;
using ComicVerse.Core.Interfaces.Repositories;
using FluentAssertions;
using Moq;

namespace ComicVerse.Tests.Services
{
    public class EditoraServiceTests
    {
        private readonly Mock<IEditoraRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly EditoraService _service;

        public EditoraServiceTests()
        {
            _mockRepo = new Mock<IEditoraRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EditoraProfile());
            });
            _mapper = config.CreateMapper();

            _service = new EditoraService(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllEditoras()
        {
            // Arrange
            var editoras = new List<Editora>
            {
                new Editora("Marvel"),
                new Editora("DC Comics")
            };

            _mockRepo.Setup(r => r.GetAll()).ReturnsAsync(editoras);

            // Act
            var result = await _service.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().Nome.Should().Be("Marvel");
        }

        [Fact]
        public async Task GetById_ShouldReturnEditora_WhenExists()
        {
            // Arrange
            var editoraId = Guid.NewGuid();
            var editora = new Editora("Marvel") { Id = editoraId };

            _mockRepo.Setup(r => r.GetById(editoraId)).ReturnsAsync(editora);

            // Act
            var result = await _service.GetById(editoraId);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(editoraId);
            result.Nome.Should().Be("Marvel");
        }

        // Implementar outros testes...
    }
}