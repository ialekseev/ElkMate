// ReSharper disable InconsistentNaming

using System.IO;
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
    }
}

// ReSharper restore InconsistentNaming