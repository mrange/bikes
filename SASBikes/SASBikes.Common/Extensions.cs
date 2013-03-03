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
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SASBikes.Common.DataModel;

namespace SASBikes.Common
{
    public static class Extensions
    {
        const double EarthMeanRadius = 6371009;

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

        public static double Asin(this double d)
        {
            return Math.Asin(d);
        }

        public static double Sqrt(this double d)
        {
            return Math.Sqrt(d);
        }

        public static double Cos(this double d)
        {
            return Math.Cos(d);
        }

        public static double Haversine(this double d)
        {
            var x = Math.Sin(d/2);
            return x*x;
        }

        public static double DistanceTo(this GeoCoordinate location, GeoCoordinate otherLocation)
        {
            var slo = location.NormalizedLo;
            var flo = otherLocation.NormalizedLo;
            var dlo = slo - flo;
            var dla = location.NormalizedLa - otherLocation.NormalizedLa;

            // http://en.wikipedia.org/wiki/Great_circle_distance

            return EarthMeanRadius*2*(dlo.Haversine() + slo.Cos()*flo.Cos()*dla.Haversine());
        }

        public static IEnumerable<Station> NearestOpenStations (this IEnumerable<Station> stations)
        {
            if (stations == null)
            {
                return new Station[0];
            }

            return stations
                .Where (s => s.Station_IsOpen)
                .OrderBy (s => s.Station_Distance)
                ;
        }
    }
}