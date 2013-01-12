using System.Xml.Linq;

namespace SASBikes
{
    static class Extensions
    {
        public static string GetAttributeValue(
            this XElement element,
            XName name,
            string defaultValue
            )
        {
            if (element == null)
            {
                return defaultValue;
            }

            if (name == null)
            {
                return defaultValue;
            }

            var attribute = element.Attribute(name);

            if (attribute == null)
            {
                return defaultValue;
            }

            return attribute.Value ?? defaultValue;
        }
        
    }
}