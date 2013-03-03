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

// ReSharper disable InconsistentNaming

using System;
using SASBikes.Common.Source.Common;
using Windows.Devices.Geolocation;

namespace SASBikes.Common.AppServices
{
    public sealed class LocatorService : IService
    {
        Geolocator m_locator;
        Geoposition m_lastKnownPosition;

        public void Start()
        {
            m_locator = new Geolocator();
            m_locator.PositionChanged += PositionChanged_Locator;
        }

        void PositionChanged_Locator(Geolocator sender, PositionChangedEventArgs args)
        {
            try
            {
                m_lastKnownPosition = args.Position;
                Services.App.Async_Invoke(
                    AsyncGroup.LocatorService_UpdateMyPosition,
                    LocatorService_UpdateMyPosition
                    );
            }
            catch (Exception exc)
            {
                Log.Exception ("Failed to update my position", exc);
            }
        }

        void LocatorService_UpdateMyPosition()
        {
            var state = Services.App.State;
            var lastKnownPosition = m_lastKnownPosition;
            if (lastKnownPosition != null && state != null)
            {
                state.State_MyLa = m_lastKnownPosition.Coordinate.Latitude;
                state.State_MyLo = m_lastKnownPosition.Coordinate.Longitude;
            }
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