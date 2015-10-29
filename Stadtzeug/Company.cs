namespace Stadtzeug
{
	public sealed class Company
	{
		private readonly City _city;
		private readonly long _seed;

		public string Name { get; }

		private Company(City city, long seed)
		{
			_seed = seed;
			_city = city;

			var sdata = city.Rant.DoPackaged("sz/company");
			Name = sdata["name"];
		}
	}
}