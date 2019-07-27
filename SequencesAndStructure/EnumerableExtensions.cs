using System;
using System.Collections.Generic;
using System.Linq;

namespace SequencesAndStructure
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"> T must be a reference type (class) because we plan to  use null as the seed for the Aggregate function.</typeparam>
        /// <typeparam name="TKey">TKey must be comparable because we plan on comparing the keys to fnd the best fitting element.</typeparam>
        /// <param name="sequence"> Sequence of elements to find the minimun in.</param>
        /// <param name="criterion"> Func used to evaluate each element to filter out the one with the minimum value.</param>
        /// <returns></returns>
        public static T WithMinimum<T, TKey>(this IEnumerable<T> sequence, Func<T, TKey> criterion) where T: class where TKey : IComparable<TKey>
        {
            return sequence
                .Select(element => Tuple.Create(element, criterion(element)))
                .Aggregate((Tuple<T, TKey>)null, (best, current) => best == null || current.Item2.CompareTo(best.Item2) < 0 ? current : best)
                .Item1;
        }
    }
}