using System.Net.Http.Json;

namespace MonkeyFinder.Services;

public class MonkeyService
{
    List<Monkey> monkeys = new();
    private readonly HttpClient _client;

    public MonkeyService()
    {
        _client = new();
    }

    public async Task<List<Monkey>> GetMonkeys()
    {
        if (monkeys?.Count > 0)
            return monkeys;

        var url = "https://montemagno.com/monkeys.json";
        var response = await _client.GetAsync(url);        
        
        if (response.IsSuccessStatusCode)
        {
            Debug.WriteLine("Online mode used");
            return await response.Content.ReadFromJsonAsync<List<Monkey>>();
        }

        //Debug.WriteLine("Offline version used");
       
        //// offline version
        //using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
        //using var reader = new StreamReader(stream);
        //var contents = await reader.ReadToEndAsync();
        //monkeys = JsonSerializer.Deserialize<List<Monkey>>(contents);

        return monkeys;
    }
}
