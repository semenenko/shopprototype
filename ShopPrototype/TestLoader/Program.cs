using ShopPrototype.DataAccess.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoader
{
	class Program
	{
		static void Main(string[] args)
		{
			InitialLoader loader = new InitialLoader();

			//loader.Load();

			//55.787100, 37.454614

			string name = loader.TestLocation(55.787100, 37.454614);

			Console.WriteLine(name);


			Console.ReadKey();
		}
	}
}
