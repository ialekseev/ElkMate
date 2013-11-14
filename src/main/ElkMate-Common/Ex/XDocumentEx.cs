using System.Collections.Generic;
using System.Xml.Linq;

namespace SmartElk.ElkMate.Common.Ex
{
    public static class XDocumentEx
    {
        public static IEnumerable<XElement> ElementsOrEmpty(this XElement element)
        {
            return element == null ? new XElement[0] : element.Elements();
        }

        public static XElement ElementOrDefault(this XElement element, XName name)
        {
            return element == null ? null : element.Element(name);
        }

        public static string ValueOrDefault(this XElement element)
        {
            return element == null ? null : element.Value;
        }

        public static string AttributeValueOrDefault(this XElement element, XName name)
        {
            if (element == null)
                return null;

            var attribute = element.Attribute(name);
            return attribute != null ? attribute.Value : null;
        }
    }
}
