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

namespace SASBikes.Common
{
    public struct GeoCoordinate
    {
        public double La;
        public double Lo;

        public GeoCoordinate(double la, double lo)
        {
            La = la;
            Lo = lo;
        }

        public double NormalizedLa
        {
            get
            {
                var laf = La / 180.0;
                var t = Math.Truncate(laf);
              
                return (laf - t) * 180.0;
                
            }
        }
    
        public double NormalizedLo
        {
            get
            {
                var lof = Lo / 180.0;
                var t = Math.Truncate(lof);
              
                return (lof - t) * 180.0;
                
            }
        }

    }
}