using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
	private readonly MonkeyService _monkeyService;
	private readonly IMonkeyNavService _navService;
	private readonly IMessageService _messageService;
	private readonly IConnectivity _connectivity;
	private readonly IGeolocation _geolocation;

	public ObservableCollection<Monkey> Monkeys { get;} = new();

	[ObservableProperty]
	bool _isRefreshing;

	public MonkeysViewModel(MonkeyService monkeyService,
		IMonkeyNavService navService,
		IMessageService messageService,
		IConnectivity connectivity,
		IGeolocation geolocation)
	{
		Title = "Monkey Finder";
		_monkeyService = monkeyService;
		_navService = navService;
		_messageService = messageService;
		_connectivity = connectivity;
		_geolocation = geolocation;
	}

	[RelayCommand]
	async Task GetClosestMonkey()
	{
		if (IsBusy || Monkeys.Count == 0)
			return;

		try
		{
			IsBusy = true;

			var location = await _geolocation.GetLastKnownLocationAsync();

			if (location is null || location.Timestamp.AddSeconds(15) < DateTime.Now)
			{
				location = await _geolocation.GetLocationAsync(
					new GeolocationRequest
					{
						DesiredAccuracy = GeolocationAccuracy.Medium,
						Timeout = TimeSpan.FromSeconds(30),
					});
			}

			if (location is null)
				return;

			var closestMonkey = Monkeys.OrderBy(m =>
				location.CalculateDistance(m.Latitude, m.Longitude, DistanceUnits.Kilometers)
				).FirstOrDefault();

			if (closestMonkey is null)
				return;

			await _navService.GoToMonkeyDetails(closestMonkey);
			
		}
		catch (Exception ex)
		{
            Debug.WriteLine(ex);
            await _messageService.DisplayDefaultAlert("Error!",
                    $"Unable to get closest monkey: {ex.Message}");
		}
		finally
		{
			IsBusy = false;
		}
	}

	[RelayCommand]
	async Task GoToDetails(Monkey monkey)
	{
		if (monkey is null) 
			return;		

		await _navService.GoToMonkeyDetails(monkey);		
	}

	[RelayCommand]
	async Task GetMonkeys()
	{
		if (IsBusy) 
			return;

		try
		{
			if (_connectivity.NetworkAccess != NetworkAccess.Internet)
			{
				await _messageService.DisplayDefaultAlert("Internet issue",
					"Check your internet and try again!");
				return;
			}

			IsBusy = true;
			var monkeys = await _monkeyService.GetMonkeys();

			if (Monkeys.Count != 0)
				Monkeys.Clear();

			foreach(var monkey in monkeys)
				Monkeys.Add(monkey);
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex);
			await _messageService.DisplayDefaultAlert("Error!",
				$"Unable to get monkeys: {ex.Message}");
		}
		finally
		{
			IsBusy = false;
			IsRefreshing = false;
		}
	}
}
