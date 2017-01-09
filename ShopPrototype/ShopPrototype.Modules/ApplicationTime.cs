using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrototype.Modules
{
	public static class ApplicationTime
	{
		const string MoscowTimeZone = "Russian Standard Time";

		public static DateTime GetApplicationDefaultNow()
		{
			TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(MoscowTimeZone);
			DateTime utcNow = DateTime.UtcNow;

			return TimeZoneInfo.ConvertTimeFromUtc(utcNow, timeZone);
		}

		public static DateTime GetTimeZoneNow(string timeZoneId)
		{
			TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
			DateTime utcNow = DateTime.UtcNow;

			return TimeZoneInfo.ConvertTimeFromUtc(utcNow, timeZone);
		}
	}
}
