using System;
using System.Linq;

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

		public string MiddleName { get; }
		public char MiddleInitial { get; }

		public string LastName { get; }

		public Sex Sex => _sex;

		public DateTime BirthDate { get; }

		public int Age => _city.CurrentTime.Subtract(BirthDate).Days / 365;

		public long Seed => _seed;

		public Citizen(City city, long seed)
		{
			if (city == null) throw new ArgumentNullException(nameof(city));
			_city = city;
			_seed = seed;
			_rng = new RNG(RNG.GetRaw(CITIZEN_RNG_BASE_GEN, seed));

			var sdata = city.Rant.DoPackaged("sz/citizen", _seed);
			FirstName = String.Intern(sdata["firstname"]);
			MiddleName = String.Intern(sdata["middlename"]);
			MiddleInitial = MiddleName.FirstOrDefault(Char.IsLetter);
			LastName = String.Intern(sdata["lastname"]);
			Enum.TryParse(sdata["sex"], true, out _sex);

			// Tilt the distribution towards younger people
			var age = _rng.Next(18, _rng.Next(30, _rng.Next(30, 100)));

			var birthMonth = _rng.Next(12) + 1;

			var birthDay = _rng.Next(DateTime.DaysInMonth(city.StartingTime.Year - age, birthMonth)) + 1;

			BirthDate = new DateTime(city.StartingTime.Year - age, birthMonth, birthDay, 0, 0, 0);
		}
	}
}
