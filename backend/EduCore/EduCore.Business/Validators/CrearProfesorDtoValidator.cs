using EduCore.Business.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCore.Business.Validators
{
    public class CrearProfesorDtoValidator : AbstractValidator<CrearProfesorDto>
    {
        public CrearProfesorDtoValidator()
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

            RuleFor(x => x.Especialidad)
                .NotEmpty().WithMessage("La especialidad es obligatoria")
                .MaximumLength(100).WithMessage("La especialidad no puede tener más de 100 caracteres");

            RuleFor(x => x.CorreoElectronico)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio")
                .EmailAddress().WithMessage("El correo electrónico no tiene un formato válido");

            RuleFor(x => x.FechaNacimiento)
                .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento no puede ser futura")
                .When(x => x.FechaNacimiento.HasValue);
        }
    }
}
