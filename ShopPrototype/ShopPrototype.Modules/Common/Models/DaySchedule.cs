using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopPrototype.Modules.Common.Models
{
	public class DaySchedule
	{
		public int SalonId { get; private set; }

		public string SalonName { get; private set; }

		public DateTime Date { get; private set; }

		public IEnumerable<ScheduleItem> Items { get; private set; }

		public IEnumerable<CategoryHeader> Headers { get; set; }

		public IEnumerable<ScheduleRow> Rows { get; set; }

		public void Initialize(int salonId, string salonName, DateTime date, IEnumerable<ScheduleItem> items)
		{
			SalonId = salonId;
			SalonName = salonName;
			Items = items;
			Date = date;

			if (!Items.Any())
			{
				Headers = Enumerable.Empty<CategoryHeader>();
				Rows = Enumerable.Empty<ScheduleRow>();
			}
			else
			{
				IEnumerable<IGrouping<DateTime, ScheduleItem>> itemsByDateTime = items.GroupBy(x => x.ItemStartsAt);
				List<ScheduleRow> rowsList = new List<ScheduleRow>();

				foreach(IGrouping<DateTime, ScheduleItem> group in itemsByDateTime)
				{
					ScheduleRow row = new ScheduleRow
					{
						RowDateTime = group.Key
					};

					row.Items = group.OrderBy(x => x.CategoryId).ToList();

					rowsList.Add(row);
				}

				Rows = rowsList.OrderBy(x => x.RowDateTime).ToList();
				Headers = rowsList.First().Items.Select(x => new CategoryHeader
				{
					Id = x.CategoryId,
					Name = x.CategoryName
				}).OrderBy(x => x.Id).ToList();
			}
		}
	}

	public class ScheduleItem
	{
		public int CategoryId { get; set; }

		public string CategoryName { get; set; }

		public bool Available { get; set; }

		public DateTime ItemStartsAt { get; set; }

		public DateTime ItemEndsAt { get; set; }
	}

	public class CategoryHeader
	{
		public int Id { get; set; }

		public string Name { get; set; }
	}

	public class ScheduleRow
	{
		public DateTime RowDateTime { get; set; }

		public string RowTime
		{
			get
			{
				if (RowDateTime.Minute != 0 && RowDateTime.Minute != 30)
					return string.Empty;

				return RowDateTime.ToShortTimeString();
			}
		}

		public IEnumerable<ScheduleItem> Items { get; set; }
	}
}
