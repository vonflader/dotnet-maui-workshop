using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

[QueryProperty("Monkey", "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
	private readonly IMonkeyNavService _navService;
	[ObservableProperty]
	Monkey _monkey;

	public MonkeyDetailsViewModel(IMonkeyNavService navService)
	{
		_navService = navService;
	}

	[RelayCommand]
	async Task GoBack() =>
		await _navService.GoBack();
}
