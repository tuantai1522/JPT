using JPT.Web.Extensions;
using JPT.Web.Infrastructure;

namespace JPT.Web;

public static class DependencyInjection
{
    public static void AddWeb(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddCorsPolicy();
        
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        
    }
}