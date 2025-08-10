using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JPT.Infrastructure.Options;

public sealed class FluentValidationOptions<TOptions>(IServiceProvider serviceProvider, string? name) : IValidateOptions<TOptions> 
    where TOptions : class
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly string? _name = name;
    
    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (_name is not null && _name != name)
        {
            return ValidateOptionsResult.Skip;
        }

        ArgumentNullException.ThrowIfNull(options);

        using var scope = _serviceProvider.CreateScope();

        var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();
        
        var result = validator.Validate(options);
        if (result.IsValid)
        {
            return ValidateOptionsResult.Success;
        }

        var type = options.GetType().Name;
 
        var errors = result.Errors.Select(e => $"Validation failed for {type}.{e.PropertyName} with the error {e.ErrorMessage}");
        
        return ValidateOptionsResult.Fail(errors);
    }
}