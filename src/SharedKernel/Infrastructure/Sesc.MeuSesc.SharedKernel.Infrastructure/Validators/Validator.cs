using FluentValidation;
using FluentValidation.Results;
using Sesc.MeuSesc.SharedKernel.Infrastructure.Entities;
using System;

namespace Sesc.MeuSesc.SharedKernel.Infrastructure.Validators
{
    public abstract class Validator<T> : AbstractValidator<T> where T : EntityBase
    {
        abstract public void SetRules();
        public Validator()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            SetRules();
        }

        public bool IsValid(T validatable)
        {
            if (null == validatable)
            {
                throw new ArgumentNullException("Objeto para ser validado não pode ser nulo.");
            }

            return Validate(validatable).IsValid;
        }

        public ValidationResult DoValidate(T validatable)
        {
            if (null == validatable)
            {
                throw new ArgumentNullException("Objeto para ser validado não pode ser nulo.");
            }

            var result = Validate(validatable);

            return result;
        }
    }
}
