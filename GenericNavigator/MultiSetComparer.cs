using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericNavigator
{
    public class MultiSetComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        public bool Equals(IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null)
                return second == null;

            if (second == null)
                return false;

            if (ReferenceEquals(first, second))
                return true;

            var firstCollection = first as ICollection<T>;
            var secondCollection = second as ICollection<T>;
            if (firstCollection != null && secondCollection != null)
            {
                if (firstCollection.Count != secondCollection.Count)
                    return false;

                if (firstCollection.Count == 0)
                    return true;
            }

            return !HaveMismatchedElement(first, second);
        }

        private static bool HaveMismatchedElement(IEnumerable<T> first, IEnumerable<T> second)
        {
            int firstCount;
            int secondCount;

            var firstElementCounts = GetElementCounts(first, out firstCount);
            var secondElementCounts = GetElementCounts(second, out secondCount);

            if (firstCount != secondCount)
                return true;

            foreach (var kvp in firstElementCounts)
            {
                firstCount = kvp.Value;
                secondElementCounts.TryGetValue(kvp.Key, out secondCount);

                if (firstCount != secondCount)
                    return true;
            }

            return false;
        }

        private static Dictionary<T, int> GetElementCounts(IEnumerable<T> enumerable, out int nullCount)
        {
            var dictionary = new Dictionary<T, int>();
            nullCount = 0;

            foreach (T element in enumerable)
            {
                if (element == null)
                {
                    nullCount++;
                }
                else
                {
                    int num;
                    dictionary.TryGetValue(element, out num);
                    num++;
                    dictionary[element] = num;
                }
            }

            return dictionary;
        }

        public int GetHashCode(IEnumerable<T> enumerable)
        {
            int hash = 17;

            foreach (T val in enumerable.OrderBy(x => x))
                hash = hash * 23 + val.GetHashCode();

            return hash;
        }
    }
}
