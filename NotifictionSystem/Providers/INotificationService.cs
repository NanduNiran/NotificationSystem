namespace NotifictionSystem.Providers
{
	public interface INotificationService
	{
		bool Notify(string subject, string message);
	}
}
