using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

        public static string GetStringBeforeFirstOccurrenceOfChar(this string str, char ch)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            
            var index = str.IndexOf(ch);
            if (index > 0)
            {
                return str.Substring(0, index);
            }
            return string.Empty;
        }

        public static string GetStringAfterFirstOccurrenceOfChar(this string str, char ch)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var index = str.IndexOf(ch);
            if (index >= 0 && index < str.Length - 1)
            {
                return str.Substring(index + 1);
            }
            return string.Empty;
        }

        public static IEnumerable<string> SplitToArray(this string str, char separator)
        {                        
            if (string.IsNullOrEmpty(str))
                return new string[0];

            return str.Split(separator).Where(t=>!string.IsNullOrEmpty(t)).ToArray();
        }

        public static string StripTags(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return Regex.Replace(str, @"<[^>]+>|&nbsp;", "").Trim();
        }

        public static string ReplaceNewLinesWith(this string str, string replacement)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return Regex.Replace(str, @"\r\n?|\n", replacement);
        }
                
        public static string ReplaceNewLinesWithBrs(this string str)
        {
            return ReplaceNewLinesWith(str, "<br/>");                        
        }

        public static string RemoveNewLines(this string str)
        {
            return ReplaceNewLinesWith(str, string.Empty);
        }

        public static string RemoveSubstring(this string str, string subString)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return str.Replace(subString, string.Empty);
        }

        public static string ReplaceSubstringWith(this string str, string subString, string with)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(subString))
                return str;

            if (with == null)
                return str;

            return str.Replace(subString, with);
        }
        
        public static string ReplaceBrsWithNewLines(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return Regex.Replace(str, @"<br\s*[\/]?>", "\n", RegexOptions.IgnoreCase);
        }

        public static string HtmlEncode(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            return System.Net.WebUtility.HtmlEncode(source);
        }

        public static string HtmlDecode(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            return System.Net.WebUtility.HtmlDecode(source);
        }

        public static string ReviveEncodedBrs(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return Regex.Replace(str, @"&lt;br\s*[\/]?&gt;", "<br/>", RegexOptions.IgnoreCase);
        }
    }
}
