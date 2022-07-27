namespace MonkeyFinder.Services;

public class MonkeyNavService : IMonkeyNavService
{
    public async Task GoToMonkeyDetails(Monkey monkey) => 
        await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true,
            new Dictionary<string, object>
            {
                {"Monkey", monkey }
            });
    
}
