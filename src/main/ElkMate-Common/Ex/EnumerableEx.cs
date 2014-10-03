using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartElk.ElkMate.Common.Ex
{
    public static class EnumerableEx
    {
        public const string DefaultSeparator = ", ";

        public static string JoinToString<T, TTransform>(this IEnumerable<T> list, Func<T, TTransform> select, string separator)
        {
            if (list == null)
                return string.Empty;
            
            list = list.Where(t => t != null).ToArray();
            var targetList = list.Select(@select).Where(t => t != null);

            return string.Join(separator, targetList);
        }
        
        public static string JoinToString<T>(this IEnumerable<T> list, string separator)
        {
            return JoinToString(list, x => x, separator);
        }
        
        public static string JoinToString<T>(this IEnumerable<T> list)
        {
            return JoinToString(list, x => x, DefaultSeparator);
        }
        
        public static string JoinToString<T>(this IEnumerable<T> list, Func<T, string> select)
        {
            return JoinToString(list, select, DefaultSeparator);
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

        public static IList<T> Apply<T>(this IList<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
            return list;
        }
    }
}
