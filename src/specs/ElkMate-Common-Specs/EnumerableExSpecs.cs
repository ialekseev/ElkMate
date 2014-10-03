// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.Linq;
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
                //arrange
                var list = new [] {1, 2, 3, 4};
                //act
                var result = list.JoinToString();
                //assert
                result.Should().Be("1, 2, 3, 4");
            }
        }

        [TestFixture]
        public class when_trying_to_join_string_list_to_string_with_custom_separator
        {
            [Test]
            public void should_join_to_string()
            {
                //arrange
                var list = new[] { "4", "3", "2", "1" };
                //act
                var result = list.JoinToString("+");
                //assert
                result.Should().Be("4+3+2+1");
            }
        }

        [TestFixture]
        public class when_trying_to_join_string_list_containing_null_values_with_default_separator
        {
            [Test]
            public void should_join_to_string()
            {
                //arrange
                var list = new[] { "4", null, "2", "3" };
                //act
                var result = list.JoinToString();
                //assert
                result.Should().Be("4, 2, 3");
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
                //arrange
                var list = new[] { new Composite("Ivan"), null, new Composite("John"), new Composite(null) };
                //act
                var result = list.JoinToString(t=>t.Name);
                //assert
                result.Should().Be("Ivan, John");
            }
        }
       
        [TestFixture]
        public class when_trying_to_join_null_list_to_string
        {
            [Test]            
            public void should_return_empty_string()
            {
                //arrange
                IEnumerable<string> list = null;
                //act
                var result = list.JoinToString();
                //assert
                result.Should().BeEmpty();
            }
        }

        [TestFixture]
        public class when_trying_to_join_empty_list_to_string
        {
            [Test]
            public void should_return_empty_string()
            {
                //arrange
                IEnumerable<string> list = null;
                //act
                var result = list.JoinToString();
                //assert
                result.Should().BeEmpty();
            }
        }

        [TestFixture]
        public class when_trying_to_replace_existing_item_in_enumerable
        {
            protected class TestClass
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }
            
            [Test]
            public void should_found_and_replace_item()
            {
                //arrange
                var list = new List<TestClass>() { new TestClass() { Id = 5, Name = "5" }, new TestClass() { Id = 3, Name = "3" }, new TestClass() { Id = 6, Name = "6" } };

                var newItem = new TestClass() {Id = 3, Name = "new 3"};

                //act
                var result = list.ReplaceItems(t=>t.Id, t => t.Id == 3, newItem).ToList();

                //assert
                result.Count().Should().Be(3);
                
                result[0].Id.Should().Be(5);
                result[0].Name.Should().Be("5");

                result[1].Id.Should().Be(3);
                result[1].Name.Should().Be("new 3");

                result[2].Id.Should().Be(6);
                result[2].Name.Should().Be("6");
            }
        }

        [TestFixture]
        public class when_trying_to_replace_non_existing_item_in_enumerable
        {
            protected class TestClass
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }

            [Test]
            public void should_replace_nothing()
            {
                //arrange
                var list = new List<TestClass>() { new TestClass() { Id = 5, Name = "5" }, new TestClass() { Id = 3, Name = "3" }, new TestClass() { Id = 6, Name = "6" } };

                var newItem = new TestClass() { Id = 12, Name = "new 12" };

                //act
                var result = list.ReplaceItems(t=>t.Id, t => t.Id == 12, newItem).ToList();

                //assert
                result.Count().Should().Be(3);
                
                result[0].Id.Should().Be(5);
                result[0].Name.Should().Be("5");

                result[1].Id.Should().Be(3);
                result[1].Name.Should().Be("3");

                result[2].Id.Should().Be(6);
                result[2].Name.Should().Be("6");
            }
        }

        [TestFixture]
        public class when_trying_to_replace_multiple_items_in_enumerable
        {
            protected class TestClass
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }

            [Test]
            public void should_found_and_replace_items()
            {
                //arrange
                var list = new List<TestClass>() { new TestClass() { Id = 3, Name = "5" }, new TestClass() { Id = 3, Name = "3" }, new TestClass() { Id = 6, Name = "6" } };

                var newItem = new TestClass() { Id = 3, Name = "new" };

                //act
                var result = list.ReplaceItems(t => t.Id, t => t.Id == 3, newItem).ToList();

                //assert
                result.Count().Should().Be(3);

                result[0].Id.Should().Be(3);
                result[0].Name.Should().Be("new");

                result[1].Id.Should().Be(3);
                result[1].Name.Should().Be("new");

                result[2].Id.Should().Be(6);
                result[2].Name.Should().Be("6");
            }
        }

        [TestFixture]
        public class when_trying_to_merge_two_lists
        {
            protected class TestClass
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }

            [Test]
            public void should_return_merged_list()
            {
                //arrange
                IEnumerable<TestClass> list1 = new List<TestClass>()
                    {
                        new TestClass() {Id = 4, Name = "Super"},
                        new TestClass() {Id = 5, Name = "Great"},
                        new TestClass() {Id = 7, Name = "Awesome"}
                    };

                IEnumerable<TestClass> list2 = new List<TestClass>()
                    {
                        new TestClass() {Id = 4, Name = "Super1"},
                        new TestClass() {Id = 5, Name = "Great"},
                        new TestClass() {Id = 6, Name = "Amazing"}
                    };

                //act
                var result = list1.Merge(list2, t => t.Id).ToArray();
                
                //assert
                result.Length.Should().Be(4);
                result[0].Id.Should().Be(4);
                result[0].Name.Should().Be("Super1");
                result[1].Id.Should().Be(5);
                result[1].Name.Should().Be("Great");
                result[2].Id.Should().Be(7);
                result[2].Name.Should().Be("Awesome");
                result[3].Id.Should().Be(6);
                result[3].Name.Should().Be("Amazing");
            }
        }

        [TestFixture]
        public class when_trying_to_merge_list_with_null_list
        {
            protected class TestClass
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }

            [Test]
            public void should_return_first_list()
            {
                //arrange
                IEnumerable<TestClass> list1 = new List<TestClass>()
                    {
                        new TestClass() {Id = 4, Name = "Super"},
                        new TestClass() {Id = 5, Name = "Great"},
                        new TestClass() {Id = 7, Name = "Awesome"}
                    };

                IEnumerable<TestClass> list2 = null;

                //act
                var result = list1.Merge(list2, t => t.Id).ToArray();

                //assert
                result.Length.Should().Be(3);
                result[0].Id.Should().Be(4);
                result[0].Name.Should().Be("Super");
                result[1].Id.Should().Be(5);
                result[1].Name.Should().Be("Great");
                result[2].Id.Should().Be(7);
                result[2].Name.Should().Be("Awesome");                
            }
        }

        [TestFixture]
        public class when_trying_to_merge_null_list_with_list
        {
            protected class TestClass
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }

            [Test]
            public void should_return_second_list()
            {
                //arrange
                IEnumerable<TestClass> list1 = null;
                IEnumerable<TestClass> list2 = new List<TestClass>()
                    {
                        new TestClass() {Id = 4, Name = "Super"},
                        new TestClass() {Id = 5, Name = "Great"},
                        new TestClass() {Id = 7, Name = "Awesome"}
                    };
                
                //act
                var result = list1.Merge(list2, t => t.Id).ToArray();

                //assert
                result.Length.Should().Be(3);
                result[0].Id.Should().Be(4);
                result[0].Name.Should().Be("Super");
                result[1].Id.Should().Be(5);
                result[1].Name.Should().Be("Great");
                result[2].Id.Should().Be(7);
                result[2].Name.Should().Be("Awesome");
            }
        }

        [TestFixture]
        public class when_trying_to_merge_list_with_empty_list
        {
            protected class TestClass
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }

            [Test]
            public void should_return_first_list()
            {
                //arrange
                IEnumerable<TestClass> list1 = new List<TestClass>()
                    {
                        new TestClass() {Id = 4, Name = "Super"},
                        new TestClass() {Id = 5, Name = "Great"},
                        new TestClass() {Id = 7, Name = "Awesome"}
                    };

                IEnumerable<TestClass> list2 = new List<TestClass>();

                //act
                var result = list1.Merge(list2, t => t.Id).ToArray();

                //assert
                result.Length.Should().Be(3);
                result[0].Id.Should().Be(4);
                result[0].Name.Should().Be("Super");
                result[1].Id.Should().Be(5);
                result[1].Name.Should().Be("Great");
                result[2].Id.Should().Be(7);
                result[2].Name.Should().Be("Awesome");
            }
        }

        [TestFixture]
        public class when_trying_to_merge_null_list_with_null_list
        {
            protected class TestClass
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }

            [Test]
            public void should_return_null()
            {
                //arrange
                IEnumerable<TestClass> list1 = null;

                IEnumerable<TestClass> list2 = null;

                //act
                var result = list1.Merge(list2, t => t.Id);

                //assert
                result.Should().BeNull();
            }
        }

        [TestFixture]
        public class when_trying_to_apply_function_on_list
        {
            private class CompositeObject
            {
                public string Value { get; set; }
            }

            [Test]
            public void should_apply()
            {
                //arrange
                var list = new List<CompositeObject>()
                    {
                        new CompositeObject() {Value = " 123 "},
                        new CompositeObject() {Value = " 456 "}
                    };

                //act
                var result = list.Apply(t => t.Value = t.Value.Trim());

                //assert
                result.Count.Should().Be(2);
                result[0].Value.Should().Be("123");
                result[1].Value.Should().Be("456");
            }
        }    
    }
}
// ReSharper restore InconsistentNaming