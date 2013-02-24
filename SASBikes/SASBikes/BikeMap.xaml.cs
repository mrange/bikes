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
using System.Linq;
using Bing.Maps;
using SASBikes.DataModel;
using SASBikes.PushPins;
using Windows.UI;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SASBikes
{
    public sealed partial class BikeMap
    {
        static readonly Brush   s_openBackground    = new SolidColorBrush(Colors.DeepSkyBlue        );
        static readonly Brush   s_closedBackground  = new SolidColorBrush(Colors.Red                );
        static readonly Brush   s_meBackground      = new SolidColorBrush(Colors.MediumVioletRed    );
        static readonly Color[] s_nearestColors     = 
            new []
                {
                    Colors.LawnGreen  ,
                    Colors.Yellow       ,
                    Colors.Orange       ,
                };

        Pushpin                 m_me;
        readonly MapShapeLayer  m_nearestLayer      = new MapShapeLayer();
        readonly MapLayer       m_stationsLayer     = new MapLayer();

        partial void GetBingLicenseKey(ref string key);

        partial void Constructed__BikeMap()
        {
            InitializeComponent();

             var key = "";

            GetBingLicenseKey(ref key);

            Map.Credentials = key;
            Map.MapType = MapType.Aerial;
            Map.SetView(new Location(View_La, View_Lo), View_ZoomLevel);
            Map.ViewChangeEnded += Map_ViewChangeEnded;

            m_me = new Pushpin
            {
                Background  = s_meBackground    , 
            };

            MapLayer.SetPosition(m_me, new Location(My_La, My_Lo));

            Map.ShapeLayers.Add(m_nearestLayer);
            Map.Children.Add(m_stationsLayer);
            Map.Children.Add(m_me);
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
            m_stationsLayer.Children.Clear();
            var stations = Stations;
            if (stations != null)
            {
                for (var index = 0; index < stations.Count; index++)
                {
                    var station = stations[index];
                    var spp = new StationPushPin
                                  {
                                      IsOpen        = station.Station_IsOpen                                ,
                                      StationName   = station.Station_Name                                  ,
                                      Location      = new Location(station.Station_La, station.Station_Lo)  , 
                                  };

                    m_stationsLayer.Children.Add(spp);
                }
            }

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
            var myLa = My_La;
            var myLo = My_Lo;
            MapLayer.SetPosition(m_me, new Location(myLa, myLo));
            
            var stations = Stations;
            if (stations != null)
            {
                var nearestThree = Stations
                    .Where (s => s.Station_IsOpen)
                    .OrderBy (s=>s.Station_Distance)
                    .Take(s_nearestColors.Length)
                    .ToArray();

                m_nearestLayer.Shapes.Clear();

                for (int index = 0; index < nearestThree.Length; index++)
                {
                    var nearest = nearestThree[index];
                    m_nearestLayer.Shapes.Add(
                        new MapPolyline
                            {
                                Color = s_nearestColors[index],
                                Width = 5.0,
                                Visible = true,
                                Locations =
                                    new LocationCollection
                                        {
                                            new Location(myLa, myLo),
                                            new Location(nearest.Station_La, nearest.Station_Lo),
                                        }
                            });
                }
            }
    
        }
    }

}
