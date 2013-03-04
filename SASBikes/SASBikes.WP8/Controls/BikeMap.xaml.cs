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
using System.Device.Location;
using System.Windows;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;
using SASBikes.Common.SASBikes.Common.AppServices;
using SASBikes.Common.SASBikes.Common.DataModel;

namespace SASBikes.WP8.Controls
{
    public partial class BikeMap
    {
        static readonly Brush   s_meBackground      = new SolidColorBrush(Colors.Magenta            );
        static readonly Color[] s_nearestColors     = 
            new []
                {
                    Colors.Green        ,
                    Colors.Yellow       ,
                    Colors.Orange       ,
                };

        readonly MapLayer       m_nearestLayer      = new MapLayer();
        readonly MapLayer       m_stationsLayer     = new MapLayer();

        partial void Constructed__BikeMap()
        {
            InitializeComponent();

            Map.CartographicMode = MapCartographicMode.Aerial;
            Map.SetView(new GeoCoordinate(View_La, View_Lo), View_ZoomLevel);
            Map.ViewChanged += Map_ViewChanged;

            Map.Layers.Add(m_stationsLayer);
            Map.Layers.Add(m_nearestLayer);
        }

        void Map_ViewChanged(object sender, MapViewChangedEventArgs mapViewChangedEventArgs)
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
            Services.App.Async_Invoke(AsyncGroup.Map_UpdateMapStations, Map_UpdateMapStations);
        }

        void Map_UpdateMapStations()
        {
            m_stationsLayer.Clear();
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
                                  };

                    var coordinate = new GeoCoordinate(station.Station_La, station.Station_Lo);
                    var mo = new MapOverlay
                                 {
                                     Content        = spp               ,
                                     GeoCoordinate  = coordinate        ,
                                     PositionOrigin = new Point (0,0)   ,
                                 };

                    m_stationsLayer.Add(mo);
                }
            }

            Map_UpdateNearestPositions ();
        }

        void Async_UpateView()
        {
            Services.App.Async_Invoke(AsyncGroup.Map_UpdateView, Map_UpdateView);
        }

        void Map_UpdateView()
        {
            Map.SetView(new GeoCoordinate(View_La, View_Lo), View_ZoomLevel);
        }

        partial void Changed_My_La(double oldValue, double newValue)
        {
            Services.App.Async_Invoke(AsyncGroup.Map_UpdateMyPosition, Map_UpdateMyPosition);
        }

        partial void Changed_My_Lo(double oldValue, double newValue)
        {
            Services.App.Async_Invoke(AsyncGroup.Map_UpdateMyPosition, Map_UpdateMyPosition);
        }

        void Map_UpdateMyPosition()
        {
            var myLa = My_La;
            var myLo = My_Lo;
            //MapLayer.SetPosition(m_me, new Location(myLa, myLo));
            
            //Map_UpdateNearestPositions();
        }

        void Map_UpdateNearestPositions ()
        {
            //var myLa = My_La;
            //var myLo = My_Lo;

            //var nearestStations = Stations
            //    .NearestOpenStations ()
            //    .Take(s_nearestColors.Length)
            //    .ToArray();

            //m_nearestLayer.Shapes.Clear();

            //for (int index = 0; index < nearestStations.Length; index++)
            //{
            //    var nearest = nearestStations[index];
            //    m_nearestLayer.Shapes.Add(
            //        new MapPolyline
            //            {
            //                Color = s_nearestColors[index],
            //                Width = 5.0,
            //                Visible = true,
            //                Locations =
            //                    new LocationCollection
            //                        {
            //                            new Location(myLa, myLo),
            //                            new Location(nearest.Station_La, nearest.Station_Lo),
            //                        }
            //            });
            //}
        }
    }
}
