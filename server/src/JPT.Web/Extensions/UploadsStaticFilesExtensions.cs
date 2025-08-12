using Microsoft.Extensions.FileProviders;

namespace JPT.Web.Extensions;

internal static class UploadsStaticFilesExtensions
{
    public static void UseUploadsStaticFiles(this WebApplication app)
    {
        var uploadsAbs = Path.Combine(app.Environment.ContentRootPath, "Uploads");
        Directory.CreateDirectory(uploadsAbs);
        
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(uploadsAbs),
            RequestPath  = "/Uploads"
        });
    }
}
