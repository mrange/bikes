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
using SASBikes.Source.Common;
using Windows.Devices.Geolocation;

namespace SASBikes.AppServices
{
    sealed class LocatorService : IService
    {
        Geolocator m_locator;

        public void Start()
        {
            m_locator = new Geolocator();
            m_locator.PositionChanged += PositionChanged_Locator;
        }

        void PositionChanged_Locator(Geolocator sender, PositionChangedEventArgs args)
        {
            try
            {
                var pos = args.Position;
                Async_UpdatePosition(pos);
            }
            catch (Exception exc)
            {
                Log.Exception ("Failed to update my position", exc);
            }
        }

        void Async_UpdatePosition(Geoposition pos)
        {
            var coordinate = pos.Coordinate;

            var lo = coordinate.Longitude;
            var la = coordinate.Latitude;

            App.Value.Async_Invoke(
                App.AsyncGroup.LocatorService_UpdateMyPosition,
                () => LocatorService_UpdateMyPosition(lo, la)
                );
        }

        void LocatorService_UpdateMyPosition(double lo, double la)
        {
            var appState = App.Value.AppState;
            appState.State_MyLo = lo;
            appState.State_MyLa = la;
        }

        public void Stop()
        {
            if (m_locator != null)
            {
                m_locator.PositionChanged -= PositionChanged_Locator;
                m_locator = null;
            }

        }

        
    }
}