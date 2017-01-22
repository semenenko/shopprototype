using ShopPrototype.Modules.Common;
using ShopPrototype.Modules.Entities;
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

		IEnumerable<SelectedFacilities> GetFacilitiesSelected(IndexModel model)
		{
			return model.Categories
					.Where(c => c.Facilities != null)
					.SelectMany(c => c.Facilities)
					.Where(f => f.Checked)
					.Select(f => new SelectedFacilities
					{
						FacilityId = f.Id,
						FacilityName = f.Name
					}).ToList();
		}

		//move to own class and split
		public IEnumerable<SalonModel> GetSalonsForTimeSlots(IEnumerable<SalonModel> salons, IEnumerable<Facility> facilities, IEnumerable<SalonCategoryTimeSlot> slots, DateTime datetime)
		{
			int waitTimeMin = 15;
			int facilityDurationMin = 30;

			IEnumerable<int> facilitiesIds = facilities.Select(x => x.Id).OrderBy(x => x).ToList();

			Dictionary<int, Facility> facilitiesDictionary = facilities.ToDictionary(x => x.Id);

			IEnumerable<IEnumerable<int>> facilitiesPermutations = facilitiesIds.GetAllPermutations();

			List<SalonModel> result = new List<SalonModel>();

			foreach(IEnumerable<int> permutation in facilitiesPermutations)
			{
				foreach(SalonModel salon in salons)
				{
					IEnumerable<SalonCategoryTimeSlot> salonSlots = slots.Where(x => x.SalonId == salon.SalonId).ToList();

					if (!salonSlots.Any())
						continue;

					bool allFacilitiesAvailable = true;

					DateTime facilityDateTime = datetime;

					foreach(int facilityId in permutation)
					{
						int categoryId = facilitiesDictionary[facilityId].FacilityCategoryId;
						IEnumerable<SalonCategoryTimeSlot> slotsForFacility = slots.Where(x => x.CategoryId == categoryId).ToList();

						if (!slotsForFacility.Any())
						{
							allFacilitiesAvailable = false;
							break;
						}

						SalonCategoryTimeSlot currentSlot = slotsForFacility.First();

						if ((currentSlot.Start - facilityDateTime).Minutes > waitTimeMin)
						{
							allFacilitiesAvailable = false;
							break;
						}

						List<SalonCategoryTimeSlot> timeSlotsBunle = new List<SalonCategoryTimeSlot>();
						timeSlotsBunle.Add(currentSlot);
						
						foreach(SalonCategoryTimeSlot slot in slotsForFacility)
						{
							if (slot.Start == currentSlot.End)
								timeSlotsBunle.Add(slot);

							if (timeSlotsBunle.Sum(x => x.DurationInMin) >= facilityDurationMin)
								break;
						} 
						
						if (timeSlotsBunle.Sum(x => x.DurationInMin) < facilityDurationMin)
						{
							allFacilitiesAvailable = false;
							break;
						}

						facilityDateTime = timeSlotsBunle.OrderBy(x => x.Start).Last().End;
					}

					if (allFacilitiesAvailable && !result.Any(x => x.SalonId == salon.SalonId))
					{
						result.Add(salon);
					}
				}
			}

			return result;
		}

		public SearchResultModel GetSearchResult(IndexModel model)
		{
			using (repository.BeginUnitOfWork())
			{
				IEnumerable<SelectedFacilities> selectedFacilities = GetFacilitiesSelected(model);

				IEnumerable<int> selectedFacilitiesIds = selectedFacilities.Select(x => x.FacilityId).ToList();

				SearchResultModel result = new SearchResultModel(model.Lat, model.Long, selectedFacilities);

				SimpleSearchCriteria criteria = new SimpleSearchCriteria(model.Lat, model.Long, selectedFacilitiesIds);

				IEnumerable<SalonModel> salons = repository.GetNearestSalons(criteria.Lat, criteria.Long, 20);

				IEnumerable<Facility> facilities = repository.GetFacilities(selectedFacilitiesIds);

				IEnumerable<int> selectedCategoriesIds = facilities.Select(x => x.FacilityCategoryId).ToList();

				IEnumerable<SalonCategoryTimeSlot> slotsAvailable = repository.GetSlotsAvailable(criteria.DateTime, selectedCategoriesIds);

				//salons = 

				result.Salons = salons;

				return result;
			}
		}
	}
}
