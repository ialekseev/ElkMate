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

        public static IEnumerable<T> ReplaceItems<T, TId>(this IEnumerable<T> list, Func<T, TId> identity, Func<T, bool> whereReplace, T replaceWith)
        {
            list = list.ToList();
            var itemsToReplace = list.Where(whereReplace).Select(identity).ToList();
            return (from item in list let id = identity(item) select itemsToReplace.Contains(id) ? replaceWith : item).ToList();
        }

        public static IEnumerable<T> Merge<T, TId>(this IEnumerable<T> list, IEnumerable<T> other, Func<T, TId> keyProperty)
        {
            if (list == null)
                return other;
            if (other == null)
                return list;

            var listAsDict = list.ToDictionary(keyProperty);
            foreach (var item in other)
            {
                listAsDict[keyProperty(item)] = item;
            }
            
            return listAsDict.Values.ToList();
        }
    }
}
