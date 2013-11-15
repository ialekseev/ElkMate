// ReSharper disable InconsistentNaming
using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using SmartElk.ElkMate.Common.Ex;


namespace SmartElk.ElkMate.Common.Specs
{
    public class XDocumentExSpecs
    {
        [TestFixture]
        public class when_trying_to_get_elements_from_null_element
        {
            [Test]            
            public void should_return_empty_list()
            {
                //arrange
                XElement xElement = null;
                //act
                var result = xElement.ElementsOrEmpty();
                //assert
                result.Should().BeEmpty();
            }
        }

        [TestFixture]
        public class when_trying_to_get_elements_from_element
        {
            [Test]
            public void should_return_list_of_elements()
            {
                //arrange
                var xElement = new XElement("Root",
                                            new XElement("Element1", 1),
                                            new XElement("Element2", 2),
                                            new XElement("Element3", 3));                                                 
                //act
                var result = xElement.ElementsOrEmpty().ToArray();
                //assert
                result.Length.Should().Be(3);
                result[0].Name.LocalName.Should().Be("Element1");
                result[0].Value.Should().Be("1");
                result[1].Name.LocalName.Should().Be("Element2");
                result[1].Value.Should().Be("2");
                result[2].Name.LocalName.Should().Be("Element3");
                result[2].Value.Should().Be("3");
            }
        }
        
        [TestFixture]
        public class when_trying_to_get_elements_by_name_from_null_element
        {
            [Test]
            public void should_return_empty_list()
            {
                //arrange
                XElement xElement = null;
                //act
                var result = xElement.ElementsOrEmpty("elem");
                //assert
                result.Should().BeEmpty();
            }
        }

        [TestFixture]
        public class when_trying_to_get_elements_by_name_from_element
        {
            [Test]
            public void should_return_list_of_elements()
            {
                //arrange
                var xElement = new XElement("Root",
                                            new XElement("Element1", 1),
                                            new XElement("Element2", 2),
                                            new XElement("Element3", 3));
                //act
                var result = xElement.ElementsOrEmpty("Element2").ToArray();
                //assert
                result.Length.Should().Be(1);
                result[0].Name.LocalName.Should().Be("Element2");
                result[0].Value.Should().Be("2");
            }
        }

        [TestFixture]
        public class when_trying_to_get_element_from_null_element
        {
            [Test]
            public void should_return_null()
            {
                //arrange
                XElement xElement = null;
                //act
                var result = xElement.ElementOrDefault("next");
                //assert
                result.Should().BeNull();
            }
        }

        [TestFixture]
        public class when_trying_to_get_element_from_element
        {
            [Test]
            public void should_return_element()
            {
                //arrange
                var xElement = new XElement("Root",
                                            new XElement("Element1", 1),
                                            new XElement("Element2", 2),
                                            new XElement("Element3", 3));   
                //act
                var result = xElement.Element("Element2");
                //assert
                result.Name.LocalName.Should().Be("Element2");
                result.Value.Should().Be("2");
            }
        }

        [TestFixture]
        public class when_trying_to_get_value_from_null_element
        {
            [Test]
            public void should_return_null()
            {
                //arrange
                XElement xElement = null;
                //act
                var result = xElement.ValueOrDefault();
                //assert
                result.Should().BeNull();
            }
        }

        [TestFixture]
        public class when_trying_to_get_value_from_element
        {
            [Test]
            public void should_return_value()
            {
                //arrange
                var xElement = new XElement("elem", "val");
                //act
                var result = xElement.Value;
                //assert
                result.Should().Be("val");
            }
        }

        [TestFixture]
        public class when_trying_to_get_attribute_value_from_null_element
        {
            [Test]
            public void should_return_null()
            {
                //arrange
                XElement xElement = null;
                //act
                var result = xElement.AttributeValueOrDefault("att");
                //assert
                result.Should().BeNull();
            }
        }

        [TestFixture]
        public class when_trying_to_get_attribute_value_from_null_attribute
        {
            [Test]
            public void should_return_null()
            {
                //arrange
                var xElement = new XElement("elem", "val");                
                //act
                var result = xElement.AttributeValueOrDefault("att");
                //assert
                result.Should().BeNull();
            }
        }

        [TestFixture]
        public class when_trying_to_get_attribute_value
        {
            [Test]
            public void should_return_value()
            {
                //arrange
                var xElement = new XElement("elem", "val");
                xElement.SetAttributeValue("attr", "1");
                //act
                var result = xElement.AttributeValueOrDefault("attr");
                //assert
                result.Should().Be("1");
            }
        }
    }
}
// ReSharper restore InconsistentNaming