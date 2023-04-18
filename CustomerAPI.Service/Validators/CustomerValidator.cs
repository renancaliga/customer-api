using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAPI.Domain.Entities;

namespace CustomerAPI.Service.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Por favor, informe o nome.")
                .NotNull().WithMessage("Por favor, informe o nome.");

            var conditions = new List<string>() { "GRANDE", "MEDIA", "PEQUENA" };
            RuleFor(c => c.Size)
                .NotEmpty().WithMessage("Por favor, informe o nome.")
                .NotNull().WithMessage("Por favor, informe o nome.")
                .Must(c => conditions.Contains(c))
                .WithMessage("Por favor, apenas use: " + String.Join(", ", conditions));
        }
    }
}
