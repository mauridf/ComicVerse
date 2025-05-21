using FluentValidation;

namespace ComicVerse.Application.DTOs.Validators
{
    public class UpdatePersonagemDtoValidator : AbstractValidator<UpdatePersonagemDTO>
    {
        public UpdatePersonagemDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(100).WithMessage("Nome não pode exceder 100 caracteres");

            RuleFor(x => x.Descricao)
                .MaximumLength(500).WithMessage("Descrição não pode exceder 500 caracteres");
        }
    }
}