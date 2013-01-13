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

using System;
using System.Xml.Linq;
using Bing.Maps;

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
        
        public static double DistanceTo(this Location location, Location otherLocation)
        {
            var lo = Location.NormalizeLongitude(location.Longitude) - Location.NormalizeLongitude(otherLocation.Longitude);
            var la = location.Latitude - otherLocation.Latitude;
            return Math.Sqrt(lo*lo + la*la);
        }
    }
}