
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace CulturalNameGenerator;

public static class NameGeneratorHelper
{
    private static readonly Dictionary<string, List<string>>? ListFirstNames = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(File.ReadAllText("wwwroot/data/firstNames.json"));
    private static readonly Dictionary<string, List<string>>? ListLastNames = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(File.ReadAllText("wwwroot/data/lastNames.json"));
    private static readonly Dictionary<string, string>? ListCultures = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("wwwroot/data/cultures.json"));

    public static List<CultureGroup> GenerateNames(string cultures, int numberOfNames)
    {
        List<CultureGroup> results = new List<CultureGroup>();
        List<string> cultureList = cultures.Split(',').Select(c => c.Trim().ToLower()).ToList();
        foreach (string culture in cultureList)
        {
            int count = 0;
            if (!ListCultures!.ContainsKey(culture.ToLower()))
            {
                throw new Exception("Culture not found");
            }

            if (ListFirstNames!.ContainsKey(culture.ToLower()) && ListLastNames!.ContainsKey(culture.ToLower()))
            {
                List<FullName> names = new List<FullName>();
                while (count < numberOfNames)
                {
                    var random = new Random();
                    var firstNameList = ListFirstNames[culture.ToLower()];
                    var lastNameList = ListLastNames[culture.ToLower()];
                    var generatedFirstName = firstNameList[random.Next(firstNameList.Count)];
                    var generatedLastName = lastNameList[random.Next(lastNameList.Count)];

                    names.Add(new FullName
                    {
                        FirstName = generatedFirstName,
                        LastName = generatedLastName
                    });
                    count++;
                }
                ;

                results.Add(new CultureGroup
                {
                    Culture = culture,
                    Names = names
                });
            }
            else
            {
                throw new Exception("Culture not found");
            }
        }

        return results;
    }
}