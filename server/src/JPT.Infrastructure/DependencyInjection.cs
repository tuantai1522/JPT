using System.Text;
using FluentValidation;
using JPT.Core.Features.Countries;
using JPT.Core.Features.Files;
using JPT.Core.Features.Users;
using JPT.Infrastructure.Authentication;
using JPT.Infrastructure.Database;
using JPT.Infrastructure.Files;
using JPT.Infrastructure.Options;
using JPT.Infrastructure.Options.Files;
using JPT.Infrastructure.Repositories;
using JPT.UseCases.Abstractions.Authentication;
using JPT.UseCases.Abstractions.Files;
using JPT.UseCases.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using FileOptions = JPT.Infrastructure.Options.Files.FileOptions;

namespace JPT.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddDatabase(configuration)
            .AddRepositories()
            .AddAuthenticationInternal(configuration)
            .AddAuthorizationInternal()
            .AddOptionSettings()
            .AddFileExplorer();
    
    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContextPool<ApplicationDbContext>(
            (sp, options) => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                .UseSnakeCaseNamingConvention());

        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<ICountryRepository, CountryRepository>()
            .AddScoped<IUserRepository, UserRepository>();

        return services;
    }
    
    private static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.RequireHttpsMetadata = false;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddHttpContextAccessor();
        
        services.AddScoped<IUserProvider, UserProvider>();
        
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();

        return services;
    }

    private static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();

        return services;
    }

    private static IServiceCollection AddOptionSettings(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddSingleton<IFileOptions, FileOptionsProvider>();
        services.AddOptionsWithFluentValidation<FileOptions>(nameof(FileOptions));
        
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        return services;
    }
    
    private static IServiceCollection AddFileExplorer(this IServiceCollection services)
    {
        services.AddScoped<IFileUrlResolver, FileUrlResolver>();
        services.AddScoped<IFileExplorerContext, LocalhostExplorerContext>();

        return services;
    }
}