using System.IO;

namespace SmartElk.ElkMate.Common.Ex
{
    public static class DirectoryEx
    {
        public static string[] GetFilesSafe(string path)
        {
            if (Directory.Exists(path))
            {
                return Directory.GetFiles(path);
            }
            return new string[0];
        }
    }
}
