using EduCore.Business.DTOs;
using FluentValidation;

namespace EduCore.Business.Validators
{
    public class CrearCursoDtoValidator : AbstractValidator<CrearCursoDto>
    {
        public CrearCursoDtoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del curso es obligatorio")
                .MaximumLength(100).WithMessage("El nombre no puede tener más de 100 caracteres");

            RuleFor(x => x.Descripcion)
                .MaximumLength(500).WithMessage("La descripción no puede tener más de 500 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Descripcion));
        }
    }
}
