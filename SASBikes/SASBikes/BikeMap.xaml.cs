// ReSharper disable InconsistentNaming

using System.Collections.Specialized;
using Bing.Maps;
using SASBikes.DataModel;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SASBikes
{
    public sealed partial class BikeMap
    {
        readonly Brush m_openBrush = new SolidColorBrush(Colors.WhiteSmoke);
        readonly Brush m_closedBrush = new SolidColorBrush(Colors.Crimson);

        bool m_isUpdatingView;
        bool m_isUpdatingStations;

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
            if (!m_isUpdatingStations)
            {
                var task = Dispatcher.RunAsync(CoreDispatcherPriority.Low, UpdateStations);
                m_isUpdatingStations = true;
            }
        }

        void UpdateStations()
        {
            try
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
            finally
            {
                m_isUpdatingStations = false;
            }
        }

        void Async_UpateView()
        {
            if (!m_isUpdatingView)
            {
                var task = Dispatcher.RunAsync(CoreDispatcherPriority.Low, UpdateView);
                m_isUpdatingView = true;
            }
        }

        void UpdateView()
        {
            try
            {
                Map.SetView(new Location(View_La, View_Lo), View_ZoomLevel);
            }
            finally
            {
                m_isUpdatingView = false;
            }
        }
    }
}
