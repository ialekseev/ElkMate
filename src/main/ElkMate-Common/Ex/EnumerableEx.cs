using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartElk.ElkMate.Common.Ex
{
    public static class EnumerableEx
    {
        public const string DefaultSeparator = ", ";

        public static string JoinToString<T>(this IEnumerable<T> list, Func<T, string> select, string separator, bool doNotJoinDefaultValues)
        {
            if (list == null)
                return string.Empty;

            // ReSharper disable CompareNonConstrainedGenericWithNull
            var targetList = doNotJoinDefaultValues ? list.Where(t => t != null && !t.Equals(default(T))): list;
            // ReSharper restore CompareNonConstrainedGenericWithNull
                                    
            return @select!=null ? string.Join(separator, targetList.Select(@select)) : string.Join(separator, targetList);
        }

        public static string JoinToString<T>(this IEnumerable<T> list, bool doNotJoinDefaultValues)
        {
            return JoinToString(list, null, DefaultSeparator, doNotJoinDefaultValues);
        }

        public static string JoinToString<T>(this IEnumerable<T> list, string separator)
        {
            return JoinToString(list, null, separator, false);
        }
        
        public static string JoinToString<T>(this IEnumerable<T> list)
        {
            return JoinToString(list, null, DefaultSeparator, false);
        }
        
        public static string JoinToString<T>(this IEnumerable<T> list, Func<T, string> select)
        {
            return JoinToString(list, select, DefaultSeparator, false);
        }
        
        public static string JoinToString<T>(this IEnumerable<T> list, Func<T, string> select, string separator)
        {
            return JoinToString(list, select, separator, false);
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
