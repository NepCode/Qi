using BusinessLogic.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class EmpleadoValidatorUpdate : AbstractValidator<Empleado>
    {
        public EmpleadoValidatorUpdate()

        {
            Include(new EstadoCivilSpecified());
            Include(new DireccionSpecified());
            Include(new EmailSpecified());
            Include(new TelefonoSpecified());
            Include(new PuestoSpecified());
            Include(new FechaBajaSpecified());
        }

        public class FechaBajaSpecified : AbstractValidator<Empleado>
        {
            public FechaBajaSpecified()
            {
                RuleFor(user => user.FechaBaja)
                   .Cascade(CascadeMode.Stop)
                   .Must(date => date != default(DateTime))
                   .WithMessage("{PropertyName} ingrese una fecha valida");
            }
        }   

        
       
        public class EstadoCivilSpecified : AbstractValidator<Empleado>
        {
            public EstadoCivilSpecified()
            {
                RuleFor(user => user.EstadoCivil)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("No ha indicado el estado civil del empleado.")
                   .Length(2, 20)
                   .WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");
            }
        }


        public class DireccionSpecified : AbstractValidator<Empleado>
        {
            public DireccionSpecified()
            {
                RuleFor(user => user.Direccion)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("No ha indicado la direccion del empleado.")
                   .Length(2, 50)
                   .WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");
            }
        }


        public class EmailSpecified : AbstractValidator<Empleado>
        {
            public EmailSpecified()
            {
                RuleFor(user => user.Email)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("No ha indicado el email del empleado.")
                   .EmailAddress()
                   .WithMessage("Ingrese una direccion valida.")
                   .Length(2, 50)
                   .WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");
            }
        }


        public class TelefonoSpecified : AbstractValidator<Empleado>
        {
            public TelefonoSpecified()
            {
                RuleFor(user => user.Telefono)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("No ha indicado el rfc del empleado.")
                   .Length(2, 50)
                   .WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");
            }
        }


        public class PuestoSpecified : AbstractValidator<Empleado>
        {
            public PuestoSpecified()
            {
                RuleFor(user => user.Puesto)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("No ha indicado el puesto del empleado");
            }
        }
        


    }
}
