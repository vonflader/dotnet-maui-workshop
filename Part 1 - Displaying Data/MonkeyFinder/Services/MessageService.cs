namespace MonkeyFinder.Services;

public class MessageService : IMessageService
{
    public async Task DisplayDefaultAlert(string title, string message) =>
        await Shell.Current.DisplayAlert(title, message, "OK");
}
