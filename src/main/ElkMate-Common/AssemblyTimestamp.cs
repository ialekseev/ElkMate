using System;
using System.IO;
using System.Reflection;

namespace ElkMate.Common
{
    public static class AssemblyTimestamp 
    {    
        public static long Get<T>() 
        {
            return Get(typeof(T));
        }
        
        public static long Get(Type type) 
        {
            return Get(type.Assembly);
        }
        
        public static long Get(Assembly assembly) 
        {
            var path = new Uri(assembly.CodeBase).LocalPath;
            return File.GetLastWriteTime(path).Ticks;
        }
    }
}