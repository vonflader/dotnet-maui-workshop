using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

[QueryProperty("Monkey", "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
	private readonly IMonkeyNavService _navService;
	private readonly IMessageService _messageService;
	private readonly IMap _map;
	[ObservableProperty]
	Monkey _monkey;

	public MonkeyDetailsViewModel(IMonkeyNavService navService,
		IMessageService messageService,
		IMap map)
	{
		_navService = navService;
		_messageService = messageService;
		_map = map;
	}

	[RelayCommand]
	async Task GoBack() =>
		await _navService.GoBack();

	[RelayCommand]
	async Task OpenMap()
	{
		try
		{
			await _map.OpenAsync(Monkey.Latitude, Monkey.Longitude,
				new MapLaunchOptions
				{
					Name = Monkey.Name,					
					NavigationMode = NavigationMode.None,					
				});
		}
		catch (Exception ex)
		{
            Debug.WriteLine(ex);
            await _messageService.DisplayDefaultAlert("Error!",
                    $"Unable to open map: {ex.Message}");
        }
	}
}
