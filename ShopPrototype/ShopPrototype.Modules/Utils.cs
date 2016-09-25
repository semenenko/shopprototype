using System;

namespace ShopPrototype.Modules
{
	public static class Utils
	{
		const string RussianTimeZoneId = "Russian Standard Time";
		public static DateTime GetApplicationDateTime()
		{
			TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(RussianTimeZoneId);
			DateTime utcNow = DateTime.UtcNow;

			return TimeZoneInfo.ConvertTimeFromUtc(utcNow, timeZone);
		}
	}
}
