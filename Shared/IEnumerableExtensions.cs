﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Dobble.Shared
{
	/// <summary>
	/// A class that contains extension methods for IEnumerable.
	/// </summary>
	public static class IEnumerableExtensions
	{
		public static readonly Random rng = new Random();

		/// <summary>
		/// Shuffles the elements of a collection.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
		{
			return source.Shuffle(rng);
		}

		/// <summary>
		/// Shuffles the elements of a collection using a specified random number generator.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The collection to be shuffled</param>
		/// <param name="rng">Random number generator</param>
		/// <returns></returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
		{
			return source.OrderBy(x => rng.Next());
		}

		/// <summary>
		/// Performs an action on each element of a collection.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The collection to be processed</param>
		/// <param name="action">The action to perform on each element</param>
		/// <returns></returns>
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
