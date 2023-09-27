using System;
using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Results;

namespace WeatherSampleOne.Domain
{
    public abstract class Validated<TModel> : IValidated
        where TModel : class
    {
        private bool hasRules;
        private AbstractValidator<TModel> _validator;
        private AbstractValidator<TModel> Validator => _validator ??
            (_validator = new InlineValidator<TModel>()
            {
                CascadeMode = CascadeMode.StopOnFirstFailure
            });

        protected void SetValidator(AbstractValidator<TModel> validator) => _validator = validator;
        protected abstract void SetRules();
        public ValidationResult Validate()
        {
            if (!hasRules)
            {
                SetRules();
                hasRules = true;
            }
            return Validator.Validate(this as TModel);
        }
        protected IRuleBuilderInitial<TModel, TProperty> RuleFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return Validator.RuleFor(expression);
        }
    }

    public interface IValidated
    {
        ValidationResult Validate();
    }
}

