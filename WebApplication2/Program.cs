using AnimeWebAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services registration here

/* 
 * Register services with scoped lifetime
 * Scoped instances are created once per request (per client request in case of a web app)
 * This means that for each incoming request, a new instance of the service will be created,
 * allowing us to keep the data separate and isolated for each request.
 * It's a good choice for this app, as we're managing in-memory data and want to keep data
 * separate for each request without sharing it among multiple requests.
 */
builder.Services.AddScoped<IAnimeService, AnimeService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();

// Add controllers services
builder.Services.AddControllers();

// Add Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AnimeWebAPI", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Enable Swagger and Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnimeWebAPI v1");
});

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
