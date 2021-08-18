using Hydra.AuthorizationCodeFlow.HttpClients;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<AuthorizationServerHttpClient>(configuration =>
{
    configuration.BaseAddress = new Uri("https://localhost:5001");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();


app.MapDefaultControllerRoute();
app.Run();
