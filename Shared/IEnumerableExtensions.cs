using System;
using System.Collections.Generic;
using System.Linq;

namespace Dobble.Shared
{
	public static class IEnumerableExtensions
	{
		public static readonly Random rng = new Random();
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
		{
			return source.Shuffle(rng);
		}

		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
		{
			return source.OrderBy(x => rng.Next());
		}

		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (T item in source)
			{
				action(item);
			}
			return source;
		}
	}
}
