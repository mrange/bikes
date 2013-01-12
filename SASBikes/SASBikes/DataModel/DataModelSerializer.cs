using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace SASBikes.DataModel
{
    partial class DataModelSerializer
    {
        public static readonly CultureInfo SerializeCulture = CultureInfo.InvariantCulture;

        public static XElement CreateElement(
            string name,
            params object[] elements
            )
        {
            return new XElement("V" , new XAttribute("name", name ?? "") , elements);
        }

        public static XElement Serialize(this ObservableCollection<string> instance, string name)
        {
            if (instance == null)
            {
                return null;
            }

            return CreateElement(
                    name
                , instance.Select((v, i) => v.Serialize(i.ToString()))
                );

        }

        static XElement CreateTextElement(string name, string value)
        {
            return CreateElement(name, new XText(value ?? ""));
        }

        public static XElement Serialize(this string value, string name)
        {
            return CreateTextElement(name, value);
        }

        public static XElement Serialize(this decimal value, string name)
        {
            return CreateTextElement(name, value.ToString(SerializeCulture));
        }

        public static XElement Serialize(this int value, string name)
        {
            return CreateTextElement(name, value.ToString(SerializeCulture));
        }

        public static XElement Serialize(this bool value, string name)
        {
            return CreateTextElement(name, value.ToString());
        }

    }
}
