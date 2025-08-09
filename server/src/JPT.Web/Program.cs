using JPT.Infrastructure;
using JPT.Web;
using JPT.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGenWithAuth();

builder.Services
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

