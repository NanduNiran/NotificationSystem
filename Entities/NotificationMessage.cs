using System.Collections.Generic;

namespace Utils
{
	public class NotificationMessage
	{
		public string Body { get; set; }
		public string Subject { get; set; }
		public RecipientDetails RecipientDetails { get; set; }
		
	}

	public class RecipientDetails
	{
		public string Name { get; set; }
		public string EmailAddress { get; set; }
		public string PhoneNumber { get; set; }
	}

}
