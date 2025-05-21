using FluentValidation;
using ComicVerse.Application.DTOs;

namespace ComicVerse.Application.DTOs.Validators
{
    public class CreateHQDtoValidator : AbstractValidator<CreateHQDTO>
    {
        public CreateHQDtoValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("Título é obrigatório")
                .MaximumLength(150).WithMessage("Título não pode exceder 150 caracteres");

            RuleFor(x => x.Descricao)
                .MaximumLength(1000).WithMessage("Descrição não pode exceder 1000 caracteres");
        }
    }
}