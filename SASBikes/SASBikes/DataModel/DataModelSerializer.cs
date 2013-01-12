using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using SASBikes.Source.Extensions;

namespace SASBikes.DataModel
{
    partial class DataModelSerializer
    {
        public static readonly CultureInfo SerializeCulture = CultureInfo.InvariantCulture;

        public static readonly XName NodeName = "V";
        public static readonly XName NameAttributeName = "n";

        public static XElement CreateElement(
            string name,
            params object[] elements
            )
        {
            return new XElement(NodeName , new XAttribute(NameAttributeName, name ?? "") , elements);
        }

        static XElement CreateTextElement(string name, string value)
        {
            return CreateElement(name, new XText(value ?? ""));
        }

        public static XElement Serialize(this DataModelCollection<string> instance, string name)
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

        public static XElement Serialize(this string value, string name)
        {
            return CreateTextElement(name, value);
        }

        public static void Unserialize (
                this XElement element
            ,   DataModelContext context
            ,   IUnserializeErrorReporter reporter
            ,   ref DataModelCollection<string> instance
            )
        {
            instance = new DataModelCollection<string> (context);

            if (element == null)
            {
                return;
            }

            foreach (var subElement in element.Elements (NodeName))
            {
                instance.Add (subElement.Value);                                
            }
        }

        public static void Unserialize(
            this XElement element
            , DataModelContext context
            , IUnserializeErrorReporter reporter
            , ref string instance
            )
        {
            if (element == null)
            {
                return;
            }

            instance = element.Value;
        }

        public static XElement Serialize(this decimal value, string name)
        {
            return CreateTextElement(name, value.ToString(SerializeCulture));
        }

        public static void Unserialize(
            this XElement element
            , DataModelContext context
            , IUnserializeErrorReporter reporter
            , ref decimal instance
            )
        {
            if (element == null)
            {
                return;
            }

            instance = element.Value.Parse(0M);
        }

        public static XElement Serialize(this int value, string name)
        {
            return CreateTextElement(name, value.ToString(SerializeCulture));
        }

        public static void Unserialize(
            this XElement element
            , DataModelContext context
            , IUnserializeErrorReporter reporter
            , ref int instance
            )
        {
            if (element == null)
            {
                return;
            }

            instance = element.Value.Parse(0);
        }

        public static XElement Serialize(this bool value, string name)
        {
            return CreateTextElement(name, value.ToString());
        }

        public static void Unserialize(
            this XElement element
            , DataModelContext context
            , IUnserializeErrorReporter reporter
            , ref bool instance
            )
        {
            if (element == null)
            {
                return;
            }

            instance = element.Value.Parse(false);
        }


    
    }
}
