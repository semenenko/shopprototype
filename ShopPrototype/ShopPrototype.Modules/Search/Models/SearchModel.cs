using ShopPrototype.Modules.Search.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrototype.Modules.Search.Models
{
	public class SearchModel
	{
		public IEnumerable<Facility> Facilities { get; set; }
		public IEnumerable<FacilityItem> Items { get; set; }
		public IEnumerable<Place> Places { get; set; }

		public string GetPlacesScript()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("<script type='text/javascript'>");
			builder.Append("var places = [");
			foreach(Place place in Places)
			{
				builder.Append("{");
				builder.AppendFormat("id:{0}, name:'{1}', latitude:{2}, longitude:{3}", place.Id, place.Name, 
					place.Latitude.ToString(CultureInfo.GetCultureInfo("en-GB")), 
					place.Longitude.ToString(CultureInfo.GetCultureInfo("en-GB")));
				builder.Append("},");
			}
			builder.Append("];");
			builder.Append("</script>");

			return builder.ToString();
		}
	}
}
