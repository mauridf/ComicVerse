using FluentValidation;

namespace ComicVerse.Application.DTOs.Validators
{
    public class UpdateEdicaoDtoValidator : AbstractValidator<UpdateEdicaoDTO>
    {
        public UpdateEdicaoDtoValidator()
        {
            RuleFor(x => x.Numero)
                .GreaterThan(0).WithMessage("Número deve ser maior que zero");

            RuleFor(x => x.Titulo)
                .MaximumLength(150).WithMessage("Título não pode exceder 150 caracteres");

            RuleFor(x => x.Nota)
                .InclusiveBetween(0, 10).WithMessage("Nota deve estar entre 0 e 10")
                .When(x => x.Nota.HasValue);
        }
    }
}