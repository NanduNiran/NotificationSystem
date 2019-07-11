using log4net;
using NotifictionSystem.Processors;
using System;
using System.Configuration;
using System.Messaging;
using System.Threading.Tasks;
using Utils;

namespace NotifictionSystem
{
	public class Program
	{
		private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private static MessageQueue _orderMessageQueue;
		const string privateQueuePrefix = @".\Private$\";
		private static string _queueName = "";
		
		//Lazy property, creating the queue instance only when it is needed
		private static MessageQueue OrderMessageQueue
		{
			get {
				if (_orderMessageQueue == null)
				{
					var orderQueueName = Convert.ToString(ConfigurationManager.AppSettings["OrderQueueName"]);
					if (!string.IsNullOrEmpty(orderQueueName))
						_queueName = privateQueuePrefix + orderQueueName;
					if (!string.IsNullOrEmpty(_queueName))
					{
						if (!MessageQueue.Exists(_queueName))
							MessageQueue.Create(_queueName);
						_orderMessageQueue = new MessageQueue(_queueName, QueueAccessMode.ReceiveAndAdmin)
						{
							Formatter = new JsonMessageFormatter<NotificationMessage>()
						};
					}
					else
						return null;
				}
				return _orderMessageQueue;
			}
		}

		static void Main(string[] args)
		{
			try
			{
				Task.Factory.StartNew(()=>ReadMessageFromGivenQueueName(MessageSource.Order));
				

				//There could be another task involed for reading incident queue messages
				//Task.Factory.StartNew(() => ReadMessageFromGivenQueueName(MessageSource.Incident));

				Console.ReadLine();
			}
			catch (Exception ex)
			{
				logger.Error($"There was an error in the Notification System : {ex.Message}");
			}
		}

		private static void ReadMessageFromGivenQueueName(MessageSource source)
		{
			logger.Info($"Read Messages From order queue");
			
			if (source.Equals(MessageSource.Order))
			{
				var messageQueue = OrderMessageQueue;
				if (OrderMessageQueue != null)
				{

					while (true)
					{
						if (messageQueue != null && messageQueue.CanRead)
						{
							Message receivedMessage = messageQueue.Receive();
							logger.Info($"Recieved Message from queue: {receivedMessage}");
							MessageProcessor.ProcessMessage(receivedMessage, source);
						}
					}
				}
			}
		}


	}
}
