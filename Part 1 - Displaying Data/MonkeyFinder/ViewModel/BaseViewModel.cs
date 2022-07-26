namespace MonkeyFinder.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]    
    bool _isBusy;

    [ObservableProperty]
    string _title;

    public BaseViewModel()
    {            
    }

    public bool IsNotBusy => !IsBusy;    
}
