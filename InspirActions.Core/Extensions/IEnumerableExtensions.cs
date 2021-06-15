using System;
using System.Collections.Generic;
using System.Linq;

namespace InspirActions.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        private static readonly Random rng = new();

        /// <summary>
        /// Gets a random element that satisfies the given condition.
        /// </summary>
        public static T Random<T>(this IEnumerable<T> list, Func<T, bool> condition)
            => list.Shuffle().First(condition);

        /// <summary>
        /// Gets a random element.
        /// </summary>
        public static T Random<T>(this IEnumerable<T> list)
            => list.Shuffle().First();

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
            => list.OrderBy(i => rng.Next());
    }
}
