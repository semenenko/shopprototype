using ShopPrototype.Modules.Search.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrototype.Modules.Search.Models
{
	public class SearchModel
	{
		public IEnumerable<Facility> Facilities { get; set; }
	}
}
