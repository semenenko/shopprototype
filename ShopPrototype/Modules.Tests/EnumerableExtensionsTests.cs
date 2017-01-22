using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ShopPrototype.Modules.Common;
using System.Linq;
using System.Diagnostics;

namespace Modules.Tests
{
	[TestClass]
	public class EnumerableExtensionsTests
	{
		[TestMethod]
		public void TestGetAllPermutations()
		{
			List<int> input = new List<int> { 1, 2, 3 };
			IEnumerable<IEnumerable<int>> result = input.GetAllPermutations();
			int index = 0;
			foreach(IEnumerable<int> resultItem in result)
			{
				index++;
				string output = string.Concat(resultItem);
				Debug.WriteLine(output);
			}

			Assert.AreEqual(6, result.Count());
		}

		[TestMethod]
		public void TestIncrement()
		{
			int index = 0;
			bool areEqual = index++ == 0;

			int index2 = 0;
			bool areEqual2 = ++index2 == 1;

			Assert.IsTrue(areEqual);
			Assert.IsTrue(areEqual2);
		}
	}
}
