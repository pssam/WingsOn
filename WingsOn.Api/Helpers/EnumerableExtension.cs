using System;
using System.Collections.Generic;
using System.Linq;

namespace WingsOn.Api.Helpers
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TValue> Distinct<TValue, TId>(this IEnumerable<TValue> source, Func<TValue, TId> getId)
        {
            return source.GroupBy(getId).Select(group => group.First());
        }
    }
}
