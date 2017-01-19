using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrototype.Modules.Common
{
	public static class EnumerableExtensions
	{
		public static List<List<T>> GetAllCombinations<T>(this List<T> input)
		{
			List<List<T>> result = new List<List<T>>();
			List<T> inputList = input.ToList();

			result.Add(new List<T>());
			result.Last().Add(inputList[0]);
			if (inputList.Count == 1)
				return result;

			List<List<T>> tailCombinations = GetAllCombinations(inputList.Skip(1).ToList());
			tailCombinations.ForEach(combination =>
			{
				result.Add(new List<T>(combination));
				combination.Add(inputList[0]);
				result.Add(new List<T>(combination));
			});

			return result;
		}
	}
}
