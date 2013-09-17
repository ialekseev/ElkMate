// ReSharper disable InconsistentNaming

using NUnit.Framework;
using SmartElk.ElkMate.Common.Ex;

namespace SmartElk.ElkMate.Common.Specs
{
    public class StringExSpecs
    {
        public class when_trying_to_combine_paths
        {
            [Test]
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
    }
}

// ReSharper restore InconsistentNaming