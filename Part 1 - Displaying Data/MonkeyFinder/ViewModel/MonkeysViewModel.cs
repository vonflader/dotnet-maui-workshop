using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
	private readonly MonkeyService _monkeyService;
	private readonly IMonkeyNavService _navService;

	public ObservableCollection<Monkey> Monkeys { get;} = new();

	public MonkeysViewModel(MonkeyService monkeyService,
		IMonkeyNavService navService)
	{
		Title = "Monkey Finder";
		_monkeyService = monkeyService;
		_navService = navService;
	}

	[RelayCommand]
	async Task GoToDetails(Monkey monkey)
	{
		if (monkey is null) 
			return;		

		await _navService.GoToMonkeyDetails(monkey);

		//await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true,
		//	new Dictionary<string, object>
		//	{
		//		{"Monkey", monkey }
		//	});
	}

	[RelayCommand]
	async Task GetMonkeys()
	{
		if (IsBusy) 
			return;

		try
		{
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
			await Shell.Current.DisplayAlert("Error!",
				$"Unable to get monkeys: {ex.Message}", "OK");
		}
		finally
		{
			IsBusy = false;
		}
	}
}
