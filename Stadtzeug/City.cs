using System;

using Rant;

namespace Stadtzeug
{
	public sealed class City
	{
		public const long CITIZEN_BASE_GEN = 0x0000000000000001L;
		public const long COMPANY_BASE_GEN = 0x0000000000000002L;
		public const long STREET_BASE_GEN = 0x0000000000000004L;
		public const long LAW_BASE_GEN = 0x0000000000000008L;

		public const float TimeScale = 2.75f;

		public const int CITIZEN_CACHE_CAPACITY = 300;

		private readonly long _seed;
		private readonly RantEngine _rant;

		private readonly SeedCache<Citizen> citizenCache;

		public long Seed => _seed;
		public RantEngine Rant => _rant;
		public DateTime StartingTime { get; }
		public DateTime CurrentTime { get; private set; }

		public string Name { get; }

		public City(long seed, RantEngine rant)
		{
			_seed = seed;
			_rant = rant;

			citizenCache = new SeedCache<Citizen>(CITIZEN_CACHE_CAPACITY, s => new Citizen(this, s));

			var data = rant.DoPackaged("sz/city", seed);

			Name = data["name"];
			int year, month, day;
			if (!(
					int.TryParse(data["year"], out year) &&
					int.TryParse(data["month"], out month) &&
					int.TryParse(data["day"], out day)))
			{
				year = 1990;
				month = 4;
				day = 1;
			}
			StartingTime = CurrentTime = new DateTime(year, month, day);
		}

		public void Update(float dt)
		{
			CurrentTime = CurrentTime.AddSeconds(dt * TimeScale);

		}

		public Citizen GetCitizen(int id)
		{
			return citizenCache.Get(RNG.GetRaw(RNG.GetRaw(_seed, CITIZEN_BASE_GEN), id));
		}
	}
}