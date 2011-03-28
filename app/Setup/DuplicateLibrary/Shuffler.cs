using System;
using System.Collections.Generic;
using System.Text;

namespace OxigenIIAdvertising.ServerConnectAttempt
{
  /// <summary>
  /// Provides static methods that shuffle collections
  /// </summary>
  public static class Shuffler
  {
    private static Random rnd = new Random();

    /// <summary>
    /// Shuffles an array of type T
    /// </summary>
    /// <typeparam name="T">Type of objects contained in the array</typeparam>
    /// <param name="array">the array to shuffle</param>
    /// <returns>a new array with the contents of the original array, shuffled</returns>
    public static T[] ShuffleArray<T>(T[] array)
    {
      T[] a = new T[array.Length];

      array.CopyTo(a, 0);

      byte[] b = new byte[a.Length];
      
      rnd.NextBytes(b);

      Array.Sort(b, a);

      return a;
    }
  }
}
