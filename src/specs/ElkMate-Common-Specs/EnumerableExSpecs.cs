// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using SmartElk.ElkMate.Common.Ex;

namespace SmartElk.ElkMate.Common.Specs
{
    public class EnumerableExSpecs
    {
        [TestFixture]
        public class when_trying_to_join_int_list_to_string_with_default_separator
        {
            [Test]            
            public void should_join_to_string()
            {
                var list = new [] {1, 2, 3, 4};

                var result = list.JoinToString();
                result.Should().Be("1, 2, 3, 4");
            }
        }

        [TestFixture]
        public class when_trying_to_join_string_list_to_string_with_custom_separator
        {
            [Test]
            public void should_join_to_string()
            {
                var list = new[] { "4", "3", "2", "1" };

                var result = list.JoinToString("+");
                result.Should().Be("4+3+2+1");
            }
        }

        [TestFixture]
        public class when_trying_to_join_composite_list_to_string_with_default_separator
        {
            private class Composite
            {
                public string Name { get; set; }
                public Composite(string name)
                {
                    Name = name;
                }
            }
            
            [Test]
            public void should_join_to_string()
            {
                var list = new[] { new Composite("Ivan"), new Composite("John") };

                var result = list.JoinToString(t=>t.Name);
                result.Should().Be("Ivan, John");
            }
        }

        [TestFixture]
        public class when_trying_to_join_null_list_to_string
        {
            [Test]            
            public void should_return_empty_string()
            {
                IEnumerable<string> list = null;

                var result = list.JoinToString();
                result.Should().BeEmpty();
            }
        }

        [TestFixture]
        public class when_trying_to_join_empty_list_to_string
        {
            [Test]
            public void should_return_empty_string()
            {
                IEnumerable<string> list = null;

                var result = list.JoinToString();
                result.Should().BeEmpty();
            }
        }
    }
}
// ReSharper restore InconsistentNaming