using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using NeerCore.Exceptions;

namespace EventPlus.Application.Minis.Base.Mixins;

public abstract class AutoFluentValidationMixin
{
    protected ValidationResult AutoValidate(object model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var abstractValidatorType = typeof(AbstractValidator<>);
        var modelType = model.GetType();
        var abstractValidatorGeneric = abstractValidatorType.MakeGenericType(modelType);

        var validatorType = FindValidatorType(Assembly.GetExecutingAssembly(), abstractValidatorGeneric);

        if (validatorType is null)
            throw new NotFoundException($"No such validator for type {modelType.Name}");

        var validatorInstance = Activator.CreateInstance(validatorType) as IValidator;
        
        var validationContextType = typeof(ValidationContext<>);
        var validationContextTypeGeneric = validationContextType.MakeGenericType(modelType);
        var validationContextInstance = Activator.CreateInstance(validationContextTypeGeneric, model) as IValidationContext;

        return validatorInstance!.Validate(validationContextInstance);
    }
    
    private Type? FindValidatorType(Assembly assembly, Type validatorType)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        ArgumentNullException.ThrowIfNull(validatorType);

        return assembly.GetTypes().FirstOrDefault(t => t.IsSubclassOf(validatorType));
    }
}