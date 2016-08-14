using ShopPrototype.Modules.Search.Entities;
using ShopPrototype.Modules.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrototype.Modules.Search
{
	public class SearchModule
	{
		public SearchModel GetSearchModel()
		{
			SearchModel result = new SearchModel();
			result.Facilities = GetFacilities();
			result.Items = result.Facilities.Select(x => new FacilityItem
			{
				Id = x.Id,
				Title = x.Title
			}).ToList();

			result.Places = GetPlaces();

			return result;
		}

		IEnumerable<Facility> GetFacilities()
		{
			return new[]
			{
				new Facility { Id = 1, Title = "Стрижка", DisplayOrder = 0 },
				new Facility { Id = 2, Title = "Маникюр", DisplayOrder = 1 },
				//new Facility { Id = 3, Title = "Подпиливание ногтей при разговоре по Skype", DisplayOrder = 2 },
				new Facility { Id = 4, Title = "Макияж", DisplayOrder = 3 },
				new Facility { Id = 5, Title = "Укладка бровей", DisplayOrder = 4 }
			};
		}

		IEnumerable<Place> GetPlaces()
		{
			return new[]
			{
				new Place { Id = 1, Name = "Бизнес-центр класса А", Latitude = 55.678672m, Longitude = 37.632153m }
			};
		}

		//55.678672, 37.632153
	}
}
