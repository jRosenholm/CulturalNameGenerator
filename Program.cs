using System.Text.Json;
using CulturalNameGenerator;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Serve static files from wwwroot
app.UseStaticFiles();

// Endpoint to generate a name based on culture and gender
app.MapGet("/generate-names", (string cultures, int numberOfNames = 1) =>
{
    List<CultureGroup> results = NameGeneratorHelper.GenerateNames(cultures, numberOfNames);
    return Results.Ok(results);
});

app.Run();