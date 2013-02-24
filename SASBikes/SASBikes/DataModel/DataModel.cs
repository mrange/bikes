﻿// ----------------------------------------------------------------------------------------------
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

using Bing.Maps;

namespace SASBikes.DataModel
{
    partial class State
    {
        partial void Changed__State_MyLa(double oldValue, double newValue)
        {
            App.Value.Async_Invoke(App.AsyncGroup.Model_UpdateMyPosition, Model_UpdateMyPosition);
        }

        partial void Changed__State_MyLo(double oldValue, double newValue)
        {
            App.Value.Async_Invoke(App.AsyncGroup.Model_UpdateMyPosition, Model_UpdateMyPosition);
        }

        void Model_UpdateMyPosition()
        {
            Model_UpdateStations ();

            if (State_IsTrackingMyPosition)
            {
                State_La = State_MyLa;
                State_Lo = State_MyLo;
            }
        }

        void Model_UpdateStations ()
        {
            var location = new Location(State_MyLa, State_MyLo);

            for (int index = 0; index < State_Stations.Count; index++)
            {
                var station = State_Stations[index];

                station.Station_Distance = location.DistanceTo(new Location(
                                                                   station.Station_La,
                                                                   station.Station_Lo
                                                                   ));
            }
        }

        partial void Changed__State_IsTrackingMyPosition(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                State_La        = State_MyLa;
                State_Lo        = State_MyLo;
                State_ZoomLevel = 20        ;
            }
        }

        partial void Changed__State_Stations(StationList oldValue, StationList newValue)
        {
            App.Value.Async_Invoke(App.AsyncGroup.Model_UpdateStations, Model_UpdateStations);
        }

        partial void CollectionChanged__State_Stations(StationList value, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            App.Value.Async_Invoke(App.AsyncGroup.Model_UpdateStations, Model_UpdateStations);
        }
    }
}
