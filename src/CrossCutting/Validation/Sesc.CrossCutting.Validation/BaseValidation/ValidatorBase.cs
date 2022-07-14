using FluentValidation;
using FluentValidation.Results;
using Sesc.CrossCutting.Validation.BaseException;

namespace Sesc.CrossCutting.Validation.BaseValidation
{
    public abstract class ValidatorBase<TEntity> : AbstractValidator<TEntity> where TEntity : class
    {
        abstract public void SetRules();
        public ValidatorBase()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            this.SetRules();
        }

        public void DoValidate(TEntity entity)
        {
            ValidationResult result = Validate(entity);

            if (!result.IsValid)
            {
                ContentSingleton.AddMessage(result);
                ContentSingleton.Dispatch();
            }
        }

        public bool IsValid(TEntity entity)
        {
            ValidationResult result = Validate(entity);

            return result.IsValid;
        }
    }
}
