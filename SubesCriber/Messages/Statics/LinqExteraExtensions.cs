using System;
namespace MicroTest1.Messages.Statics
{
    public static class LinqExteraExtensions
    {
        public static IEnumerable<T> Insert<T>(this IEnumerable<T> source, int index, T item)
        {
            var list = new List<T>(source);
            list.Insert(index, item);
            return list;
        }

        public static bool In<T>(this T value, params T[] toSearchItems) where T : IEquatable<T>
        {
            return In(value, toSearchItems?.ToList());
        }
        public static bool In<T>(this T value, IEnumerable<T> toSearchItems) where T : IEquatable<T>
        {
            if (toSearchItems is null || toSearchItems?.Any() == false)
                return false;
            if (value == null)
                return false;
            return toSearchItems.Contains(value);
        }

    }
}

