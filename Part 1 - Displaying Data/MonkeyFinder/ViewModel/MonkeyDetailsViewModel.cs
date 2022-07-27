namespace MonkeyFinder.ViewModel;

[QueryProperty("Monkey", "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
	[ObservableProperty]
	Monkey _monkey;

	public MonkeyDetailsViewModel()
	{				
	}	
}
