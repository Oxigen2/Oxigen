using System;
using System.Collections.Generic;

namespace LinqToHashSet
{
  /// <summary>
  /// This extension method class will add a ToHashSet<> 
  /// in exactly the same way it is provided by the others:
  /// 
  /// ToList(), ToArray(), ToDictionary().. Now ToHashSet() is available
  /// </summary>
  public static class HashSetLinqAccess
  {
    private static HashSet<T> ToHashSet<T>(this IEnumerable<T> fromEnumerable,
        IEqualityComparer<T> comparer)
    {
      if (fromEnumerable == null)
        throw new ArgumentNullException("fromEnumerable");

      if (comparer == null)
        comparer = EqualityComparer<T>.Default;

      return !typeof(HashSet<T>).IsAssignableFrom(fromEnumerable.GetType())
                     ? new HashSet<T>(fromEnumerable, comparer)
                     : (HashSet<T>)fromEnumerable;
    }

    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> fromEnumerable)
    {

      return ToHashSet(fromEnumerable, EqualityComparer<T>.Default);
    }
  }
}
