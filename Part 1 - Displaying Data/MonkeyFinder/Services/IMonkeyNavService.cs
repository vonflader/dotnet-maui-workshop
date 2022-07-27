namespace MonkeyFinder.Services;

public interface IMonkeyNavService
{
    Task GoToMonkeyDetails(Monkey monkey);
    Task GoBack();
}
