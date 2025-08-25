using System.Reflection;
using JPT.Infrastructure;
using JPT.UseCases;
using JPT.Web;
using JPT.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGenWithAuth();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddWeb(builder.Configuration);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseUploadsStaticFiles();

app.MapEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

// Use global custom exception handler
app.UseExceptionHandler();

app.UseCors();            
app.UseAuthentication();  
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();

