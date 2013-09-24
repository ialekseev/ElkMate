// ReSharper disable InconsistentNaming
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace SmartElk.ElkMate.Common.Specs
{
    public class OnSpecs
    {        
        public class BaseOnSpec
        {
            public class Item
            {
                public string A { get; set; }
                public string B { get; set; }
            }

            public interface IClass
            {
                void DoSomething();
            }
        }
        
        
        [TestFixture]
        public class when_trying_to_apply_and_perform_operations: BaseOnSpec
        {                                    
            
            [Test]
            public void should_return_valid_result()
            {
                var someObject = A.Fake<IClass>();

                var result =
                    On<Item>.Items(() => new List<Item>() {new Item() {A = "1", B = "1"}, new Item() {A = "2", B = "2"}})
                            .Apply(t =>
                                {
                                    t.A = "3";
                                    t.B = "4";
                                }).
                             Perform(someObject.DoSomething).
                             Execute();
                   

                result.Count.Should().Be(2);
                result[0].A.Should().Be("3");
                result[0].B.Should().Be("4");
                result[1].A.Should().Be("3");
                result[1].B.Should().Be("4");

                A.CallTo(() => someObject.DoSomething()).MustHaveHappened();
            }
        }
       
        [TestFixture]
        public class when_trying_to_perform_two_times: BaseOnSpec
        {            
            [Test]
            public void should_perform_two_times()
            {
                var someObject = A.Fake<IClass>();

                var result =
                    On<Item>.Items(() => new List<Item>() {new Item() {A = "1", B = "1"}, new Item() {A = "2", B = "2"}})
                            .
                             Perform(someObject.DoSomething).
                             Perform(someObject.DoSomething).
                             Execute();
                   

                result.Count.Should().Be(2);
                result[0].A.Should().Be("1");
                result[0].B.Should().Be("1");
                result[1].A.Should().Be("2");
                result[1].B.Should().Be("2");   

                A.CallTo(() => someObject.DoSomething()).MustHaveHappened(Repeated.Exactly.Twice);
            }
        }

        [TestFixture]
        public class when_trying_to_apply_multiple_times: BaseOnSpec
        {            
            [Test]
            public void should_return_valid_result()
            {
                var someObject = A.Fake<IClass>();

                var result =
                    On<Item>.Items(() => new List<Item>() {new Item() {A = "1", B = "1"}, new Item() {A = "2", B = "2"}})
                            .Apply(t =>
                                {
                                    t.A = "3";
                                })
                            .Apply(t =>
                                {
                                    t.B = "4";
                                }).
                             Perform(someObject.DoSomething).
                             Execute();                   

                result.Count.Should().Be(2);
                result[0].A.Should().Be("3");
                result[0].B.Should().Be("4");
                result[1].A.Should().Be("3");
                result[1].B.Should().Be("4");

                A.CallTo(() => someObject.DoSomething()).MustHaveHappened();
            }
        }
    }
}

// ReSharper restore InconsistentNaming