using System.Text.RegularExpressions;

namespace Utils
{
	public class ValidationHelper
	{

		public static bool IsValidPhoneNumber(string phoneNumberInput)
		{
			Regex _regex = new Regex(@"^(\+91[\-\s])\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
									RegexOptions.CultureInvariant | RegexOptions.Singleline);
			return _regex.IsMatch(phoneNumberInput);

		}

		public static bool IsValidEmail(string emailInput)
		{
			Regex _regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
									RegexOptions.CultureInvariant | RegexOptions.Singleline);
			return _regex.IsMatch(emailInput);
		}
	}
}
