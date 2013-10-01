// ReSharper disable InconsistentNaming
using System.Collections.Generic;
using System.Linq;
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
                void DoSomething(IEnumerable<Item> items);
            }
        }
        
        
        [TestFixture]
        public class when_trying_to_apply_and_perform_operations: BaseOnSpec
        {                                    
            
            [Test]
            public void should_return_valid_result()
            {
                //arrange
                var someObject = A.Fake<IClass>();

                //act
                var result =
                    On<Item>.Items(() => new List<Item>() {new Item() {A = "1", B = "1"}, new Item() {A = "2", B = "2"}})
                            .Apply(t =>
                                {
                                    t.A = "3";
                                    t.B = "4";
                                }).
                             PerformAfterApply(someObject.DoSomething).
                             Execute();
                   
                //assert
                result.Count.Should().Be(2);
                result[0].A.Should().Be("3");
                result[0].B.Should().Be("4");
                result[1].A.Should().Be("3");
                result[1].B.Should().Be("4");

                A.CallTo(() => someObject.DoSomething(A<IEnumerable<Item>>.Ignored)).MustHaveHappened();
            }
        }
       
        [TestFixture]
        public class when_trying_to_perform_two_times: BaseOnSpec
        {            
            [Test]
            public void should_perform_two_times()
            {
                //arrange
                var someObject = A.Fake<IClass>();

                //act
                var result =
                    On<Item>.Items(() => new List<Item>() {new Item() {A = "1", B = "1"}, new Item() {A = "2", B = "2"}})
                            .
                             PerformBeforeApply(someObject.DoSomething).
                             PerformBeforeApply(someObject.DoSomething).
                             Execute();
                   
                //assert
                result.Count.Should().Be(2);
                result[0].A.Should().Be("1");
                result[0].B.Should().Be("1");
                result[1].A.Should().Be("2");
                result[1].B.Should().Be("2");

                A.CallTo(() => someObject.DoSomething(A<IEnumerable<Item>>.Ignored)).MustHaveHappened(Repeated.Exactly.Twice);
            }
        }

        [TestFixture]
        public class when_trying_to_apply_multiple_times: BaseOnSpec
        {            
            [Test]
            public void should_return_valid_result()
            {
                //arrange
                var someObject = A.Fake<IClass>();

                //act
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
                             PerformAfterApply(someObject.DoSomething).
                             Execute();                   

                //assert
                result.Count.Should().Be(2);
                result[0].A.Should().Be("3");
                result[0].B.Should().Be("4");
                result[1].A.Should().Be("3");
                result[1].B.Should().Be("4");

                A.CallTo(() => someObject.DoSomething(A<IEnumerable<Item>>.Ignored)).MustHaveHappened();
            }
        }

        [TestFixture]
        public class when_trying_to_grave_apply_and_execute_without_force_option : BaseOnSpec
        {
            [Test]
            public void should_not_apply()
            {                
                //arrange
                //act
                var result =
                    On<Item>.Items(() => new List<Item>() { new Item() { A = "1", B = "1" }, new Item() { A = "2", B = "2" } })
                            .Apply(t =>
                            {
                                t.A = "3";
                            })
                            .ApplyGrave(t =>
                            {
                                t.B = "4";
                            }).                             
                             Execute(false);

                //assert
                result.Count.Should().Be(2);
                result[0].A.Should().Be("3");
                result[0].B.Should().Be("1");
                result[1].A.Should().Be("3");
                result[1].B.Should().Be("2");                
            }
        }

        [TestFixture]
        public class when_trying_to_grave_apply_and_execute_with_force_option : BaseOnSpec
        {
            [Test]
            public void should_apply()
            {
                //arrange
                //act
                var result =
                    On<Item>.Items(() => new List<Item>() { new Item() { A = "1", B = "1" }, new Item() { A = "2", B = "2" } })
                            .Apply(t =>
                            {
                                t.A = "3";
                            })
                            .ApplyGrave(t =>
                            {
                                t.B = "4";
                            }).
                             Execute(true);

                //assert
                result.Count.Should().Be(2);
                result[0].A.Should().Be("3");
                result[0].B.Should().Be("4");
                result[1].A.Should().Be("3");
                result[1].B.Should().Be("4");
            }
        }

        [TestFixture]
        public class when_trying_to_perform_passing_function_with_items_argument : BaseOnSpec
        {
            [Test]
            public void should_call_function_with_items_argument()
            {
                //arrange
                var someObject = A.Fake<IClass>();
                
                //act
                var result =
                    On<Item>.Items(() => new List<Item>() { new Item() { A = "1", B = "1" }, new Item() { A = "2", B = "2" } }).
                    PerformBeforeApply(t => someObject.DoSomething(t)).
                    PerformAfterApply(t => someObject.DoSomething(t)).                            
                    Execute(true);

                //assert
                result.Count.Should().Be(2);
                result[0].A.Should().Be("1");
                result[0].B.Should().Be("1");
                result[1].A.Should().Be("2");
                result[1].B.Should().Be("2");

                A.CallTo(() => someObject.DoSomething(A<IEnumerable<Item>>.That.Matches(x => x.Count()==2 && x.First().A=="1" && x.First().B=="1" && x.Last().A=="2" && x.Last().B=="2"))).MustHaveHappened(Repeated.Exactly.Twice);
            }
        }

    }
}

// ReSharper restore InconsistentNaming