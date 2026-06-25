using EduCore.Business.DTOs;
using FluentValidation;

namespace EduCore.Business.Validators
{
    public class CrearAlumnoDtoValidator : AbstractValidator<CrearAlumnoDto>
    {
        public CrearAlumnoDtoValidator()
        {
            RuleFor(x => x.Dni)
                .NotEmpty().WithMessage("El DNI es obligatorio")
                .Length(8).WithMessage("El DNI debe tener 8 dígitos")
                .Matches("^[0-9]+$").WithMessage("El DNI solo debe contener números");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(50).WithMessage("El nombre no puede tener más de 50 caracteres");

            RuleFor(x => x.ApellidoPaterno)
                .NotEmpty().WithMessage("El apellido paterno es obligatorio")
                .MaximumLength(50).WithMessage("El apellido paterno no puede tener más de 50 caracteres");

            RuleFor(x => x.FechaNacimiento)
                .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria")
                .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento no puede ser futura");

            RuleFor(x => x.CorreoPersonal)
                .EmailAddress().WithMessage("El correo personal no tiene un formato válido")
                .When(x => !string.IsNullOrEmpty(x.CorreoPersonal));
        }
    }
}
