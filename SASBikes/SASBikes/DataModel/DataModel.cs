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
