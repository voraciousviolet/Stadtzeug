using System;
using System.Collections.Generic;

namespace Stadtzeug
{
	internal class SeedCache<T>
	{
		private readonly int _capacity;
		private readonly Func<long, T> _initializer;
		private int pos;

		// Maps seeds to their locations in the arrays
		private readonly Dictionary<long, int> map; 

		// Holds the caches items
		private readonly T[] items;

		// Holds the seed for each item
		private readonly long[] seeds;


		public SeedCache(int capacity, Func<long, T> initializer)
		{
			if (initializer == null) throw new ArgumentNullException(nameof(initializer));
			if (capacity < 1) throw new ArgumentException("Capacity must be greater than zero.");
			_capacity = capacity;
			_initializer = initializer;
			items = new T[capacity];
			seeds = new long[capacity];
			map = new Dictionary<long, int>();
		}

		public T Get(long seed)
		{
			int i;
			if (!map.TryGetValue(seed, out i))
			{
				items[pos] = items[i];
			}
			else
			{
				items[pos] = _initializer(seed);
				map.Remove(seeds[pos]);
			}

			seeds[pos] = seed; // Update the seed record for the current item
			i = pos; // Recycle the variable to hold the return value index
			map[seed] = pos; // Update the index to the item's new location
			pos = (pos + 1) % _capacity; // Incrememt the write position
			return items[pos];
		}
	}
}