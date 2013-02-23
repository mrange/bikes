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

using Bing.Maps;

namespace SASBikes.DataModel
{
    partial class State
    {
        partial void Changed__State_MyLa(double oldValue, double newValue)
        {
            App.Value.Async_Invoke(App.AsyncGroup.UpdateStationDistances, UpdateStationDistances);
        }

        partial void Changed__State_MyLo(double oldValue, double newValue)
        {
            App.Value.Async_Invoke(App.AsyncGroup.UpdateStationDistances, UpdateStationDistances);
        }

        void UpdateStationDistances()
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
    }
}
