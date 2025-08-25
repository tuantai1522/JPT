namespace JPT.Web.Extensions;

public static class CorsExtension
{
    public static void AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy( policy =>
            {
                policy
                    .WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>() ?? [])
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Content-Disposition")
                    .AllowCredentials();
            });
        });
    }
}