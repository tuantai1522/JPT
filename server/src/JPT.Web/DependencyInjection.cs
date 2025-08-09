namespace JPT.Web;

public static class DependencyInjection
{
    public static void AddWeb(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
    }
}