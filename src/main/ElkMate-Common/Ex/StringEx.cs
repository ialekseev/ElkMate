using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SmartElk.ElkMate.Common.Ex
{
    public static class StringEx
    {
        public static string CombinePath(this string left, string right)
        {
            if (right == null)
                return left;
            if (left == null)
                return right;
            return Path.Combine(left, right);
        }

        public static byte[] ToBytes(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return new byte[0];
                            
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static Stream ToStream(this string str)
        {
            if (str == null)
                return null;
            
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;   
        }

        public static string ToEncoding(this string str, Encoding encoding)
        {
            if (str == null)
                return null;
            
            var encodedBytes = encoding.GetBytes(str);
            return encoding.GetString(encodedBytes);
        }

        public static string ToEncoding(this string str, Encoding encoding, int takeBytes)
        {
            if (str == null)
                return null;

            var encoded = str.ToEncoding(encoding);
            return new string(encoded.TakeWhile((c, i) => encoding.GetByteCount(encoded.Substring(0, i + 1)) <= takeBytes).ToArray());
        }
        
        public static string ToUtf8(this string str)
        {
            if (str == null)
                return null;
            
            return str.ToEncoding(Encoding.UTF8);                        
        }
                
        public static string ToUtf8(this string str, int takeBytes)
        {
            if (str == null)
                return null;
            return str.ToEncoding(Encoding.UTF8, takeBytes);                        
        }
        
        public static string Strip(this string str, int limit)
        {
            if (str == null)
                return null;

            if (limit < 0)
                return str;

            if (limit > str.Length)
                return str;

            return str.Substring(0, limit);
        }
    }
}
