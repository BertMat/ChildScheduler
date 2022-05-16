using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChildScheduler.Extensions
{
	public static class StringExtensions
	{
		public static string SanitizePhoneNumber(this string value)
		{
			return new string(value.ToCharArray().Where(char.IsDigit).ToArray());
		}

		public static bool IsNullOrWhiteSpace(this string value)
		{
			return string.IsNullOrWhiteSpace(value);
		}
	}
}
