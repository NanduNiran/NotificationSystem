using log4net;
using System;

namespace NotifictionSystem.Providers
{
	class SmsService : INotificationService
	{
		private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private readonly string _sender;
		private readonly string _receiver;

		public SmsService(string sender, string receiver)
		{
			_sender = sender;
			_receiver = receiver;
		}

		public bool Notify(string subject, string message)
		{
			logger.Info($"Sms being sent with subject: {subject} and message: {message} to phone number: {_receiver} from {_sender}");
			return true;
		}
	}
}
