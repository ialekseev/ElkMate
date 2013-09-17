// ReSharper disable InconsistentNaming

using FluentAssertions;
using NUnit.Framework;
using SmartElk.ElkMate.Common.Console;

namespace SmartElk.ElkMate.Common.Specs
{
    public class ArgumentsParserSpecs
    {
        [TestFixture]
        public class when_trying_to_parse_valid_arguments
        {
            [Test]
            public void should_retrun_parsed_arguments()
            {
                var args = new[] { "-size=100", "-height:'400'", "--debug", "-release", "width:"};
                var argumentsParser = new ArgumentsParser(args);                
                argumentsParser["size"].Should().Be("100");
                argumentsParser["height"].Should().Be("400");
                argumentsParser["debug"].Should().Be("true");
                argumentsParser["release"].Should().Be("true");
                argumentsParser["width"].Should().BeNull();
            }
        }

        [TestFixture]
        public class when_trying_to_parse_invalid_arguments
        {
            [Test]
            public void should_not_return_any_arguments()
            {
                var args = new[] { "size=100" };
                var argumentsParser = new ArgumentsParser(args);

                argumentsParser["size"].Should().BeNull();
                argumentsParser["debug"].Should().BeNull();              
                argumentsParser["bad"].Should().BeNull();
            }
        }
    }
}
// ReSharper restore InconsistentNaming
