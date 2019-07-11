using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace NotifictionSystem.Processors
{
	public class MqProcessor
	{


		
		private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private static void ReadMessageFromGivenQueueName(string queueName, int interval, MessageSource source, JsonMessageFormatter<NotificationMessage> formatter)
		{
			logger.Info($"Read Messages From Queue : {queueName}");
			MessageQueue messageQueue = GetQueue(queueName);

			if (messageQueue != null && messageQueue.CanRead)
			{
				Message receivedMessage = messageQueue.Receive(new TimeSpan(0, 0, interval));
				receivedMessage.Formatter = formatter;
				logger.Info($"Recieved Message from queue: {receivedMessage}");
				MessageProcessor.ProcessMessage(receivedMessage, source);
			}
		}

		private static MessageQueue GetQueue(string queueName)
		{
			if (MessageQueue.Exists(queueName))
			{
				var queue = new MessageQueue(queueName);
				return queue;
			}
			return null;
		}
	}
}
