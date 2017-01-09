using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrototype.Modules.ClientServices
{
	public class ClientModule
	{
		public ClientModule(IClientModuleRepository repository)
		{
			this.repository = repository;
		}

		readonly IClientModuleRepository repository;

		public IndexModel GetIndexModel()
		{
			using (repository.BeginUnitOfWork())
			{
				IndexModel result = new IndexModel();

				result.Categories = repository.GetFacilitiesByGroups();

				return result;
			}
		}

		public SearchResultModel GetSearchResult(IndexModel model)
		{
			using (repository.BeginUnitOfWork())
			{
				IEnumerable<SelectedFacilities> selectedFacilities = model.Categories
					.Where(c => c.Facilities != null)
					.SelectMany(c => c.Facilities)
					.Where(f => f.Checked)
					.Select(f => new SelectedFacilities
					{
						FacilityId = f.Id,
						FacilityName = f.Name
					}).ToList();

				SearchResultModel result = new SearchResultModel
				{
					Lat = model.Lat,
					Long = model.Long,
					Facilities = selectedFacilities
				};

				SimpleSearchCriteria criteria = new SimpleSearchCriteria
				{
					Lat = model.Lat,
					Long = model.Long,
					Facilities = selectedFacilities.Select(f => f.FacilityId).ToList()
				};

				result.Salons = repository.GetSalonsByCriteria(criteria);

				return result;
			}
		}
	}
}
