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

		//http://stackoverflow.com/questions/15150147/all-permutations-of-a-list
		public static IEnumerable<IEnumerable<T>> GetAllPermutations<T>(this IEnumerable<T> sequence)
		{
			if (sequence == null)
			{
				yield break;
			}

			List<T> list = sequence.ToList();

			if (!list.Any())
			{
				yield return Enumerable.Empty<T>();
			}
			else
			{
				int startingElementIndex = 0;

				foreach (T startingElement in list)
				{
					IEnumerable<T> remainingItems = list.AllExcept(startingElementIndex);

					foreach (IEnumerable<T> permutationOfRemainder in remainingItems.GetAllPermutations())
					{
						yield return startingElement.Concat(permutationOfRemainder);
					}

					startingElementIndex++;
				}
			}
		}

		static IEnumerable<T> Concat<T>(this T firstElement, IEnumerable<T> secondSequence)
		{
			yield return firstElement;
			if (secondSequence == null)
			{
				yield break;
			}

			foreach (T item in secondSequence)
			{
				yield return item;
			}
		}

		static IEnumerable<T> AllExcept<T>(this IEnumerable<T> sequence, int indexToSkip)
		{
			if (sequence == null)
			{
				yield break;
			}

			int index = 0;

			foreach (T item in sequence.Where(item => index++ != indexToSkip))
			{
				yield return item;
			}
		}
	}
}
