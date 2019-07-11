using System;

namespace MessagingQueueService
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Console.WriteLine("Starting up Message Queue System");
				Console.ReadLine();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"There was an error in the Notification System : {ex.Message}");
			}
			
		}
	}
}
