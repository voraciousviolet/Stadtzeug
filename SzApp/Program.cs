using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rant;

using Stadtzeug;

namespace SzApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var rant = new RantEngine();
			rant.LoadPackage("Stadtzeug-1.0.0.rantpkg");
			rant.Dictionary.IncludeHiddenClass("nsfw");
            var city = new City(9001, rant);
			Console.WriteLine($"{city.Name}, {city.CurrentTime}\n");
			for (int i = 0; i < 45; i++)
			{
				var crime = rant.DoPackaged("sz/crime/conduct");
                Console.WriteLine($"{crime} (severity: {crime["severity"]})");
			}
			Console.ReadKey();
		}
	}
}
