using JPT.Web.Infrastructure;

namespace JPT.Web;

public static class DependencyInjection
{
    public static void AddWeb(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        
    }
}