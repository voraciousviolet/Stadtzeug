using System;

using Rant;

namespace Stadtzeug
{
	public sealed class Citizen
	{
		public const long CITIZEN_RNG_BASE_GEN = 8000;

		private readonly long _seed;
		private readonly City _city;
		private readonly Sex _sex;
		private readonly RNG _rng;

		public string FirstName { get; }

		public string LastName { get; }

		public Sex Sex => _sex;

		public DateTime BirthDate { get; }

		public Citizen(City city, long seed)
		{
			if (city == null) throw new ArgumentNullException(nameof(city));
			_city = city;
			_seed = seed;
			_rng = new RNG(RNG.GetRaw(CITIZEN_RNG_BASE_GEN, seed));

			var sdata = city.Rant.DoPackaged("sz/citizen", _seed);
			FirstName = String.Intern(sdata["firstname"]);
			LastName = String.Intern(sdata["lastname"]);
			Enum.TryParse(sdata["sex"], true, out _sex);
			int age, birthMonth, birthDay;
			
			if (!int.TryParse(sdata["age"], out age) || age < 0)
			{
				age = (_rng.Next(18, 30) + _rng.Next(18, 70)) / 2;
			}

			if (!int.TryParse(sdata["birthmonth"], out birthMonth) || birthMonth < 0 || birthMonth > 11)
			{
				birthMonth = _rng.Next(12);
			}

			if (!int.TryParse(sdata["birthday"], out birthMonth) || birthMonth < 0 || birthMonth > 11)
			{
				birthMonth = _rng.Next(32);
			}
		}
	}
}
