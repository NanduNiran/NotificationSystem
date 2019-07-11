using log4net;
using NotifictionSystem.Providers;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Messaging;
using Utils;

namespace NotifictionSystem.Processors
{
	public static class MessageProcessor
	{
		private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private static readonly NameValueCollection AppSettingsReader;

		private static string OrderEmailSender { get; }
		private static string OrderSmsSender { get; }
		private static string IncidentEmailSender { get; }
		private static string IncidentSmsSender { get; }
		private static bool IsValidOrderEmailSender { get; }
		private static bool IsValidOrderSmsSender { get; }
		private static bool IsValidIncidentEmailSender { get; }
		private static bool IsValidIncidentSmsSender { get; }

		static MessageProcessor()
		{
			AppSettingsReader = ConfigurationManager.AppSettings;
			OrderEmailSender = Convert.ToString(AppSettingsReader["OrderSenderEmailId"]);
			OrderSmsSender = Convert.ToString(AppSettingsReader["OrderSenderSmsId"]);
			IncidentEmailSender = Convert.ToString(AppSettingsReader["IncidentSenderEmailId"]);
			IncidentSmsSender = Convert.ToString(AppSettingsReader["IncidentSenderSmsId"]);
			IsValidOrderEmailSender = ValidationHelper.IsValidEmail(OrderEmailSender);
			IsValidOrderSmsSender = ValidationHelper.IsValidEmail(OrderSmsSender);
			IsValidIncidentEmailSender = ValidationHelper.IsValidEmail(IncidentEmailSender);
			IsValidIncidentSmsSender = ValidationHelper.IsValidEmail(IncidentSmsSender);
		}

		public static void ProcessMessage(Message msg, MessageSource source)
		{

			logger.Info($"Read sender details from app settings -> Email sender : {OrderEmailSender}, SMS sender : {OrderSmsSender}");
			var notificationMessage = msg.Body as NotificationMessage;


			if (source.Equals(MessageSource.Order))
			{
				if (IsValidOrderEmailSender && !string.IsNullOrEmpty(notificationMessage.RecipientDetails.EmailAddress))
				{
					SendEmailMessage(OrderEmailSender, notificationMessage.RecipientDetails.EmailAddress, notificationMessage.Subject, notificationMessage.Body);
				}
				if (IsValidOrderSmsSender && !string.IsNullOrEmpty(notificationMessage.RecipientDetails.PhoneNumber))
				{
					SendSmsMessage(OrderSmsSender, notificationMessage.RecipientDetails.PhoneNumber, notificationMessage.Subject, notificationMessage.Body);
				}
			}
			else if (source.Equals(MessageSource.Incident))
			{
				if (IsValidIncidentEmailSender && !string.IsNullOrEmpty(notificationMessage.RecipientDetails.EmailAddress))
				{
					SendEmailMessage(IncidentEmailSender, notificationMessage.RecipientDetails.EmailAddress, notificationMessage.Subject, notificationMessage.Body);

				}
				if (IsValidIncidentSmsSender && !string.IsNullOrEmpty(notificationMessage.RecipientDetails.PhoneNumber))
				{
					SendSmsMessage(IncidentSmsSender, notificationMessage.RecipientDetails.PhoneNumber, notificationMessage.Subject, notificationMessage.Body);

				}
			}
		}

		private static void SendEmailMessage(string fromEmail, string toEmail, string subject, string body )
		{
			INotificationService notificationService = new MailService(fromEmail, toEmail);
			notificationService.Notify(subject, body);
		}

		private static void SendSmsMessage(string fromPhoneNumber, string toPhoneNumber, string subject, string body)
		{
			INotificationService notificationService = new SmsService(fromPhoneNumber, toPhoneNumber);
			notificationService.Notify(subject, body);
		}

	}
}
