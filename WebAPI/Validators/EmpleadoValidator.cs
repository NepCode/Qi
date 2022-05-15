using BusinessLogic.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class EmpleadoValidator : AbstractValidator<Empleado>
    {
        public EmpleadoValidator()
        {
            Include(new FechaAltaSpecified());
            Include(new NombreIsSpecified());
            Include(new ApellidoPaternoIsSpecified());
            Include(new ApellidoMaternoSpecified());
            Include(new RfcSpecified());
            Include(new FechaNacimientoSpecified());
            Include(new UsuarioAgregaSpecified());
        }


        public class FechaAltaSpecified : AbstractValidator<Empleado>
        {
            public FechaAltaSpecified()
            {
                RuleFor(user => user.FechaAlta)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("No ha indicado la fecha de registro.")
                   .Must(date => date != default(DateTime))
                   .WithMessage("{PropertyName} ingrese una fecha valida");
            }
        }   

        
       
        public class NombreIsSpecified : AbstractValidator<Empleado>
        {
            public NombreIsSpecified()
            {
                RuleFor(user => user.Nombre)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("No ha indicado el nombre de usuario.")
                   .Length(2, 50)
                   .WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");
            }
        }


        public class ApellidoPaternoIsSpecified : AbstractValidator<Empleado>
        {
            public ApellidoPaternoIsSpecified()
            {
                RuleFor(user => user.ApellidoPaterno)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("No ha indicado el apellido del usuario.")
                   .Length(2, 50)
                   .WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");
            }
        }


        public class ApellidoMaternoSpecified : AbstractValidator<Empleado>
        {
            public ApellidoMaternoSpecified()
            {
                RuleFor(user => user.ApellidoMaterno)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("No ha indicado el nombre de usuario.")
                   .Length(2, 50)
                   .WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");
            }
        }


        public class RfcSpecified : AbstractValidator<Empleado>
        {
            public RfcSpecified()
            {
                RuleFor(user => user.Rfc)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("No ha indicado el nombre de usuario.")
                   .Length(2, 50)
                   .WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");
            }
        }


        public class FechaNacimientoSpecified : AbstractValidator<Empleado>
        {
            public FechaNacimientoSpecified()
            {
                RuleFor(user => user.FechaNacimiento)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("No ha indicado la fecha de nacimiento.")
                   .Must(date => date != default(DateTime))
                   .WithMessage("{PropertyName} ingrese una fecha valida");
            }
        }
        

        public class UsuarioAgregaSpecified : AbstractValidator<Empleado>
        {
            public UsuarioAgregaSpecified()
            {
                RuleFor(user => user.Edad)
                   .Cascade(CascadeMode.Stop)
                   .NotNull()
                   .WithMessage("No ha indicado un entero")
                   .GreaterThanOrEqualTo(0)
                   .WithMessage("{PropertyName} ingrese una entero valido");
            }
        }

    }
}
