using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartElk.ElkMate.Common.Ex
{
    public static class EnumerableEx
    {
        public static string JoinToString<T>(this IEnumerable<T> list, string separator)
        {
            if (list == null)
                return string.Empty;            
            return string.Join(separator, list);
        }

        public static string JoinToString<T>(this IEnumerable<T> list)
        {
            return JoinToString(list, ", ");
        }

        public static string JoinToString<T>(this IEnumerable<T> list, Func<T, string> select, string separator)
        {
            return JoinToString(list.Select(select), separator);
        }
        
        public static string JoinToString<T>(this IEnumerable<T> list, Func<T, string> select)
        {
            return JoinToString(list.Select(select), ", ");
        }
    }
}
