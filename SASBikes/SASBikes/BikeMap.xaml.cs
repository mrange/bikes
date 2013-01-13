// ReSharper disable InconsistentNaming

using System.Collections.Specialized;
using Bing.Maps;
using SASBikes.DataModel;
using Windows.UI;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SASBikes
{
    public sealed partial class BikeMap
    {
        readonly Brush m_openBrush = new SolidColorBrush(Colors.WhiteSmoke);
        readonly Brush m_closedBrush = new SolidColorBrush(Colors.Crimson);

        partial void Constructed__BikeMap()
        {
            InitializeComponent();
            Map.Credentials = BingLicenseKeys.TrialKey;
            Map.MapType = MapType.Aerial;
            Map.SetView(new Location(57.700324, 11.973429), 18);
            Map.ViewChangeEnded += View_ChangeEnded;
        }

        void View_ChangeEnded(object sender, ViewChangeEndedEventArgs e)
        {
            View_Lo         = Map.Center.Longitude  ;
            View_La         = Map.Center.Latitude   ;
            View_ZoomLevel  = Map.ZoomLevel         ;
        }

        partial void Changed_View_La(double oldValue, double newValue)
        {
            Async_UpateView();
        }

        partial void Changed_View_Lo(double oldValue, double newValue)
        {
            Async_UpateView();
        }

        partial void Changed_View_ZoomLevel(double oldValue, double newValue)
        {
            Async_UpateView();
        }

        partial void Changed_Stations(StationList oldValue, StationList newValue)
        {
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= CollectionChanged_Stations;
            }

            if (newValue != null)
            {
                newValue.CollectionChanged += CollectionChanged_Stations;
            }

            Async_UpdateStations(); 
        }

        void CollectionChanged_Stations(object sender, NotifyCollectionChangedEventArgs e)
        {
            Async_UpdateStations();
        }

        void Async_UpdateStations()
        {
            App.Value.Async_Invoke(App.AsyncGroup.UpdateMapStations, UpdateMapStations);
        }

        void UpdateMapStations()
        {
            Map.Children.Clear();
            var stations = Stations;
            if (stations != null)
            {
                for (int index = 0; index < stations.Count; index++)
                {
                    var station = stations[index];
                    var pp = new Pushpin
                                 {
                                     Tag = station.Station_Name,
                                     Foreground = station.Station_IsOpen ? m_openBrush : m_closedBrush,
                                     Text = station.Station_Number.ToString(),
                                 };

                    MapLayer.SetPosition(pp, new Location(station.Station_La, station.Station_Lo));

                    Map.Children.Add(pp);
                }
            }
        }

        void Async_UpateView()
        {
            App.Value.Async_Invoke(App.AsyncGroup.UpdateMapView, UpdateMapView);
        }

        void UpdateMapView()
        {
            Map.SetView(new Location(View_La, View_Lo), View_ZoomLevel);
        }
    }
}
