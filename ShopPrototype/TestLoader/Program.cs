using ShopPrototype.DataAccess.EF;
using System;
using System.Diagnostics;

namespace TestLoader
{
	class Program
	{
		static void Main(string[] args)
		{
			InitialLoader loader = new InitialLoader();

			Stopwatch stopwatch = Stopwatch.StartNew();

			Console.WriteLine("Load started");
			loader.Load();
			stopwatch.Stop();
			Console.WriteLine("Finished at {0} ms", stopwatch.ElapsedMilliseconds);

			//55.787100, 37.454614

			//string name = loader.TestLocation(55.787100, 37.454614);

			//Console.WriteLine(name);


			Console.ReadKey();
		}
	}
}
