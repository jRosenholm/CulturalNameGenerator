using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Load JSON data
var firstNames = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(File.ReadAllText("Data/firstNames.json"));
var lastNames = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(File.ReadAllText("Data/lastNames.json"));
var cultures = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("Data/cultures.json"));

// Endpoint to generate a name based on culture
app.MapGet("/generate-name/{culture}/", (string culture) =>
{
    if (cultures!.ContainsKey(culture.ToLower()))
    {
        return Results.NotFound();
    }

    if (firstNames!.ContainsKey(culture.ToLower()) && lastNames!.ContainsKey(culture.ToLower()))
    {
        var random = new Random();
        var firstNameList = firstNames[culture.ToLower()];
        var lastNameList = lastNames[culture.ToLower()];
        var generatedFirstName = firstNameList[random.Next(firstNameList.Count)];
        var generatedLastName = lastNameList[random.Next(lastNameList.Count)];
        return Results.Ok(new { FirstName = generatedFirstName, LastName = generatedLastName });
    }
    else
    {
        return Results.NotFound(new { Message = "Culture not found" });
    }
});


// Endpoint to generate a name based on culture and gender
app.MapGet("/generate-name/{culture}/{gender}", (string culture, string gender) =>
{
    if (cultures!.ContainsKey(culture.ToLower()))
    {
        return Results.NotFound();\
        
        
    }

    if (firstNames!.ContainsKey(culture.ToLower()) && lastNames!.ContainsKey(culture.ToLower()))
        {
            var random = new Random();
            var firstNameList = firstNames[culture.ToLower()];
            var lastNameList = lastNames[culture.ToLower()];
            var generatedFirstName = firstNameList[random.Next(firstNameList.Count)];
            var generatedLastName = lastNameList[random.Next(lastNameList.Count)];
            return Results.Ok(new { FirstName = generatedFirstName, LastName = generatedLastName });
        }
        else
        {
            return Results.NotFound(new { Message = "Culture not found" });
        }
});

app.Run();