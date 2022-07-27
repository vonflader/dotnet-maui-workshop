namespace MonkeyFinder.Services;

public interface IMessageService
{
    Task DisplayDefaultAlert(string title, string message);
}
