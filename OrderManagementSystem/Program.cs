using MessagingQueueService;
using System;
using System.Threading;
using Utils;

namespace OrderManagementSystem
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Starting up Order Management System");
			var publisher = new MessagePublisher(@".\Private$\OrderStatusQueue", new Utils.JsonMessageFormatter<Utils.NotificationMessage>());

			for (int i = 0; i < 10; i++)
			{
				var notificationMessage = new NotificationMessage
				{
					RecipientDetails = new RecipientDetails() { Name = "Abc" + i, EmailAddress = "abc" + i + "@gmail.com", PhoneNumber = "+ 256 478 5478" + i },
					Subject = "Order # 123" + i + " is Placed",
					Body = "Thank you for placing the order # 123 amounting to $15" + i
				};
				Console.WriteLine($"Order management system writing {i}th message to the message queue");
				publisher.WriteMessage(notificationMessage);
				Thread.Sleep(1000);
			}
			Console.ReadLine();
		}
	}
}
