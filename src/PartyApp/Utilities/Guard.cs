using System;

namespace PartyApp.Utilities
{
	internal static class Guard
	{
		public static void NotEmpty(this string value, string name)
		{
			if (string.IsNullOrEmpty(value))
				throw new ArgumentException(name);
		}

		public static void NotNull<T>(this T value, string name) where T : class
		{
			if (value == null)
				throw new ArgumentNullException(name);
		}
	}
}
