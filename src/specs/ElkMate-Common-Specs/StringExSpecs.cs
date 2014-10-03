// ReSharper disable InconsistentNaming

using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using SmartElk.ElkMate.Common.Ex;

namespace SmartElk.ElkMate.Common.Specs
{
    public class StringExSpecs
    {
        [TestFixture]
        public class when_trying_to_combine_paths
        {            
            [TestCase("C:\\", "File1.txt", Result = "C:\\File1.txt")]
            [TestCase("", "File1.txt", Result = "File1.txt")]
            [TestCase(null, "File1.txt", Result = "File1.txt")]
            [TestCase("C:\\", "", Result = "C:\\")]
            [TestCase("C:\\", null, Result = "C:\\")]
            [TestCase("C:\\Test\\", "1.htm", Result = "C:\\Test\\1.htm")]
            [TestCase("C:\\Test", "1.htm", Result = "C:\\Test\\1.htm")]
            public string should_combine_paths_properly(string left, string right)
            {
                return left.CombinePath(right);
            }
        }

        [TestFixture]
        public class when_trying_to_get_bytes_array_from_string
        {
            [Test]            
            public void should_return_bytes_array()
            {
                //arrange
                var str = "hello12345";
                //act
                var result = str.ToBytes();
                //assert                                
                result.Length.Should().Be(sizeof (char)*str.Length);
                
                var resultStr = System.Text.Encoding.Unicode.GetString(result);
                resultStr.Should().Be(str);
            }
        }

        [TestFixture]
        public class when_trying_to_get_bytes_from_null_string
        {
            [Test]
            public void should_return_empty_bytes_array()
            {
                //arrange
                string str = null;
                //act
                var result = str.ToBytes();
                //assert
                result.Length.Should().Be(0);
            }
        }

        [TestFixture]
        public class when_trying_to_get_bytes_from_empty_string
        {
            [Test]
            public void should_return_empty_bytes_array()
            {
                //arrange
                var str = "";
                //act
                var result = str.ToBytes();
                //assert
                result.Length.Should().Be(0);
            }
        }

        [TestFixture]
        public class when_trying_to_get_stream_from_string
        {
            [TestCase("hello12345")]
            [TestCase("")]
            public void should_return_stream(string str)
            {                                
                //act
                var resultStream = str.ToStream();

                //assert
                var reader = new StreamReader(resultStream);
                var resultStr  = reader.ReadToEnd();
                str.Should().Be(resultStr);
            }
        }

        [TestFixture]
        public class when_trying_to_get_stream_from_null_string
        {
            [Test]
            public void should_return_null()
            {
                //arrange
                string str = null;
                
                //act
                var resultStream = str.ToStream();
                
                //assert
                resultStream.Should().BeNull();
            }
        }

        [TestFixture]
        public class when_trying_to_transform_string_to_utf8
        {
            [Test]
            public void should_transform_string()
            {
                //arrange
                string str = "hello";

                //act
                var result = str.ToUtf8();

                //assert
                result.Should().Be(str);
            }
        }

        [TestFixture]
        public class when_trying_to_transform_null_string_to_utf8
        {
            [Test]
            public void should_return_null()
            {
                //arrange
                string str = null;

                //act
                var result = str.ToUtf8();

                //assert
                result.Should().BeNull();
            }
        }

        [TestFixture]
        public class when_trying_to_transform_string_to_encoding_and_limit_result
        {
            [Test]
            public void should_transform_and_limit_string()
            {
                //arrange
                string str = "hello";

                //act
                var resultUtf16 = str.ToEncoding(Encoding.Unicode, 2);
                var resultUtf8 = str.ToUtf8(2);

                //assert                
                resultUtf16.Should().Be("h");
                resultUtf8.Should().Be("he");
            }
        }

        [TestFixture]
        public class when_trying_to_transform_null_to_utf8_and_limit_result
        {
            [Test]
            public void should_return_null()
            {
                //arrange
                string str = null;

                //act
                var result = str.ToUtf8(4);

                //assert
                result.Should().BeNull();
            }
        }


        [TestFixture]
        public class when_trying_to_transform_null_to_utf8
        {
            [Test]
            public void should_return_null()
            {
                //arrange
                string str = null;

                //act
                var result = str.ToUtf8();

                //assert
                result.Should().BeNull();
            }
        }

        [TestFixture]
        public class when_trying_to_strip_string
        {
            [TestCase("Hello12345", 5 , Result ="Hello" )]
            [TestCase("Hello12345", 15, Result = "Hello12345")]
            [TestCase("", 1, Result = "")]
            [TestCase("Hello12345", 0, Result = "")]
            [TestCase("H", 1, Result = "H")]
            [TestCase("H", -3, Result = "H")]
            public string should_return_valid_result(string str, int limit)
            {                                
                return  str.Strip(limit);
            }
        }

        [TestFixture]
        public class when_trying_to_strip_null_string
        {
            [Test]
            public void should_return_null()
            {                
                //arrange
                string str = null;                
                
                //act
                var result = str.Strip(4);

                //assert
                result.Should().BeNull();
            }
        }

        [TestFixture]
        public class when_trying_to_get_part_of_string_before_first_occurrence_of_character
        {
            [TestCase("nldn123\\dmi15", Result = "nldn123")]
            [TestCase("nldn123\\", Result = "nldn123")]
            [TestCase("\\", Result = "")]
            [TestCase("nldn123\\dmi15\\iop", Result = "nldn123")]
            [TestCase("nldn123", Result = "")]
            [TestCase("", Result = "")]
            [TestCase("\\\\", Result = "")]
            [TestCase(null, Result = null)]
            public string should_part_of_string(string str)
            {                                                
                return str.GetStringBeforeFirstOccurrenceOfChar('\\');                
            }
        }

        [TestFixture]
        public class when_trying_to_get_part_of_string_after_character
        {
            [TestCase("nldn123\\dmi15", Result = "dmi15")]
            [TestCase("nldn123\\", Result = "")]
            [TestCase("nldn123\\dmi15\\iop", Result = "dmi15\\iop")]
            [TestCase("nldn123\\dmi15\\iop\\", Result = "dmi15\\iop\\")]
            [TestCase("\\", Result = "")]
            [TestCase("nldn123", Result = "")]
            [TestCase("\\\\", Result = "\\")]
            [TestCase("", Result = "")]
            [TestCase(null, Result = null)]
            public string should_part_of_string(string str)
            {
                return str.GetStringAfterFirstOccurrenceOfChar('\\');
            }
        }

        [TestFixture]
        public class when_trying_to_split_string
        {
            [Test]
            public void should_split()
            {                
                //act
                var result = "123;qwe".SplitToArray(';').ToArray();

                //assert
                result.Length.Should().Be(2);
                result[0].Should().Be("123");
                result[1].Should().Be("qwe");
            }
        }

        [TestFixture]
        public class when_trying_to_split_string_with_separator_ending
        {
            [Test]
            public void should_split()
            {
                //act
                var result = "123;qwe;".SplitToArray(';').ToArray();

                //assert
                result.Length.Should().Be(2);
                result[0].Should().Be("123");
                result[1].Should().Be("qwe");
            }
        }

        [TestFixture]
        public class when_trying_to_split_empty_string
        {
            [Test]
            public void should_return_empty_array()
            {
                //act
                var result = "".SplitToArray(';').ToArray();

                //assert
                result.Should().BeEmpty();
            }
        }

        [TestFixture]
        public class when_trying_to_split_null_string
        {
            [Test]
            public void should_return_empty_array()
            {
                //act
                var result = ((string)null).SplitToArray(';').ToArray();

                //assert
                result.Should().BeEmpty();
            }
        }

        [TestFixture]
        public class when_trying_to_strip_tags
        {
            [TestCase(" Hello! <b>How are you?<b/> <div>To be or not to be</div> &nbsp;", Result = "Hello! How are you? To be or not to be")]
            [TestCase("&nbsp;Hello! <b>How are you? To be or not to be</div>&nbsp;", Result = "Hello! How are you? To be or not to be")]
            [TestCase("&nbsp;", Result = "")]
            [TestCase("<p>super <b>mario</b></p>", Result = "super mario")]
            [TestCase("", Result = "")]
            [TestCase(null, Result = null)]
            public string should_process_properly(string str)
            {
                return str.StripTags();                
            }
        }

        [TestFixture]
        public class when_trying_to_replace_newlines_with_brs
        {
            [TestCase("Hello!\nHow are you?\rGood?", Result = "Hello!<br/>How are you?<br/>Good?")]
            [TestCase("\n\n", Result = "<br/><br/>")]            
            [TestCase("", Result = "")]
            [TestCase(null, Result = null)]
            public string should_process_properly(string str)
            {
                return str.ReplaceNewLinesWithBrs();
            }
        }

        [TestFixture]
        public class when_trying_to_remove_newlines
        {
            [TestCase("Hello!\nHow are you?\rGood?", Result = "Hello!How are you?Good?")]
            [TestCase("\n\n", Result = "")]
            [TestCase("", Result = "")]
            [TestCase(null, Result = null)]
            public string should_process_properly(string str)
            {
                return str.RemoveNewLines();
            }
        }

        [TestFixture]
        public class when_trying_to_remove_substring
        {
            [TestCase("Hello;How are you", Result = "HelloHow are you")]
            [TestCase(";;", Result = "")]
            [TestCase("", Result = "")]
            [TestCase(null, Result = null)]
            public string should_remove(string str)
            {
                return str.RemoveSubstring(";");
            }
        }

        [TestFixture]
        public class when_trying_to_replace_substring_with
        {
            [TestCase("Hello\nHow are you", "\n", ";", Result = "Hello;How are you")]
            [TestCase("\n\n", "\n", ";", Result = ";;")]
            [TestCase("", "\n", ";", Result = "")]
            [TestCase(null, "\n", ";", Result = null)]
            [TestCase("\n\n", null, ";", Result = "\n\n")]
            [TestCase("\n\n", "", ";", Result = "\n\n")]
            [TestCase("\n\n", "\n", "", Result = "")]
            [TestCase("\n\n", "\n", null, Result = "\n\n")]
            public string should_replace(string str, string substring, string with)
            {
                return str.ReplaceSubstringWith(substring, with);
            }
        }

        [TestFixture]
        public class when_trying_to_replace_brs_with_newlines
        {
            [TestCase("Hello!<br>How are you?<br/>Good?<br /><BR>Yes!<Br/>", Result = "Hello!\nHow are you?\nGood?\n\nYes!\n")]
            [TestCase("<br /><br><br/>", Result = "\n\n\n")]
            [TestCase("", Result = "")]
            [TestCase(null, Result = null)]
            public string should_process_properly(string str)
            {
                return str.ReplaceBrsWithNewLines();
            }
        }

        [TestFixture]
        [Category("Unit")]
        public class WhenTryingToEncodeHtml
        {
            [TestCase("Are<br/>you<br/>dead?<br/>", Result = "Are&lt;br/&gt;you&lt;br/&gt;dead?&lt;br/&gt;")]
            [TestCase("<p><div>What are you doing?</div></p>", Result = "&lt;p&gt;&lt;div&gt;What are you doing?&lt;/div&gt;&lt;/p&gt;")]
            [TestCase("", Result = "")]
            [TestCase(null, Result = null)]
            public string should_return_proper_result(string str)
            {
                return str.HtmlEncode();
            }
        }

        [TestFixture]
        [Category("Unit")]
        public class WhenTryingToDecodeHtml
        {
            [TestCase("Are&lt;br/&gt;you&lt;br/&gt;dead?&lt;br/&gt;", Result = "Are<br/>you<br/>dead?<br/>")]
            [TestCase("<p>&lt;div&gt;What are you doing?&lt;/div&gt;</p>", Result = "<p><div>What are you doing?</div></p>")]
            [TestCase("", Result = "")]
            [TestCase(null, Result = null)]
            public string should_return_proper_result(string str)
            {
                return str.HtmlDecode();
            }
        }

        [TestFixture]
        public class when_trying_to_replace_encoded_brs_with_real_brs
        {
            [TestCase("Hello!&lt;br/&gt;<br/>", Result = "Hello!<br/><br/>")]
            [TestCase("Hello!&LT;Br/&GT;<br/>", Result = "Hello!<br/><br/>")]
            [TestCase("Hello!&lt;BR&gt;<br/>", Result = "Hello!<br/><br/>")]
            [TestCase("", Result = "")]
            [TestCase(null, Result = null)]
            public string should_process_properly(string str)
            {
                return str.ReviveEncodedBrs();
            }
        }

        [TestFixture]
        public class when_trying_to_trim_string
        {
            [TestCase(" Blabla ", Result = "Blabla")]
            [TestCase(" Blabla", Result = "Blabla")]
            [TestCase("Blabla ", Result = "Blabla")]
            [TestCase("\nBla\nbla\n", Result = "Bla\nbla")]
            [TestCase("\nBlabla", Result = "Blabla")]
            [TestCase("Blabla\n", Result = "Blabla")]
            [TestCase(null, Result = null)]
            [TestCase("", Result = "")]
            public string should_trim(string str)
            {
                return str.TrimString();
            }
        }
    }
}

// ReSharper restore InconsistentNaming