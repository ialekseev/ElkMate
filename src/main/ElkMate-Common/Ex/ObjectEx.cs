using System;

namespace SmartElk.ElkMate.Common.Ex
{
    public static class ObjectEx
    {
        public static T Apply<T>(this T obj, Action<T> apply)
        {                        
            apply(obj);
            return obj;
        }
    }
}
