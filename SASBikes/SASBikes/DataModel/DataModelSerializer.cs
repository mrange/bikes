// ----------------------------------------------------------------------------------------------
// Copyright (c) Mårten Rånge.
// ----------------------------------------------------------------------------------------------
// This source code is subject to terms and conditions of the Microsoft Public License. A 
// copy of the license can be found in the License.html file at the root of this distribution. 
// If you cannot locate the  Microsoft Public License, please send an email to 
// dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
//  by the terms of the Microsoft Public License.
// ----------------------------------------------------------------------------------------------
// You must not remove this notice, or any other, from this software.
// ----------------------------------------------------------------------------------------------

using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SASBikes.Source.Extensions;

namespace SASBikes.DataModel
{
    partial class DataModelSerializer
    {
        public static readonly CultureInfo SerializeCulture = CultureInfo.InvariantCulture;

        public static readonly XName    NodeName           = "N";
        public static readonly XName    NameAttributeName  = "n";
        public static readonly string   RootName           = "Root";

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

        public static XElement Serialize(this double value, string name)
        {
            return CreateTextElement(name, value.ToString(SerializeCulture));
        }

        public static void Unserialize(
            this XElement element
            , DataModelContext context
            , IUnserializeErrorReporter reporter
            , ref double instance
            )
        {
            if (element == null)
            {
                return;
            }

            instance = element.Value.Parse(SerializeCulture, 0.0);
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

            instance = element.Value.Parse(SerializeCulture, 0);
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

            instance = element.Value.Parse(SerializeCulture, false);
        }


        public static string SerializeToString(this State state)
        {
            var doc = new XDocument(state.Serialize(RootName));

            using (var sw = new StringWriter())
            {
                doc.Save(sw);
                return sw.ToString();
            }
        }

        public static State UnserializeFromString(this string value)
        {
            var doc = XDocument.Parse(value ?? "");

            var context = new DataModelContext
                              {
                                  IsSuppressingEvents = true
                              };

            State state = null;
            doc
                .Document
                .Elements(NodeName)
                .FirstOrDefault()
                .Unserialize(context, null, ref state)
                ;

            context.IsSuppressingEvents = false;

            return state;
        }

    }
}
