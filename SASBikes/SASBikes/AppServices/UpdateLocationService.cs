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
using Windows.Devices.Geolocation;

namespace SASBikes.AppServices
{
    sealed class UpdateLocationService : IService
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
                // TODO: Log
            }
        }

        void Async_UpdatePosition(Geoposition pos)
        {
            var coordinate = pos.Coordinate;

            var lo = coordinate.Longitude;
            var la = coordinate.Latitude;

            App.Value.Async_Invoke(
                App.AsyncGroup.UpdateStatePosition,
                () => UpdateCoordinate(lo, la)
                );
        }

        void UpdateCoordinate(double lo, double la)
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