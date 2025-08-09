using JPT.Infrastructure;
using JPT.UseCases;
using JPT.Web;
using JPT.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGenWithAuth();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddWeb();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

app.UseHttpsRedirection();

app.Run();

