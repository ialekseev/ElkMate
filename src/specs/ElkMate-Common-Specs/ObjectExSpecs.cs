// ReSharper disable InconsistentNaming

using FluentAssertions;
using NUnit.Framework;
using SmartElk.ElkMate.Common.Ex;

namespace SmartElk.ElkMate.Common.Specs
{
    public class ObjectExSpecs
    {
        [TestFixture]
        public class when_trying_to_apply_function_on_object
        {
            private class CompositeObject
            {
                public string Value { get; set; }
            }
            
            [Test]            
            public void should_apply()
            {                                
                //arrange
                var obj = new CompositeObject(){Value = " 123456 "};
                
                //act
                var result = obj.Apply(t => t.Value = t.Value.Trim());
                                
                //assert
                result.Value.Should().Be("123456");
            }
        }                
    }
}
// ReSharper restore InconsistentNaming