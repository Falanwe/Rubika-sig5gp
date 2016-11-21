using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardBattle
{
    public static class Helper
    {
        public static string With(this string format, params object[] parameters)
        {
            return string.Format(format, parameters);
        }

        public static IEnumerable<IEnumerable<T>> AllTuples<T>(this IEnumerable<T> source, int n)
        {
            if(n < 0)
            {
                yield break;
            }

            if(n == 0)
            {
                yield return Enumerable.Empty<T>();
                yield break;
            }

            if(!source.Any())
            {
                yield break;
            }

            var first = source.First();
            foreach(var tuple in source.Skip(1).AllTuples(n-1))
            {
                yield return new[] { first }.Concat(tuple);
            }

            foreach(var tuple in source.Skip(1).AllTuples(n))
            {
                yield return tuple;
            }
        }
    }
}
