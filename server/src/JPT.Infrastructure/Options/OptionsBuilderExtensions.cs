using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JPT.Infrastructure.Options;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOptionsWithFluentValidation<TOptions>(
        this IServiceCollection services,
        string configurationSection) where TOptions : class
    {
        var builder = services.AddOptions<TOptions>()
            .BindConfiguration(configurationSection)
            .ValidateOnStart();

        services.AddSingleton<IValidateOptions<TOptions>>(sp =>
            new FluentValidationOptions<TOptions>(sp, builder.Name));

        return services;
    }
}