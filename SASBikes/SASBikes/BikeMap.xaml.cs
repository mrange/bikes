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

using System.Collections.Specialized;
using Bing.Maps;
using SASBikes.DataModel;
using Windows.UI;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SASBikes
{
    public sealed partial class BikeMap
    {
        readonly Brush m_openBackground     = new SolidColorBrush(Colors.DeepSkyBlue        );
        readonly Brush m_closedBackground   = new SolidColorBrush(Colors.Red                );
        readonly Brush m_meBackground       = new SolidColorBrush(Colors.MediumVioletRed    );
        Pushpin m_me;

        partial void GetBingLicenseKey(ref string key);

        partial void Constructed__BikeMap()
        {
            InitializeComponent();

            string key = "";

            GetBingLicenseKey(ref key);

            Map.Credentials = key;
            Map.MapType = MapType.Aerial;
            Map.SetView(new Location(57.700324, 11.973429), 18);
            Map.ViewChangeEnded += Map_ViewChangeEnded;
        }

        void Map_ViewChangeEnded(object sender, ViewChangeEndedEventArgs e)
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
            App.Value.Async_Invoke(App.AsyncGroup.Map_UpdateMapStations, Map_UpdateMapStations);
        }

        void Map_UpdateMapStations()
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
                                     Background = station.Station_IsOpen ? m_openBackground : m_closedBackground,
                                     Text       = station.Station_Number.ToString(),
                                 };

                    MapLayer.SetPosition(pp, new Location(station.Station_La, station.Station_Lo));

                    Map.Children.Add(pp);
                }
            }

            m_me = new Pushpin
            {
                Background  = m_meBackground    , 
                Text        = "Me"              ,
            };

            MapLayer.SetPosition(m_me, new Location(My_La, My_Lo));

            Map.Children.Add(m_me);
            
        }

        void Async_UpateView()
        {
            App.Value.Async_Invoke(App.AsyncGroup.Map_UpdateView, Map_UpdateView);
        }

        void Map_UpdateView()
        {
            Map.SetView(new Location(View_La, View_Lo), View_ZoomLevel);
        }

        partial void Changed_My_La(double oldValue, double newValue)
        {
            App.Value.Async_Invoke(App.AsyncGroup.Map_UpdateMyPosition, Map_UpdateMyPosition);
        }

        partial void Changed_My_Lo(double oldValue, double newValue)
        {
            App.Value.Async_Invoke(App.AsyncGroup.Map_UpdateMyPosition, Map_UpdateMyPosition);
        }

        void Map_UpdateMyPosition()
        {
            if (m_me != null)
            {
                MapLayer.SetPosition(m_me, new Location(My_La, My_Lo));                
            }
        }
    }

}
