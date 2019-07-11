using System;
using System.Messaging;
using Utils;

namespace MessagingQueueService
{
	public class MessagePublisher
	{
		private string _queuePath;
		private MessageQueue _queue;
		private JsonMessageFormatter<NotificationMessage> formatter;

		public MessagePublisher(string queuePath, JsonMessageFormatter<NotificationMessage> _formatter)
		{
			formatter = _formatter;
			_queuePath = queuePath;			
		}

		private void CheckIfQueueExists()
		{
			if (MessageQueue.Exists(_queuePath))
				_queue = new MessageQueue(_queuePath);
			else 
				_queue = MessageQueue.Create(_queuePath);
		}

		public bool WriteMessage(NotificationMessage notificationMessage)
		{
			try
			{				
				var message = new Message { Body = notificationMessage, Label = new Guid().ToString(), Formatter = formatter };
				CheckIfQueueExists();
				_queue?.Send(message);
				Console.WriteLine("Message successfully added to the message queue");
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"There was an error while sending message with message {notificationMessage} to queue {_queuePath} : {ex.Message}");
				return false;
			}
		}

		public void ReadMessage()
		{
			try
			{
				CheckIfQueueExists();
				_queue.Formatter = formatter;
				var message = _queue.Receive().Body as NotificationMessage;
				Console.WriteLine($"Received Message from the queue: {message.RecipientDetails.Name}, {message.RecipientDetails.EmailAddress}, {message.RecipientDetails.PhoneNumber}, {message.Body}, {message.Subject}");
				Console.WriteLine(message);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"There was an error while reading messages from the queue {_queuePath} : {ex.Message}");
			}
		}
	}
}
