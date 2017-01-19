using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ShopPrototype.Modules.Common;

namespace Modules.Tests
{
	[TestClass]
	public class EnumerableExtensionsTests
	{
		[TestMethod]
		public void TestGetAllCombinations()
		{
			List<int> input = new List<int> { 1, 2, 3 };
			List<List<int>> allCombinations = input.GetAllCombinations();

			Assert.AreEqual(9, allCombinations.Count);
		}
	}
}
